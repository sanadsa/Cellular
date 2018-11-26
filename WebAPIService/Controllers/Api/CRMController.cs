using Common;
using CRM.Common.Interfaces;
using CRM.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace WebAPIService.Controllers.Api
{
    public class CRMController : ApiController
    {
        private readonly ICrmRepository DAL;

        public CRMController()
        {
            DAL = new CrmDal();
        }
        
        [HttpPost]
        [Route("api/crm/agent")]
        public ServiceAgent CreateServiceAgent([FromBody]ServiceAgent agent)
        {
            try
            {
                return DAL.AddServiceAgent(agent);
                //return crmDal.AddServiceAgent(agent);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("api/crm/client")]
        public HttpResponseMessage CreateClient([FromBody]Client client)
        {
            try
            {
                var clientResult = DAL.AddClient(client);
                return Request.CreateResponse(HttpStatusCode.OK, clientResult);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message);
            }
        }

        [HttpPost]
        [Route("api/crm/line")]
        public Line CreateLine([FromBody]Line line)
        {
            try
            {
                return DAL.AddLine(line);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("api/crm/package")]
        public Package CreatePackage([FromBody]Package package)
        {
            try
            {
                return DAL.AddPackage(package);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("api/crm/agent/{agentId}")]
        public void UpdateServiceAgent([FromBody]ServiceAgent agent, int agentId)
        {
            // check if valid object
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            DAL.UpdateServiceAgent(agent, agentId);
        }

        [HttpPut]
        [Route("api/crm/line/{status}")]
        public void UpdateLineStatus([FromBody]int lineId, eStatus status)
        {
            // check if valid object
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            DAL.UpdateLine(lineId, status);
        }

        [HttpPut]
        [Route("api/crm/client/{clientId}")]
        public void UpdateClient([FromBody]Client client, int clientId)
        {
            // check if valid object
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            DAL.UpdateClient(client, clientId);
        }

        [HttpPut]
        [Route("api/crm/package/{packageId}")]
        public void UpdatePackage([FromBody]Package package, int packageId)
        {
            // check if valid object
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            DAL.UpdatePackage(package, packageId);
        }

        [HttpDelete]
        [Route("api/crm/delete/{clientId}")]
        public void DeleteClient(int clientId)
        {
            try
            {
                DAL.DeleteClient(clientId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete]
        [Route("api/crm/line/d/{lineId}")]
        public void DeleteLine(int lineId)
        {
            try
            {
                DAL.DeleteLine(lineId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("api/crm/clients")]
        public HttpResponseMessage GetClients()
        {
            try
            {
                var clients = DAL.GetClients();
                return Request.CreateResponse(HttpStatusCode.OK, clients);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message);
            }
        }

        /// <summary>
        /// get lines of the client - from dal 
        /// </summary>
        [Route("api/crm/lines/{clientId}")]
        public HttpResponseMessage GetLines(int clientId)
        {
            try
            {
                var lines = DAL.GetLines(clientId);
                return Request.CreateResponse(HttpStatusCode.OK, lines);
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message);
            }
        }

        [Route("api/crm/types")]
        public HttpResponseMessage GetClientTypes()
        {
            try
            {
                var clients = DAL.GetClientTypes();
                return Request.CreateResponse(HttpStatusCode.OK, clients);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message);
            }
        }

        [Route("api/crm/agent/{agentName}/{password}")]
        public IHttpActionResult Login(string agentName, string password)
        {
            ServiceAgent connectedAgent;
            try
            {
                connectedAgent = DAL.Login(agentName, password);
            }
            catch (Exception e)
            {
                return new ExceptionResult(e, this);
            }
            if (connectedAgent != null)
            {
                return Ok(connectedAgent);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
