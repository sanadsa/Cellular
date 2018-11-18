using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.DAL;
using Common;
using System.Data.Entity.Validation;
using CRM.BL;
using CRM.Common.Interfaces;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ICrmRepository DAL = new CrmDal();
                 
                var bl = new CrmBl();
                var test = new CrmDal();
                //var agent = bl.AddServiceAgent("Omer", "111");
                var newagent = new ServiceAgent("Iron", "3456");
                var client = new Client("hds", "ss", 5555, 3, "yaffo", "0546", 5);
                //test.UpdateServiceAgent(newagent, 7);
                //DAL.AddServiceAgent(newagent);
                //test.AddClient(client);
                //test.AddServiceAgent(newagent);
                bl.AddServiceAgent("abla", "5050");
                //bl.UpdateServiceAgent(7, "Omer", "10101", 78);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }            
        }
    }
}
