using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GRSAPIIntegrationLibrary.Models;
using GRSAPIIntegrationLibrary.Models.APIModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace GRSAPIIntegrationLibrary.Services
{
    public class GRSAPIService
    {
        public async Task<OrderItems> GetOrderItems(GRSOrder order)
        {
            OrderItems orderItems = new OrderItems();                                        
            var partnerOrderNumber = order.ExternalConfirmationRef + "-" + (order.LineItemNumber / 100);
                                            
            HttpResponseMessage response = null;
            try
            {
                response = ApiHelper.mHttpClient.GetAsync($"order_items?partner_order_numbers={partnerOrderNumber}").Result;
                 
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();

                    var jsArray = (JArray)JsonConvert.DeserializeObject(jsonString.Result);
                    var jsObject = (JObject)jsArray[0];

                    orderItems.OrderNumber = Convert.ToInt32(jsObject["order_number"]);
                    orderItems.OfferingType = jsObject["reward"]["offering_type"].ToString();
                    orderItems.GalleryId = Convert.ToInt32(jsObject["gallery_id"]);
                    orderItems.CatalogId = Convert.ToInt32(jsObject["catalog_id"]);
                    orderItems.RewardId = Convert.ToInt32(jsObject["reward"]["reward_id"]);
                    orderItems.PartnerCurrency = jsObject["partner_currency"].ToString();
                    orderItems.CurrencyCode = jsObject["reward"]["currency_code"].ToString();
                    orderItems.MRSP = Convert.ToDouble(jsObject["reward"]["msrp"]);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }

            }
            catch (Exception e)
            {
                throw new Exception(response.ReasonPhrase + " " + e.Message.ToString());
            }

            return orderItems;
        }

        public async Task<double> GetRewardDetails(RewardItem rewardItem)
        {
            double msrp = 0;

            HttpResponseMessage response = null;
            try
            {
                response = ApiHelper.mHttpClient.GetAsync($"rewards/{rewardItem.RewardId}/details?gallery_id={rewardItem.GalleryId}" +
                                                    $"&catalog_id={rewardItem.CatalogId}&country={rewardItem.CountryCode}&language={rewardItem.Language}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();

                    dynamic dMSRP = JsonConvert.DeserializeObject(jsonString.Result);
                    msrp = Convert.ToDouble(dMSRP.msrp);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }                   

            }
            catch (Exception e)
            {
                throw new Exception(response.ReasonPhrase + " " + e.Message.ToString());
            }

            return msrp;
        }        
    }
}
