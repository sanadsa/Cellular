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
        ServiceAgent UpdateServiceAgent(int agentId, string name, string pass, int salesAmount);
        Client AddClient(string name, string lastName, int idNumber, int clientTypeId,
                         string address, string contactNumber, int callsToCenter);
        ClientType AddClientType(string typeName, double minutePrice, int smsPrice);
        void UpdateClient(int clientId, string name, string lastName, int idNumber, int clientTypeId,
                         string address, string contactNumber, int callsToCenter);
        void DeleteClient(int clientId);
    }
}
