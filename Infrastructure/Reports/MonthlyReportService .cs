using Application.Features.Reports;
using ClosedXML.Excel;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Infrastructure.Reports
{
    public class MonthlyReportService : IMonthlyReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MonthlyReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(byte[] Content, string FileName)> GenerateMonthlyReportAsync(MonthlyReportDto dto)
        {
            if (dto.Month < 1 || dto.Month > 12) throw new ArgumentException("Mes ínvalido");

            var data = await _unitOfWork
                        .Repository<Epp>()
                        .Query()
                        .Include(e => e.ReasonRequest)
                        .Include(e => e.PreviousCondition)
                        .Include(e => e.Store)
                            .ThenInclude(s => s.ApplicationStatus)
                        .Where(e => e.CreatedAt.HasValue &&
                                e.CreatedAt.Value.Year == dto.Year &&
                                e.CreatedAt.Value.Month == dto.Month)
                        .AsNoTracking()
                        .ToListAsync();

            if (!data.Any()) throw new InvalidOperationException("No hay datos para el mes especificado");

            using var worbook = new XLWorkbook();
            var worksheet = worbook.Worksheets.Add("Reporte mensual");

            var monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dto.Month);

            worksheet.Cell(1, 1).Value = $"REPORTE EPP - {monthName.ToUpper()} {dto.Year}";
            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(1, 1).Style.Font.FontSize = 16;
            worksheet.Range(1, 1, 1, 10).Merge();

            var headers = new[]
            {
                "Solicitante",
                "Área",
                "Puesto",
                "Tipo EPP",
                "Talla",
                "Cantidad",
                "Motivo",
                "Condición previa",
                "Estado",
                "Fecha solicitud"
            };

            for(int i = 0; i < headers.Length; i++)
            {
                worksheet.Cell(3, i + 1).Value = headers[i];
                worksheet.Cell(3, i + 1).Style.Font.Bold = true;
                worksheet.Cell(3, i + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
            }

            int row = 4;

            foreach(var item in data)
            {
                worksheet.Cell(row, 1).Value = item.Name;
                worksheet.Cell(row, 2).Value = item.Area;
                worksheet.Cell(row, 3).Value = item.Position;
                worksheet.Cell(row, 7).Value = item.ReasonRequest?.NameReason ?? "N/A";
                worksheet.Cell(row, 8).Value = item.PreviousCondition?.NameCondition ?? "N/A";
                worksheet.Cell(row, 9).Value = item.Store?.ApplicationStatus?.nameStatus ?? "Pendiente";
                worksheet.Cell(row, 10).Value = item.CreatedAt?.ToString("dd/MM/yyyy");

                row++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            worbook.SaveAs(stream);

            var fileName = $"Reporte_EPP_{monthName}_{dto.Year}.xlsx";

            return (stream.ToArray(), fileName);
        }
    }
}