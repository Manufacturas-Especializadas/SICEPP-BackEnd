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

        public MonthlyReportsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("GetMonths")]
        public async Task<IActionResult> GetMonths()
        {
            var months = await _unitOfWork
                        .Repository<Epp>()
                        .Query()
                        .Where(e => e.createdAt.HasValue)
                        .Select(e => new
                        {
                            Year = e.createdAt.Value.Year,
                            Month = e.createdAt.Value.Month,
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
                Description = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(e.Month)}{e.Year}"
            });

            return Ok(result);
        }        
    }
}