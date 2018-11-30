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

        /// <summary>
        /// adds line to db, throw exception if client id not found or number exists
        /// </summary>
        [HttpPost]
        [Route("api/crm/line")]
        public HttpResponseMessage CreateLine([FromBody]Line line)
        {
            try
            {
                var l = DAL.AddLine(line);
                return Request.CreateResponse(HttpStatusCode.OK, l);
            }
            catch (ArgumentException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message);
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        /// <summary>
        /// adds package to db
        /// </summary>
        [HttpPost]
        [Route("api/crm/package")]
        public HttpResponseMessage CreatePackage([FromBody]Package package)
        {
            try
            {
                var p = DAL.AddPackage(package);
                return Request.CreateResponse(HttpStatusCode.OK, p);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        /// <summary>
        /// adds most called to db, throw exception if package id not found
        /// </summary>
        [HttpPost]
        [Route("api/crm/mostcalled")]
        public HttpResponseMessage CreateMostCalled([FromBody]MostCalled mostCalled)
        {
            try
            {
                var m = DAL.AddMostCalled(mostCalled);
                return Request.CreateResponse(HttpStatusCode.OK, m);
            }
            catch (ArgumentException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, e.Message);
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

        /// <summary>
        /// get package templates - from dal 
        /// </summary>
        [Route("api/crm/templates")]
        public HttpResponseMessage GetTemplates()
        {
            try
            {
                var templates = DAL.GetTemplates();
                return Request.CreateResponse(HttpStatusCode.OK, templates);
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

        /// <summary>
        /// get package by line id - from dal
        /// </summary>
        [Route("api/crm/package/{lineId}")]
        public HttpResponseMessage GetPackage(int lineId)
        {
            try
            {
                var package = DAL.GetPackage(lineId);
                return Request.CreateResponse(HttpStatusCode.OK, package);
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

        /// <summary>
        /// get most called numbers by package id - from dal
        /// </summary>
        [Route("api/crm/mostcalled/{packageId}")]
        public HttpResponseMessage GetMostCalledNums(int packageId)
        {
            try
            {
                var numbers = DAL.GetMostCalledNums(packageId);
                return Request.CreateResponse(HttpStatusCode.OK, numbers);
            }
            catch (KeyNotFoundException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message);
            }
        }

        /// <summary>
        /// get client types from dal
        /// </summary>
        /// <returns>http response message with status code and client types</returns>
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
