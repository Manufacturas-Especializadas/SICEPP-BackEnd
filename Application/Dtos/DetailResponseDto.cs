using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class DetailResponseDto
    {
        public string EppType { get; set; }

        public string Size { get; set; }

        public int RequestedQuantity { get; set; }
    }
}
