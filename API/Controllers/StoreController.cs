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
    public class StoreController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateStore(StoreDto request)
        {


            var eppExists = await _unitOfWork
                    .Repository<Epp>()
                    .Query()
                    .AnyAsync(x => x.Id == request.EppId);

            if (!eppExists) return NotFound("EPP no encontrado");

            var existingStore = await _unitOfWork
                        .Repository<Store>()
                        .Query()
                        .FirstOrDefaultAsync(x => x.EppId == request.EppId);

            if (existingStore == null)
            {
                var newStore = new Store
                {
                    EppId = request.EppId,
                    DeliveryDate = request.DeliveryDate ?? DateTime.Now,
                    AuthorizedBy = request.AuthorizedBy,
                    LastDelivery = request.LastDelivery,
                    ReplacementPolicy = request.ReplacementPolicy ?? false,
                    StatusId = request.StatusId,
                    DeliveryConfirmation = request.DeliveryConfirmation ?? false
                };

                await _unitOfWork.Repository<Store>().AddAsync(newStore);
            }
            else
            {
                existingStore.DeliveryDate = request.DeliveryDate;
                existingStore.AuthorizedBy = request.AuthorizedBy;
                existingStore.LastDelivery = request.LastDelivery;
                existingStore.ReplacementPolicy = request.ReplacementPolicy!.Value;
                existingStore.StatusId = request.StatusId;
                existingStore.DeliveryConfirmation = request.DeliveryConfirmation!.Value;
            }

            await _unitOfWork.SaveAsync();

            return Ok(new { message = "Store guardado correctamente" });
        }
    }
}