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
        private CRMDAL crmDal;

        public CRMController()
        {
            crmDal = new CRMDAL();
        }

        [HttpPost]
        public ServiceAgent CreateServiceAgent([FromBody]ServiceAgent agent)
        {
            return crmDal.AddServiceAgent(agent);
        }
    }
}
