using Common;
using Invoice.Common;
using Invoice.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIService.Controllers.Api
{
    public class InvoiceController : ApiController
    {
        private readonly IInvoiceRepository DAL;

        public InvoiceController()
        {
            DAL = new InvoiceDal();
        }
        
        [Route("api/invoice/package/{lineId}")]
        public Package GetPackage(int lineId)
        {
            try
            {
                return DAL.GetPackage(lineId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("api/invoice/calls/{lineId}/{month}")]
        public IEnumerable<Call> GetCalls(int lineId, int month)
        {
            try
            {
                //return DAL.GetCalls(lineId, month);
                return new Call[10];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
