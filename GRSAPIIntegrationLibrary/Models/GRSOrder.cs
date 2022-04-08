using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRSAPIIntegrationLibrary.Models
{
    public class GRSOrder
    {
        public int OrderId { get; set; }
        public int OrderDetailId { get; set; }
        public string ExternalConfirmationRef { get; set; }
        public int LineItemNumber { get; set; }
        public string CountryCode { get; set; }
        public string ItemDescription { get; set; }
        public int Quantity { get; set; }
    }
}
