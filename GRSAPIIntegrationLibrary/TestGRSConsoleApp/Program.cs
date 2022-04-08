using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRSAPIIntegrationLibrary;

namespace TestGRSConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            GRSAPIIntegrationLibrary.ProcessGRSOrders g = new ProcessGRSOrders();
           g.GRSOrdersUpdate();
        }
    }
}
