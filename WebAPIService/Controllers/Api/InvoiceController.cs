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

        [HttpPost]
        [Route("api/crm/payment")]
        public HttpResponseMessage CreatePayment([FromBody]Payment payment)
        {
            try
            {
                var pay = DAL.AddPayment(payment);
                return Request.CreateResponse(HttpStatusCode.OK, pay);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [Route("api/invoice/package/{lineId}")]
        public HttpResponseMessage GetPackage(int lineId)
        {
            var pack = DAL.GetPackage(lineId);
            if (pack == null)
            {
                var message = string.Format("Package with line id = {0} not found", lineId);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, pack);
            }
        }

        [Route("api/invoice/sms/{lineId}/{month}")]
        public HttpResponseMessage GetAllSms(int lineId, int month)
        {
            var smsList = DAL.GetAllSms(lineId, month);
            if (smsList == null)
            {
                var message = string.Format("sms for line id = {0} not found", lineId);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, smsList);
            }
        }

        [Route("api/invoice/calls/{lineId}/{month}")]
        public IEnumerable<Call> GetCalls(int lineId, int month)
        {
            try
            {
                return DAL.GetCalls(lineId, month);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("api/invoice/clientType/{lineId}")]
        public ClientType GetClientType(int lineId)
        {
            try
            {
                return DAL.GetClientType(lineId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
