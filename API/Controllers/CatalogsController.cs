using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CatalogsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("epp-types")]
        public async Task<IActionResult> GetEppTypes()
        {
            var data = await _unitOfWork.Repository<EppType>().GetAllAsync();

            return Ok(data);
        }

        [HttpGet]
        [Route("sizes")]
        public async Task<IActionResult> GetSizes()
        {
            var data = await _unitOfWork.Repository<Size>().GetAllAsync();

            return Ok(data);
        }

        [HttpGet]
        [Route("ReasonRequest")]
        public async Task<IActionResult> GetReasonRequest()
        {
            var data = await _unitOfWork.Repository<ReasonRequest>().GetAllAsync();

            return Ok(data);
        }

        [HttpGet]
        [Route("previous-condition")]
        public async Task<IActionResult> GetPreviousCondition()
        {
            var data = await _unitOfWork.Repository<PreviousCondition>().GetAllAsync();

            return Ok(data);
        }

        [HttpGet]
        [Route("status")]
        public async Task<IActionResult> GetStatus()
        {
            var data = await _unitOfWork.Repository<ApplicationStatus>().GetAllAsync();

            return Ok(data);
        }

        [HttpGet]
        [Route("epp")]
        public async Task<IActionResult> GetEpp()
        {
            var data = await _unitOfWork
                    .Repository<Epp>()
                    .Query()
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.Area,
                        x.Position,
                        x.Shift,
                        Status = x.Store.ApplicationStatus!.nameStatus ?? "Pendiente",
                        ReasonRequest = x.ReasonRequest.NameReason,
                        PreviousCondition = x.PreviousCondition.NameCondition,
                        x.DeliveryEPPPrevious,
                        ItemsCount = x.EppDetails.Count(),
                        x.CreatedAt
                    })
                    .AsNoTracking()
                    .ToListAsync();

            return Ok(data);
        }
    }
}