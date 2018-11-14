using System;
using CRM.Common.Interfaces;
using Common;
using DAL;
using System.Data.Entity.Validation;

namespace CRM.DAL
{
    public class CRMDAL : ICRMRepository
    {
        public ServiceAgent AddServiceAgent(ServiceAgent agent)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    context.ServiceAgents.Add(agent);
                    context.SaveChanges();
                    return agent;
                }
            }
            catch (Exception e)
            {
                throw new Exception("DAL exception: " + e.Message);
            }
        }

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
