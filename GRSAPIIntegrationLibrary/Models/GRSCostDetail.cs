using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRSAPIIntegrationLibrary.Models
{
    public class GRSCostDetail
    {
        public int OrderDetailId { get; set; }
        public double MSRP { get; set; }
        public string OfferingType { get; set; }
    }
}
