using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class EppDetailWithStoreDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Area { get; set; }

        public string Position { get; set; }

        public string Shift { get; set; }

        public bool DeliveryEPPPrevious { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string ReasonRequest { get; set; }

        public string PreviousCondition { get; set; }

        public List<DetailResponseDto> Details { get; set; } = new();

        public StoreDetailDto? Store { get; set; }
    }
}