using Common;
using CRM.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIService.Controllers.Api
{
    public class CRMController : ApiController
    {
        private CrmDal crmDal;

        public CRMController()
        {
            crmDal = new CrmDal();
        }
        
        [HttpPost]
        [Route("api/crm/agent")]
        public ServiceAgent CreateServiceAgent([FromBody]ServiceAgent agent)
        {
            return crmDal.AddServiceAgent(agent);
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
    }
}
