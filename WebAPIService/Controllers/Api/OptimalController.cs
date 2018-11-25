using Optimal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIService.Controllers.Api
{
    public class OptimalController : ApiController
    {
        private readonly IOptimalRepository DAL;

        [Route("api/optimal/lines/{clientId}")]
        public HttpResponseMessage GetLinesAmount(int clientId)
        {
            try
            {
                var linesAmount = DAL.GetNumberOfLines(clientId);
                return Request.CreateResponse(HttpStatusCode.OK, linesAmount);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message);
            }
        }

        [Route("api/optimal/reciepts/{clientId}")]
        public HttpResponseMessage GetRecieptsSum(int clientId)
        {
            try
            {
                var recieptsSum = DAL.GetRecieptsSum(clientId);
                return Request.CreateResponse(HttpStatusCode.OK, recieptsSum);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message);
            }
        }

        [Route("api/optimal/callsToCenter/{clientId}")]
        public HttpResponseMessage GetCallsToCenter(int clientId)
        {
            try
            {
                var callsToCenter = DAL.GetCallsToCenter(clientId);
                return Request.CreateResponse(HttpStatusCode.OK, callsToCenter);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message);
            }
        }
    }
}
