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
    /// <summary>
    /// controller that calls the dal of the optimal system
    /// </summary>
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

        /// <summary>
        /// get number of lines that the client have
        /// </summary>
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

        /// <summary>
        /// get receipt sum of the client
        /// </summary>
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

        /// <summary>
        /// get the number of calls that the client did to the center
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// gets list of most calling clients to center
        /// </summary>
        [Route("api/optimal/callingToCenter")]
        public HttpResponseMessage GetMostCallingToCenter()
        {
            try
            {
                var mostCalling = optimalRepository.GetMostCalling();
                return Request.CreateResponse(HttpStatusCode.OK, mostCalling);
            }
            catch (KeyNotFoundException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        /// <summary>
        /// gets list of best agents - the ones who sold highest amount of lines 
        /// calling the optimal repository
        /// </summary>
        [Route("api/optimal/bestSellers")]
        public HttpResponseMessage GetBestSellers()
        {
            try
            {
                var bestSellers = optimalRepository.GetBestSellers();
                return Request.CreateResponse(HttpStatusCode.OK, bestSellers);
            }
            catch (KeyNotFoundException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
