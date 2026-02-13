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

        [HttpGet]
        [Route("eppByID/{id}")]
        public async Task<IActionResult> GetEppById(int id)
        {
            var epp = await _unitOfWork
                        .Repository<Epp>()
                        .GetByIdAsync(id);

            if(epp == null)
                return NotFound();

            return Ok(epp);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(EppDto request)
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
                PreviousConditionId = request.PreviousConditionId,
                createdAt = DateTime.Now
            };

            await _unitOfWork.Repository<Epp>().AddAsync(newEpp);

            await _unitOfWork.SaveAsync();

            return Ok(newEpp);
        }
    }
}