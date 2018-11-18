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

        /// <summary>
        /// add line to db - if line number exists in db throw exception
        /// if client not exists in db throw exception
        /// </summary>
        /// <returns>line to know the line added successfully</returns>
        public Line AddLine(Line line)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var lineNumber = context.Lines.SingleOrDefault(l => l.Number == line.Number);
                    var clientFromDb = context.Clients.SingleOrDefault(c => c.ClientID == line.ClientId);
                    if (clientFromDb == null)
                    {
                        throw new Exception("Client not exits, choose another client id");
                    }
                    if (lineNumber != null)
                    {
                        throw new Exception("Number associated to other line, choose another number");
                    }
                    context.Lines.Add(line);
                    context.SaveChanges();
                    return line;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Add line Dal error: " + e.Message);
                throw new Exception("Add line exception: " + e.Message);
            }
        }

        /// <summary>
        /// add packcage to db - if line doesnot exist in db throw exception
        /// </summary>
        /// <param name="package">package to add</param>
        /// <returns>the package that added</returns>
        public Package AddPackage(Package package)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var lineFromDb = context.Lines.SingleOrDefault(l => l.LineId == package.LineId);
                    if (lineFromDb == null)
                    {
                        throw new Exception("Line not exits, choose another line id");
                    }
                    context.Packages.Add(package);
                    context.SaveChanges();
                    return package;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Add package Dal error: " + e.Message);
                throw new Exception("Add package exception: " + e.Message);
            }
        }

        // add Enurable<Line> in client class to delete all lines of the client 
        /// <summary>
        /// Delete client from db by client id
        /// </summary>
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
                log.LogWrite("Delete client Dal error: " + e.Message);
                throw new Exception("Delete client exception: " + e.Message);
            }
        }

        // add Enurable<Package> in line class to delete all packages of the line
        /// <summary>
        /// Delete line from db by line id
        /// </summary>
        public void DeleteLine(int lineId)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    //var line = context.Lines.Include(l => l.packages).Single(l => l.LineId == lineId);
                    //context.Packages.RemoveRange(line.packages);
                    var line = context.Lines.Find(lineId);
                    context.Lines.Remove(line);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Delete line Dal error: " + e.Message);
                throw new Exception("Delete line exception: " + e.Message);
            }
        }   
           
        // check what happens if update contactNum/Idnum to an existing one
        /// <summary>
        /// Update existing client in db by client id
        /// </summary>
        /// <param name="newClient">client with updated fields</param>
        /// <returns></returns>
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
                    clientInDb.ContactNumber = newClient.ContactNumber;
                    clientInDb.IdNumber = newClient.IdNumber;
                    clientInDb.LastName = newClient.LastName;
                    clientInDb.CallsToCenter = newClient.CallsToCenter;
                    
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

        /// <summary>
        /// Update existing line in db by line id
        /// </summary>
        /// <param name="newLine">line with updated fields</param>
        /// <returns></returns>
        public Line UpdateLine(int lineId, eStatus status)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var lineInDb = context.Lines.SingleOrDefault(l => l.LineId == lineId);
                    if (lineInDb == null)
                    {
                        throw new Exception("Line not found");
                    }
                    lineInDb.Status = status;

                    context.SaveChanges();
                    return lineInDb;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Update line error: " + e.Message);
                throw new Exception("Update line exception: " + e.Message);
            }
        }

        /// <summary>
        /// Update existing package in db by package id
        /// </summary>
        /// <param name="newPackage">package with updated fields</param>
        /// <returns></returns>
        public Package UpdatePackage(Package newPackage, int packageId)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var packInDb = context.Packages.SingleOrDefault(p => p.PackageId == packageId);
                    if (packInDb == null)
                    {
                        throw new Exception("Package not found");
                    }
                    packInDb.DiscountPercentage = newPackage.DiscountPercentage;
                    packInDb.FamilyDiscount = newPackage.FamilyDiscount;
                    packInDb.FavoriteNumId = newPackage.FavoriteNumId;
                    packInDb.MaxMinute = newPackage.MaxMinute;
                    packInDb.MinutePrice = newPackage.MinutePrice;
                    packInDb.Month = newPackage.Month;
                    packInDb.MostCalled = newPackage.MostCalled;
                    packInDb.MostCalledNums = newPackage.MostCalledNums;
                    packInDb.TotalPrice = newPackage.TotalPrice;

                    context.SaveChanges();
                    return newPackage;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Update package error: " + e.Message);
                throw new Exception("Update package exception: " + e.Message);
            }
        }

        /// <summary>
        /// Update existing agent in db by agent id
        /// </summary>
        /// <param name="newAgent">agent with updated password and sales</param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Login by checking if agent exists in db and password correct
        /// </summary>
        public ServiceAgent Login(string name, string password)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var agentFromDb = context.ServiceAgents.SqlQuery("Select * from ServiceAgents where AgentName=@Name && Password=@Pass", new SqlParameter("@Name", name), new SqlParameter("@Pass", password))
                    .FirstOrDefault();
                    
                    if (agentFromDb == null)
                    {
                        throw new Exception("User not exists, check username and password");
                    }
                    else
                    {
                        return agentFromDb;
                    }
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Get agent error: " + e.Message);
                throw new Exception("Get agent exception: " + e.Message);
            }
        }       
    }
}
