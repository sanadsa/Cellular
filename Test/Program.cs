﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.DAL;
using Common;
using System.Data.Entity.Validation;
using CRM.BL;
using CRM.Common.Interfaces;
using Invoice.DAL;
using Invoice.Common;
using Invoice.BL;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ICrmRepository DAL = new CrmDal();
                IInvoiceRepository invoiceRepository = new InvoiceDal();
                var bl = new CrmBl();
                var invoiceBl = new InvoiceBl();
                var test = new CrmDal();
                var invoiceDal = new InvoiceDal();
                //var agent = bl.AddServiceAgent("Omer", "111");
                var newagent = new ServiceAgent("Iron", "3456");
                var client = new Client("hds", "ss", 5555, 3, "yaffo", "0546", 5);
                var package = new Package("pp", 3, 50, new DateTime(2000, 11, 10), 120, 150, 0.3, 1, false, false);
                // var p = invoiceRepository.GetPackage(3);
                //Console.WriteLine(p.PackageName);
                // var calls = invoiceRepository.GetCalls(3, new DateTime(2000,11,10));
                Console.WriteLine(invoiceBl.GetMinutesLeft(3, new DateTime(2000,11,10)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }            
        }
    }
}
