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
                var agent = bl.AddServiceAgent("Omer", "111");
                //var test = new CRMDAL();
                //var newagent = new ServiceAgent("jhon", "132");
                //test.AddServiceAgent(newagent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //try
            //{
            //    var test = new CRMDAL();
            //    var newagent = new ServiceAgent("jordan", "132");
            //    test.AddServiceAgent(newagent);
            //}
            //catch (DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}
        }
    }
}
