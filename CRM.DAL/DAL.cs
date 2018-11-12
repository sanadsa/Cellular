using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Common.Classes;
using CRM.Common.Interfaces;

namespace CRM.DAL
{
    public class DAL : ICRMRepository
    {
        public void AddClient(Client client)
        {
            throw new NotImplementedException();
        }

        public void AddLine(Line line)
        {
            throw new NotImplementedException();
        }

        public void AddPackage(Package package)
        {
            throw new NotImplementedException();
        }

        public void AddServiceAgent(ServiceAgent agent)
        {
            throw new NotImplementedException();
        }

        public void DeleteClient(int clientId)
        {
            throw new NotImplementedException();
        }

        public void DeleteLine(int lineId)
        {
            throw new NotImplementedException();
        }

        public void DeleteServiceAgent(int agentId)
        {
            throw new NotImplementedException();
        }

        public bool IsClientExists(int idNumber, string number)
        {
            throw new NotImplementedException();
        }

        public bool IsServiceAgentExists(string agentName, string password)
        {
            throw new NotImplementedException();
        }

        public void UpdateClient(Client newClient, int clientId)
        {
            throw new NotImplementedException();
        }

        public void UpdateLine(Line newLine, int lineId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePackage(Package newPackage, int packageId)
        {
            throw new NotImplementedException();
        }

        public void UpdateServiceAgent(ServiceAgent newAgent, int agentId)
        {
            throw new NotImplementedException();
        }
    }
}
