using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Configuration;

namespace GRSAPIIntegrationLibrary
{
    public class ApiHelper
    {
        public static HttpClient mHttpClient { get; set; }
        private static string accessToken = "";

        private static string baseURL = "https://api.rewardcloud.io/";

        public static void InitializeClient()
        {
            mHttpClient = new HttpClient();
            mHttpClient.BaseAddress = new Uri(baseURL);
            mHttpClient.DefaultRequestHeaders.Clear();
            mHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            accessToken = getToken();
            mHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public static string getToken()
        {
            string tokenValue = "";

            var values = new Dictionary<string, string>();
            values.Add("username", "cashortit@cashort.com");
            values.Add("password", "mfBVx4e!_F");
            values.Add("grant_type", "password");
            values.Add("client_id", "88_54w2dcxnmqgwo0gcc4s0cc80o8k480wscgwo4osog4ko80w0k8");
            values.Add("client_secret", "55g11xbyaskkkc8o8wcwosgo4k4888008cccw0k08gk44w4gss");

            var jsonString = JsonConvert.SerializeObject(values);
            HttpResponseMessage response = null;
            try
            {
                response = mHttpClient.PostAsync("auth/token", new StringContent(jsonString, Encoding.UTF8, "application/json")).Result;
                var result = response.Content.ReadAsStringAsync();
                dynamic token = JsonConvert.DeserializeObject(result.Result);
                tokenValue = token.token;
            }
            catch (Exception e)
            {
                throw new Exception(response.ReasonPhrase + " " + e.Message.ToString());
            }

            return tokenValue;
        }


    }
}
