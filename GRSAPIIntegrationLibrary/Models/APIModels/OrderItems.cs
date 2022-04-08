using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRSAPIIntegrationLibrary.Models.APIModels
{
    public class OrderItems
    {
        public int OrderNumber { get; set; }
        public string OfferingType { get; set; }
        public string CurrencyCode { get; set; }
        public string PartnerCurrency { get; set; }
        public int RewardId { get; set; }
        public int GalleryId { get; set; }
        public int CatalogId { get; set; }
        public double MRSP { get; set; }
    }
}
