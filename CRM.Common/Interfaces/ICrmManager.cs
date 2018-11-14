using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Common.Interfaces
{
    public interface ICrmManager
    {
        ServiceAgent AddServiceAgent(string agentName, string password);
        void AddClient(Client client);
    }
}
