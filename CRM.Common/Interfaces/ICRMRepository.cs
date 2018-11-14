using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace CRM.Common.Interfaces
{
    /// <summary>
    /// interface for the CRM, implemented by the BL
    /// </summary>
    public interface ICrmRepository
    {
        // client
        Client AddClient(Client client);
        void UpdateClient(Client newClient, int clientId);
        void DeleteClient(int clientId);
        bool IsClientExists(int idNumber, string number);
        // service agent
        ServiceAgent AddServiceAgent(ServiceAgent agent);
        ServiceAgent UpdateServiceAgent(ServiceAgent newAgent, int agentId);
        void DeleteServiceAgent(int agentId);
        bool IsServiceAgentExists(string agentName, string password);
        // line
        Line AddLine(Line line);
        void UpdateLine(Line newLine, int lineId);
        void DeleteLine(int lineId);
        // package
        Package AddPackage(Package package);
        void UpdatePackage(Package newPackage, int packageId);
    }
}
