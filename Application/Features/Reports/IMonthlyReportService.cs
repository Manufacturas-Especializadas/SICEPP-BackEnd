namespace Application.Features.Reports
{
    public interface IMonthlyReportService
    {
        Task<(byte[] Content, string FileName)> GenerateMonthlyReportAsync(MonthlyReportDto dto);
    }
}