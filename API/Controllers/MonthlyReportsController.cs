using Application.Features.Reports;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonthlyReportsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMonthlyReportService _reportService;

        public MonthlyReportsController(IUnitOfWork unitOfWork, IMonthlyReportService reportService)
        {
            _unitOfWork = unitOfWork;
            _reportService = reportService;
        }

        [HttpGet]
        [Route("GetMonths")]
        public async Task<IActionResult> GetMonths()
        {
            var months = await _unitOfWork
                .Repository<Epp>()
                .Query()
                .Select(e => new
                {
                    Year = e.CreatedAt.Year,
                    Month = e.CreatedAt.Month
                })
                .Distinct()
                .OrderByDescending(x => x.Year)
                .ThenByDescending(x => x.Month)
                .AsNoTracking()
                .ToListAsync();

            var result = months.Select(e => new
            {
                Year = e.Year,
                Month = e.Month,
                MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(e.Month),
                Description = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(e.Month)} {e.Year}"
            });

            return Ok(result);
        }

        [HttpPost]
        [Route("GenerateMonthlyReport")]
        public async Task<IActionResult> GenerateMonthlyReport([FromBody] MonthlyReportDto dto)
        {
            try
            {
                var result = await _reportService.GenerateMonthlyReportAsync(dto);

                return File(
                    result.Content,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    result.FileName
                );
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }            
        }
    }
}