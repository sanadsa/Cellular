using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.DAL;
using Common;
using System.Data.Entity.Validation;
using CRM.BL;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var bl = new CrmBl();
                var test = new CrmDal();
                //var agent = bl.AddServiceAgent("Omer", "111");
                var newagent = new ServiceAgent("Morgan", "3456");
                var client = new Client("hds", "ss", 5555, 3, "yaffo", "0546", 7);
                //test.UpdateServiceAgent(newagent, 7);
                test.AddClient(client);
                //test.AddServiceAgent(newagent);

                //bl.UpdateServiceAgent(7, "Omer", "10101", 78);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }            
        }
    }
}
