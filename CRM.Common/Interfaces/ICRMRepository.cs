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
        Client UpdateClient(Client newClient, int clientId);
        void DeleteClient(int clientId);
        List<Client> GetClients();
        List<ClientType> GetClientTypes();
        // service agent
        ServiceAgent AddServiceAgent(ServiceAgent agent);
        ServiceAgent UpdateServiceAgent(ServiceAgent newAgent, int agentId);
        ServiceAgent Login(string name, string password);
        // line
        Line AddLine(Line line);
        //Line UpdateLine(Line newLine, int lineId);
        Line UpdateLine(int lineId, eStatus status);
        void DeleteLine(int lineId);
        List<Line> GetLines(int clientId);
        // package
        Package AddPackage(Package package);
        Package UpdatePackage(Package newPackage, int packageId);
    }
}
