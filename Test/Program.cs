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
                //var agent = bl.AddServiceAgent("Omer", "111");
                var test = new CrmDal();
                //bl.UpdateServiceAgent(7, "Omer", "546", 2);
                var newagent = new ServiceAgent("Omer", "456", 1);
                test.UpdateServiceAgent(newagent, 7);
                //var client = bl.AddClient("hhhh", "ss", 5555, 5, "yaffo", "054", 7);

                //var newagent = new ServiceAgent("jhon", "132");
                //test.AddServiceAgent(newagent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }            
        }
    }
}
