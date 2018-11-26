using System;
using System.Collections.Generic;
using System.Net.Http;
using Common;
using CRM.Common.Interfaces;
using Log;
using Newtonsoft.Json;

namespace CRM.BL
{
    public class CrmBl : ICrmManager
    {
        private string url = "http://localhost:11248/";
        private LogWriter log = new LogWriter();

        /// <summary>
        /// adds agent to db- gets the agent params from ui and calls the server using http
        /// </summary>
        /// <returns>if succeeded return the agent, if not throws exception</returns>
        public ServiceAgent AddServiceAgent(string agentName, string password)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    //client.DefaultRequestHeaders.Accept.Clear();
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var agent = new ServiceAgent(agentName, password);
                    string json = JsonConvert.SerializeObject(agent);
                    var result = client.PostAsync("api/crm/agent", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<ServiceAgent>(response);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Add agent error: " + e.Message);
                throw new Exception("Add agent exception: " + e.Message);
            }
        }
   
        /// <summary>
        /// gets the client params from ui and calls the add client in server using http
        /// </summary>
        /// <returns>if succeeded return the client, if not throws exception</returns>
        public Client AddClient(string name, string lastName, int idNumber, int clientTypeId, string address, string contactNumber)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var customer = new Client(name, lastName, idNumber, clientTypeId, address,
                                              contactNumber, 0);
                    string json = JsonConvert.SerializeObject(customer);
                    var result = client.PostAsync("api/crm/client", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<Client>(response);
                    }
                    else
                    {
                        throw new Exception(result.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Add client error: " + e.Message);
                throw new Exception("Add client exception: " + e.Message);
            }            
        }

