using Invoice.Common;
using Optimal.Common;
using Optimal.DAL;
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
        private readonly IOptimalRepository optimalRepository;

        public OptimalController()
        {
            this.optimalRepository = new OptimalDal();
        }
               
        /// <summary>
        /// get client by id and number, if clinet dont exists return statuc not found
        /// </summary>
        [Route("api/optimal/client/{clientId}/{contactNumber}")]
        public HttpResponseMessage GetClientLogin(int clientId, string contactNumber)
        {
            try
            {
                var client = optimalRepository.GetClient(clientId, contactNumber);
                return Request.CreateResponse(HttpStatusCode.OK, client);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message);
            }
        }

        [Route("api/optimal/lines/{clientId}")]
        public HttpResponseMessage GetLinesAmount(int clientId)
        {
            try
            {
                var linesAmount = optimalRepository.GetNumberOfLines(clientId);
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
                var recieptsSum = optimalRepository.GetRecieptsSum(clientId);
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
                var callsToCenter = optimalRepository.GetCallsToCenter(clientId);
                return Request.CreateResponse(HttpStatusCode.OK, callsToCenter);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message);
            }
        }
    }
}
