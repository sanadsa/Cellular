using System;
using CRM.Common.Interfaces;
using Common;
using DAL;
using System.Data.Entity.Validation;
using Log;
using System.Linq;
using System.Reflection;
using System.Data.SqlClient;

namespace CRM.DAL
{
    /// <summary>
    /// Class that applies the crm system, implements the interfaces ICrmRepository using entity framework (DBcontext CellularModel)
    /// </summary>
    public class CrmDal : ICrmRepository
    {
        LogWriter log = new LogWriter();

        /// <summary>
        /// add agent to db - if agent name exists in the db throw exception
        /// </summary>
        /// <returns>agent to know the agent is added</returns>
        public ServiceAgent AddServiceAgent(ServiceAgent agent)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    //var agentFromDb = context.ServiceAgents.SqlQuery("Select * from ServiceAgents where AgentName=@Name", new SqlParameter("@Name", agent.AgentName))
                    //.FirstOrDefault();
                    var agentFromDb = context.ServiceAgents.SingleOrDefault(a => a.AgentName == agent.AgentName);
                    if (agentFromDb != null)
                    {
                        throw new Exception("User already exists, choose another name");
                    }
                    else
                    {
                        context.ServiceAgents.Add(agent);
                        context.SaveChanges();
                    }

                    return agent;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Add Agent Dal error: " + e.Message);
                throw new Exception("Add agent DAL exception: " + e.Message);
            }
        }
       
        /// <summary>
        /// add client to db - if client idnumber or contact number exists in db dont add client
        /// </summary>
        /// <returns>client to know the agent is added</returns>
        public Client AddClient(Client client)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var clientIdNum = context.Clients.SingleOrDefault(c => c.IdNumber == client.IdNumber);
                    var contactNum = context.Clients.SingleOrDefault(c => c.ContactNumber == client.ContactNumber);
                    if (clientIdNum != null || contactNum != null)
                    {
                        throw new Exception("id or contact number exists, change input");
                    }
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

        // add Enurable<Line> in client class to delete all lines of the client 
        public void DeleteClient(int clientId)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    //var client = context.Clients.Include(c => c.Lines).Single(c => c.ClientID == clientId);
                    //context.Lines.RemoveRange(client.Lines);
                    var client = context.Clients.Find(clientId);
                    context.Clients.Remove(client);
                    
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Update Agent Dal error: " + e.Message);
                throw new Exception("Update Agent DAL exception: " + e.Message);
            }
        }

        public void DeleteLine(int lineId)
        {
            throw new NotImplementedException();
        }   
           
        // check what happens if update contactNum/Idnum to an existing one
        public Client UpdateClient(Client newClient, int clientId)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var clientInDb = context.Clients.SingleOrDefault(c => c.ClientID == clientId);
                    if (clientInDb == null)
                    {
                        throw new Exception("Client not found");
                    }
                    clientInDb.Address = newClient.Address;
                    clientInDb.ClientName = newClient.ClientName;
                    clientInDb.ClientTypeId = newClient.ClientTypeId;
                    clientInDb.ContactNumber = clientInDb.ContactNumber;
                    clientInDb.IdNumber = clientInDb.IdNumber;
                    clientInDb.LastName = clientInDb.LastName;
                    
                    context.SaveChanges();
                    return newClient;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Update Client error: " + e.Message);
                throw new Exception("Update Client exception: " + e.Message);
            }
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
            catch (Exception e)
            {
                log.LogWrite("Update Agent Dal error: " + e.Message);
                throw new Exception("Update Agent DAL exception: " + e.Message);
            }
        }

        public void AddCallsToCenter(int clientId)
        {
            throw new NotImplementedException();
        }
    }
}
