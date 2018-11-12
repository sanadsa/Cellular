using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Common.Classes;

namespace CRM.Common.Interfaces
{
    /// <summary>
    /// interface for the CRM, implemented by the BL
    /// </summary>
    public interface ICRMRepository
    {
        // client
        void AddClient(Client client);
        void UpdateClient(Client newClient, int clientId);
        void DeleteClient(int clientId);
        bool IsClientExists(int idNumber, string number);
        // service agent
        void AddServiceAgent(ServiceAgent agent);
        void UpdateServiceAgent(ServiceAgent newAgent, int agentId);
        void DeleteServiceAgent(int agentId);
        bool IsServiceAgentExists(string agentName, string password);
        // line
        void AddLine(Line line);
        void UpdateLine(Line newLine, int lineId);
        void DeleteLine(int lineId);
        // package
        void AddPackage(Package package);
        void UpdatePackage(Package newPackage, int packageId);
    }
}