        /// <summary>
        /// gets the line params from ui and calls the add line in server using http
        /// </summary>
        /// <returns>if succeeded return the line, if not throws exception</returns>
        public Line AddLine(int clientId, string number, eStatus status)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var line = new Line(clientId, number, status);
                    string json = JsonConvert.SerializeObject(line);
                    var result = client.PostAsync("api/crm/line", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<Line>(response);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Add line error: " + e.Message);
                throw new Exception("Add line exception: " + e.Message);
            }
        }

        /// <summary>
        /// gets the package params from ui and calls the add package in server using http
        /// </summary>
        /// <returns>if succeeded return the package, if not throws exception</returns>
        public Package AddPackage(string name, int lineId, double price, DateTime month, int maxMinute, int minutePrice, double discount, int favoritNumId, bool mostCalled, bool famDis)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var package = new Package(name, lineId, price, month, maxMinute, minutePrice, discount, favoritNumId, mostCalled, famDis);
                    string json = JsonConvert.SerializeObject(package);
                    var result = client.PostAsync("api/crm/package", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<Package>(response);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Add package error: " + e.Message);
                throw new Exception("Add package exception: " + e.Message);
            }
        }
        
        /// <summary>
        /// update agent fields - gets the agent params from ui and calls the server using http
        /// </summary>
        /// <returns>if succeeded return the agent, if not throws exception</returns>
        public ServiceAgent UpdateServiceAgent(int agentId, string name, string pass, int salesAmount)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var agent = new ServiceAgent(name, pass, salesAmount);
                    string json = JsonConvert.SerializeObject(agent);
                    var result = client.PutAsync("api/crm/agent/"+agentId, new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<ServiceAgent>(response);
                    }
                    else
                    {
                        throw new Exception("update agent not successs");
                    }
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Update Agent bl error: " + e.Message);
                throw new Exception("Update agent exception: " + e.Message);
            }
        }

        /// <summary>
        /// update line status fields - gets status from ui and calls the server using http
        /// </summary>
        /// <returns>if succeeded return the line, if not throws exception</returns>
        public Line UpdateLineStatus(int lineId, eStatus status)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var line = lineId;
                    string json = JsonConvert.SerializeObject(line);
                    var result = client.PutAsync("api/crm/line/" + status, new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<Line>(response);
                    }
                    else
                    {
                        throw new Exception("update line status not successs");
                    }
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Update line bl error: " + e.Message);
                throw new Exception("Update line exception: " + e.Message);
            }
        }

        /// <summary>
        /// update client fields - gets the updated client params from ui and calls the server using http
        /// </summary>
        /// <returns>if succeeded return the client, if not throws exception</returns>
        public Client UpdateClient(int clientId, string name, string lastName, int idNumber, int clientTypeId, string address, string contactNumber, int callsToCenter)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var customer = new Client(name, lastName, idNumber, clientTypeId, address, contactNumber, callsToCenter);
                    string json = JsonConvert.SerializeObject(customer);
                    var result = client.PutAsync("api/crm/client/" + clientId, new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<Client>(response);
                    }
                    else
                    {
                        throw new Exception("update client not successs");
                    }
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Update client bl error: " + e.Message);
                throw new Exception("Update client exception: " + e.Message);
            }
        }

        /// <summary>
        /// update package fields - gets the updated package params from ui and calls the server using http
        /// </summary>
        /// <returns>if succeeded return the package, if not throws exception</returns>
        public Package UpdatePackage(int packageId, string name, int lineId, double price, DateTime month, int maxMinute, int minutePrice, double discount, int favoritNumId, bool mostCalled, bool famDis)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var package = new Package(name, lineId, price, month, maxMinute, minutePrice, discount, favoritNumId, mostCalled, famDis);
                    string json = JsonConvert.SerializeObject(package);
                    var result = client.PutAsync("api/crm/package/" + packageId, new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<Package>(response);
                    }
                    else
                    {
                        throw new Exception("update package not successs");
                    }
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Update package bl error: " + e.Message);
                throw new Exception("Update package exception: " + e.Message);
            }
        }

        /// <summary>
        /// Calls the serve using http and delete the client by its id
        /// </summary>
        public void DeleteClient(int clientId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = client.DeleteAsync("api/crm/delete/" + clientId).Result;
                    if (!result.IsSuccessStatusCode)
                    {
                        throw new Exception("Failed to delete client " + clientId);
                    }
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Delete client error: " + e.Message);
                throw new Exception("Delete client exception: " + e.Message);
            }
        }

        /// <summary>
        /// Calls the server using http and delete the client by its id
        /// </summary>
        public void DeleteLine(int lineId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = client.DeleteAsync("api/crm/line/d/" + lineId);
                    if (!result.IsCompleted)
                    {
                        throw new Exception("Failed to delete line " + lineId);
                    }
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Delete line error: " + e.Message);
                throw new Exception("Delete line exception: " + e.Message);
            }
        }

        /// <summary>
        /// Calls the server using http to check if the agent exists to login
        /// gets name and password from ui
        /// </summary>
        public ServiceAgent LoginAgent(string name, string password)
        {
            ServiceAgent agent;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = client.GetAsync("api/crm/agent/" + name + "/" + password).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        agent = JsonConvert.DeserializeObject<ServiceAgent>(response);
                    }
                    else
                    {
                        throw new Exception(result.Content.ReadAsStringAsync().Result);
                    }
                }

                return agent;
            }
            catch (Exception e)
            {
                log.LogWrite("Get agent error: " + e.Message);
                throw new Exception("Get agent exception: " + e.Message);
            }
        }

        public List<Client> GetClients()
        {
            List<Client> customers;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);

                    var result = client.GetAsync("api/crm/clients").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        customers = JsonConvert.DeserializeObject<List<Client>>(response);
                    }
                    else
                    {
                        throw new Exception(result.Content.ReadAsStringAsync().Result);
                    }
                }

                return customers;
            }
            catch (Exception e)
            {
                log.LogWrite("Get clients error: " + e.Message);
                throw new Exception("Get clients exception: " + e.Message);
            }
        }

        public List<ClientType> GetClientTypes()
        {
            List<ClientType> types;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);

                    var result = client.GetAsync("api/crm/types").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        types = JsonConvert.DeserializeObject<List<ClientType>>(response);
                    }
                    else
                    {
                        throw new Exception(result.Content.ReadAsStringAsync().Result);
                    }
                }

                return types;
            }
            catch (Exception e)
            {
                log.LogWrite("Get client types error: " + e.Message);
                throw new Exception("Get client types exception: " + e.Message);
            }
        }

        /// <summary>
        /// Get all lines of client id
        /// </summary>
        public List<Line> GetClientLines(int clientId)
        {
            List<Line> lines;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);

                    var result = client.GetAsync("api/crm/lines/"+clientId).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        lines = JsonConvert.DeserializeObject<List<Line>>(response);
                    }
                    else
                    {
                        throw new Exception(result.Content.ReadAsStringAsync().Result);
                    }
                }

                return lines;
            }
            catch (Exception e)
            {
                log.LogWrite("Get lines error: " + e.Message);
                throw new Exception("Get lines exception: " + e.Message);
            }
        }
    }
}
