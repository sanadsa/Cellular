﻿using System;
using CRM.Common.Interfaces;
using Common;
using DAL;
using Log;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Net.Http;

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
                        throw new KeyNotFoundException("Client not exits, choose another client id");
                    }
                    if (lineNumber != null)
                    {
                        throw new ArgumentException("Number associated to other line, choose another number");
                    }
                    context.Lines.Add(line);
                    context.SaveChanges();
                    return line;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Add line Dal error: " + e.Message);
                throw new Exception(e.Message);
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
                    var lineIdFromPackage = context.Packages.SingleOrDefault(l => l.LineId == package.LineId);
                    if (lineFromDb == null)
                    {
                        throw new Exception("Line not exits, choose another line id");
                    }
                    if (lineIdFromPackage != null)
                    {
                        throw new Exception("Line connected to other package, choose another line id");
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

        /// <summary>
        /// add most called numbers to db - if package not in db throw exception
        /// </summary>
        public MostCalled AddMostCalled(MostCalled mostCalled)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var packageFromDb = context.Packages.SingleOrDefault(p => p.PackageId == mostCalled.PackageId);
                    if (packageFromDb == null)
                    {
                        throw new KeyNotFoundException("package not exits, unable to add most called");
                    }
                    context.MostCalled.Add(mostCalled);
                    context.SaveChanges();
                    return mostCalled;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Add most called Dal error: " + e.Message);
                throw new Exception(e.Message);
            }
        }

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

        /// <summary>
        /// Delete line from db by line id
        /// </summary>
        public void DeleteLine(int lineId)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
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
                    packInDb.TotalPrice = newPackage.TotalPrice;
                    packInDb.Month = newPackage.Month;
                    packInDb.MaxMinute = newPackage.MaxMinute;
                    packInDb.MinutePrice = newPackage.MinutePrice;
                    packInDb.DiscountPercentage = newPackage.DiscountPercentage;
                    packInDb.FavoriteNumber = newPackage.FavoriteNumber;
                    packInDb.MostCalledNums = newPackage.MostCalledNums;
                    packInDb.FamilyDiscount = newPackage.FamilyDiscount;

                    context.SaveChanges();
                    return newPackage;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Update package error: " + e.Message);
                throw new Exception(e.Message);
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
                    var agentFromDb = context.ServiceAgents.SingleOrDefault(a => (a.AgentName == name && a.Password == password));

                    return agentFromDb;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Get agent error: " + e.Message);
                throw new Exception("Get agent exception: " + e.Message);
            }
        }

        /// <summary>
        /// get all client from db
        /// </summary>
        /// <returns>a list of type client</returns>
        public List<Client> GetClients()
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var clients = context.Clients.ToList();
                    if (clients == null)
                    {
                        throw new Exception("no clients found");
                    }

                    return clients;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Get clients Dal error: " + e.Message);
                throw new Exception("Get clients exception: " + e.Message);
            }
        }

        /// <summary>
        /// get client types - vip/regulare..
        /// </summary>
        public List<ClientType> GetClientTypes()
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var types = context.ClientTypes.ToList();
                    if (types == null)
                    {
                        throw new Exception("no types found");
                    }

                    return types;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Get client types Dal error: " + e.Message);
                throw new Exception("Get client types exception: " + e.Message);
            }
        }

        /// <summary>
        /// get all lines of client by its id from DB
        /// </summary>
        public List<Line> GetLines(int clientId)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var lines = context.Lines.Where(l => l.ClientId == clientId).ToList();
                    if (lines == null)
                    {
                        throw new Exception("no lines found");
                    }

                    return lines;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Get lines Dal error: " + e.Message);
                throw new Exception("Get lines exception: " + e.Message);
            }
        }

        /// <summary>
        /// get package from db by line id
        /// </summary>
        public Package GetPackage(int lineId)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var pacakge = context.Packages.SingleOrDefault(p => p.LineId == lineId);
                    if (pacakge == null)
                    {
                        throw new Exception("no package found");
                    }

                    return pacakge;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Get package Dal error: " + e.Message);
                throw new Exception("Get package exception: " + e.Message);
            }
        }

        /// <summary>
        /// get most called numbers from MostCalled Table in db by package id
        /// </summary>
        public MostCalled GetMostCalledNums(int packageId)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var numbers = context.MostCalled.SingleOrDefault(m => m.PackageId == packageId);
                    if (numbers == null)
                    {
                        throw new Exception("no numbers found for package " + packageId);
                    }

                    return numbers;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Get most called numbers Dal error: " + e.Message);
                throw new Exception("Get most called numbers exception: " + e.Message);
            }
        }

        /// <summary>
        /// get package templates from db
        /// </summary>
        public List<TemplatePackage> GetTemplates()
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var templates = context.TemplatePackages.ToList();
                    if (templates == null)
                    {
                        throw new Exception("no templates found");
                    }

                    return templates;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Get templates Dal error: " + e.Message);
                throw new Exception("Get templates exception: " + e.Message);
            }
        }

        /// <summary>
        /// get line by line id
        /// </summary>
        public Line GetLine(int lineId)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var line = context.Lines.SingleOrDefault(l => l.LineId == lineId);
                    if (line == null)
                    {
                        throw new Exception("no line found");
                    }

                    return line;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Get line Dal error: " + e.Message);
                throw new Exception("Get line exception: " + e.Message);
            }
        }
    }
}
