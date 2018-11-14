using System;
using System.Net.Http;
using Common;
using CRM.Common.Interfaces;
using Newtonsoft.Json;

namespace CRM.BL
{
    public class CrmBl : ICrmManager
    {
        public void AddClient(Client client)
        {
            throw new NotImplementedException();
        }

        public ServiceAgent AddServiceAgent(string agentName, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:11248/");
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var agent = new ServiceAgent(agentName, password);
                string json = JsonConvert.SerializeObject(agent);
                var result = client.PostAsync("api/Crm", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

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
    }
}
