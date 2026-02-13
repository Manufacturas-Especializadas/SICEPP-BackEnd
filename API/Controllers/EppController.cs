using Application.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EppController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EppController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(EppDto request)
        {
            try
            {
                var newEpp = new Epp
                {
                    Name = request.Name,
                    Area = request.Area,
                    Position = request.Position,
                    Shift = request.Shift,
                    RequestedQuantity = request.RequestedQuantity,
                    DeliveryEPPPrevious = request.DeliveryEPPPrevious,
                    EppTypeId = request.EppTypeId,
                    SizeId = request.SizeId,
                    ReasonRequestId = request.ReasonRequestId,
                    PreviousConditionId = request.PreviousConditionId
                };

                await _unitOfWork.Repository<Epp>().AddAsync(newEpp);

                await _unitOfWork.SaveAsync();

                return Ok(newEpp);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }           
        }
    }
}