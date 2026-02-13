using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}