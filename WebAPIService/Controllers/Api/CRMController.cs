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
        private CrmDal crmDal;
        private readonly ICrmRepository DAL;

        public CRMController()
        {
            crmDal = new CrmDal();
            DAL = new CrmDal();
        }
        
        [HttpPost]
        [Route("api/crm/agent")]
        public ServiceAgent CreateServiceAgent([FromBody]ServiceAgent agent)
        {
            return DAL.AddServiceAgent(agent);
            //return crmDal.AddServiceAgent(agent);
        }

        [HttpPost]
        [Route("api/crm/client")]
        public Client CreateClient([FromBody]Client client)
        {
            return crmDal.AddClient(client);
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
            crmDal.UpdateServiceAgent(agent, agentId);
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
