using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRSAPIIntegrationLibrary.Models.APIModels
{
    public class RewardItem
    {
        public int RewardId { get; set; }
        public int GalleryId { get; set; }
        public int CatalogId { get; set; }
        public string CountryCode { get; set; }
        public string Language { get; set; }
    }
}
