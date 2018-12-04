using Common;
using CRM.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    public class CrmDalBl
    {
        private readonly ICrmRepository DAL;
        public CrmDalBl(ICrmRepository repository)
        {
            DAL = repository;
        }

        public ServiceAgent AddAgent(ServiceAgent agent)
        {
            return DAL.AddServiceAgent(agent);
        }
    }
}
