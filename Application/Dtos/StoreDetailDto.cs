using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class StoreDetailDto
    {
        public DateTime? DeliveryDate { get; set; }

        public string? AuthorizedBy { get; set; }

        public DateTime? LastDelivery { get; set; }

        public bool? ReplacementPolicy { get; set; }

        public int? StatusId { get; set; }

        public bool? DeliveryConfirmation { get; set; }
    }
}
