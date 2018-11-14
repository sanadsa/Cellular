using System;
using CRM.Common.Interfaces;
using Common;
using DAL;
using System.Data.Entity.Validation;
using Log;
using System.Linq;
using System.Reflection;

namespace CRM.DAL
{
    public class CrmDal : ICrmRepository
    {
        LogWriter log = new LogWriter();

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
                log.LogWrite("Add Agent Dal error: " + e.Message);
                throw new Exception("agent DAL exception: " + e.Message);
            }
        }

        public Client AddClient(Client client)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    context.Clients.Add(client);
                    context.SaveChanges();
                    return client;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Add client Dal error: " + e.Message);
                throw new Exception("Add client DAL exception: " + e.Message);
            }
        }

        public Line AddLine(Line line)
        {
            throw new NotImplementedException();
        }

        public Package AddPackage(Package package)
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

        public ServiceAgent UpdateServiceAgent(ServiceAgent newAgent, int agentId)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var agentInDb = context.ServiceAgents.SingleOrDefault(a => a.ServiceAgentId == agentId);
                    if (agentInDb == null)
                    {
                        throw new Exception("Agent not found");
                    }
                    agentInDb.AgentName = newAgent.AgentName;
                    agentInDb.Password = newAgent.Password;
                    agentInDb.SalesAmount = newAgent.SalesAmount;

                    context.SaveChanges();
                    return newAgent;
                }
            }
            catch (TargetInvocationException e)
            {
                log.LogWrite("Update Agent Dal error: " + e.Message);
                //throw new Exception("Update Agent DAL exception: " + e.Message);
                throw e.InnerException;
            }
        }
    }
}
