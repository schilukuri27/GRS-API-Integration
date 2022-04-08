using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRSAPIIntegrationLibrary.Models;
using GRSAPIIntegrationLibrary.Models.APIModels;
using GRSAPIIntegrationLibrary.Services;

namespace GRSAPIIntegrationLibrary
{
    public class ProcessGRSOrders
    {
        private List<GRSOrder> orders;
        private OrderItems orderItems;
        private RewardItem rewardItem;
        private double finalMSRP;

        private GRSDBDataService dbService;
        private GRSAPIService apiService;

        public ProcessGRSOrders()
        {
            ApiHelper.InitializeClient();
            dbService = new GRSDBDataService();
            apiService = new GRSAPIService();
        }

        public async void GRSOrdersUpdate()
        {
            orders = dbService.GetGRSOrders();

            foreach (var order in orders)
            {
                orderItems = await apiService.GetOrderItems(order);
                finalMSRP = orderItems.MRSP;
                if (orderItems.OfferingType == "Gift Card" || orderItems.OfferingType == "Mobile Top-up")
                {                    
                    if (orderItems.CurrencyCode != orderItems.PartnerCurrency)
                    {
                        rewardItem = new RewardItem();
                        rewardItem.RewardId = orderItems.RewardId;
                        rewardItem.CatalogId = orderItems.CatalogId;
                        rewardItem.GalleryId = orderItems.GalleryId;
                        rewardItem.CountryCode = order.CountryCode;
                        rewardItem.Language = "en";
                        finalMSRP = await apiService.GetRewardDetails(rewardItem);
                    }                    
                }

                GRSCostDetail costDetailItem = new GRSCostDetail();
                costDetailItem.OrderDetailId = order.OrderDetailId;
                costDetailItem.MSRP = finalMSRP;
                costDetailItem.OfferingType = orderItems.OfferingType;
                dbService.UpdateGRSCostDetails(costDetailItem);
            }
        }      

        public void testApi()
        {
            ApiHelper.getToken();
        }        
    }
}
