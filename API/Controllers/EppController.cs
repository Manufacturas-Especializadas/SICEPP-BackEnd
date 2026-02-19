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
                            DeliveryEPPPrevious = x.DeliveryEPPPrevious,
                            CreatedAt = x.CreatedAt,

                            ReasonRequest = x.ReasonRequest.NameReason,
                            PreviousCondition = x.PreviousCondition.NameCondition,

                            Details = x.EppDetails.Select(d => new DetailResponseDto
                            {
                                EppType = d.EppType.NameType,
                                Size = d.Size != null ? d.Size.NameSize : null!,
                                RequestedQuantity = d.RequestedQuantity
                            }).ToList(),

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
            if (request.Details == null || !request.Details.Any())
                return BadRequest("Debe agregar al menos un EPP.");

            var newEpp = new Epp
            {
                Name = request.Name,
                Area = request.Area,
                Position = request.Position,
                Shift = request.Shift,
                DeliveryEPPPrevious = request.DeliveryEPPPrevious,
                ReasonRequestId = request.ReasonRequestId,
                PreviousConditionId = request.PreviousConditionId,
                CreatedAt = DateTime.Now
            };

            foreach (var detail in request.Details)
            {
                newEpp.EppDetails.Add(new EppDetail
                {
                    EppTypeId = detail.EppTypeId,
                    SizeId = detail.SizeId,
                    RequestedQuantity = detail.RequestedQuantity
                });
            }

            await _unitOfWork.Repository<Epp>().AddAsync(newEpp);
            await _unitOfWork.SaveAsync();

            return Ok(new { message = "Solicitud creada correctamente" });
        }
    }
}