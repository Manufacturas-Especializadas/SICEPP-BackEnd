using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Store
    {
        public int Id { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string? AuthorizedBy { get; set; }

        public DateTime? LastDelivery { get; set; }

        public bool ReplacementPolicy { get; set; }

        public int StatusId { get; set; }

        public bool DeliveryConfirmation { get; set; }

        public int EppId { get; set; }

        public Epp Epp { get; set; }

        public ApplicationStatus? ApplicationStatus { get; set; }
    }
}