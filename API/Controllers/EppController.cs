using Application.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        [Route("{id}/details")]
        public async Task<IActionResult> GetEppDetails(int id)
        {
            var result = await _unitOfWork
                        .Repository<Epp>()
                        .Query()
                        .Where(x => x.Id == id)
                        .Select(x => new EppDetailWithStoreDto
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Area = x.Area,
                            Position = x.Position,
                            Shift = x.Shift,
                            RequestedQuantity = x.RequestedQuantity,
                            DeliveryEPPPrevious = x.DeliveryEPPPrevious,
                            CreatedAt = x.createdAt,

                            EppType = x.EppType.NameType,
                            Size = x.Size.NameSize,
                            ReasonRequest = x.ReasonRequest.NameReason,
                            PreviousCondition = x.PreviousCondition.NameCondition,

                            Store = x.Store == null ? null : new StoreDetailDto
                            {
                                DeliveryDate = x.Store.DeliveryDate,
                                AuthorizedBy = x.Store.AuthorizedBy,
                                LastDelivery = x.Store.LastDelivery,
                                ReplacementPolicy = x.Store.ReplacementPolicy,
                                StatusId = x.Store.StatusId,
                                DeliveryConfirmation = x.Store.DeliveryConfirmation
                            }
                        })
                        .AsNoTracking()
                        .FirstOrDefaultAsync();

            if (result == null) return NotFound();

            return Ok(result);
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