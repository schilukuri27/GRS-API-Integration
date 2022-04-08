using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRSAPIIntegrationLibrary.Models;
using GRSAPIIntegrationLibrary.Models.APIModels;
using System.Configuration;

namespace GRSAPIIntegrationLibrary.Services
{
    public class GRSDBDataService
    {
        private string DBConnectionString = "Server=tcp:castest.database.windows.net,1433;Initial Catalog=Site_Vendor;Persist Security Info=False;" +
                        "User ID=grsTest;Password=GR$14Acc0unt$y$tem;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public List<GRSOrder> GetGRSOrders()
        {
            List<GRSOrder> results = new List<GRSOrder>();

            //results = getMockGRSOrders();

            using (SqlConnection conn = new SqlConnection(DBConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "grsTest.sprocGetGRSOrdersWithoutCostDetails";
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();

                    if (reader.Read())
                    {
                        dt.Load(reader);
                        int index = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            index++;
                            if (index > 5)
                            {
                                break;
                            }
                            GRSOrder gOrder = new GRSOrder();
                            gOrder.OrderId = Convert.ToInt32(row["OrderID"]);
                            gOrder.OrderDetailId = Convert.ToInt32(row["OrderDetailID"]);
                            gOrder.ExternalConfirmationRef = row["ExternalConfirmationRef"].ToString();
                            gOrder.LineItemNumber = Convert.ToInt32(row["LineItemNumber"]);
                            gOrder.Quantity = Convert.ToInt32(row["Quantity"]);
                            gOrder.CountryCode = row["CountryCode"].ToString();
                            gOrder.ItemDescription = row["Item Description"].ToString();

                            results.Add(gOrder);
                        }
                    }

                    reader.Close();
                }
            }

            return results;
        }
        public void UpdateGRSCostDetails(GRSCostDetail costDetailItem)
        {
            using (SqlConnection conn = new SqlConnection(DBConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "insert into grsTest.tblGRSCostDetails (OrderDetailID, OfferingType, MSRP)  values(@orderDetailId, @offeringType, @msrp)";
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@orderDetailId", costDetailItem.OrderDetailId);
                    cmd.Parameters.AddWithValue("@offeringType", costDetailItem.OfferingType);
                    cmd.Parameters.AddWithValue("@msrp", costDetailItem.MSRP);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private List<GRSOrder> getMockGRSOrders()
        {
            List<GRSOrder> mockOrders = new List<GRSOrder>();
            for (int i = 1; i <= 5; i++)
            {
                GRSOrder order = new GRSOrder();

                order.OrderId = i;
                order.ExternalConfirmationRef = (i * 1000).ToString();
                order.LineItemNumber = (i * 100);
                order.CountryCode = "can";

                mockOrders.Add(order);
            }

            return mockOrders;
        }
    }
}
