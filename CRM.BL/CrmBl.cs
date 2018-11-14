using System;
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
                throw new Exception("Add agent exception: " + e.Message);
            }
        }

        public ServiceAgent UpdateServiceAgent(int agentId, string name, string pass, int salesAmount)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    //client.DefaultRequestHeaders.Accept.Clear();
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var agent = new ServiceAgent(name, pass, salesAmount);
                    string json = JsonConvert.SerializeObject(agent);
                    var result = client.PostAsync("api/crm/agent/"+agentId, new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<ServiceAgent>(response);
                    }
                    else
                    {
                        throw new Exception("update agent not successs");
                        //return null;
                    }
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Update Agent bl error: " + e.Message);
                throw new Exception("Update agent exception: " + e.Message);
            }
        }

        public Client AddClient(string name, string lastName, int idNumber, int clientTypeId, string address, string contactNumber, int callsToCenter)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:11248/");
                    var customer = new Client(name, lastName, idNumber, clientTypeId, address,
                                              contactNumber, callsToCenter);
                    string json = JsonConvert.SerializeObject(customer);
                    var result = client.PostAsync("api/crm/client", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<Client>(response);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Add client exception: " + e.Message);
            }            
        }

        public void DeleteClient(int clientId)
        {
            throw new NotImplementedException();
        }

        public void UpdateClient(int clientId, string name, string lastName, int idNumber, int clientTypeId, string address, string contactNumber, int callsToCenter)
        {
            throw new NotImplementedException();
        }

        public ClientType AddClientType(string typeName, double minutePrice, int smsPrice)
        {
            throw new NotImplementedException();
        }
    }
}
