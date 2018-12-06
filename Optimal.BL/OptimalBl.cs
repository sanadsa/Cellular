using Common;
using Log;
using Newtonsoft.Json;
using Optimal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Optimal.BL
{
    /// <summary>
    /// Class that applies the optimal system, implements the interface IOptimalManager and calls methods by http call
    /// </summary>
    public class OptimalBl : IOptimalManager
    {
        private string url = "http://localhost:11248/";
        private LogWriter log = new LogWriter();

        /// <summary>
        /// get client for login
        /// </summary>
        public Client GetClient(int clientId, string contactNumber)
        {
            Client clientFromDb;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = client.GetAsync("api/optimal/client/" + clientId + "/" + contactNumber).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        clientFromDb = JsonConvert.DeserializeObject<Client>(response);
                    }
                    else
                    {
                        return null;
                    }
                }

                return clientFromDb;
            }
            catch (Exception e)
            {
                log.LogWrite("Get client error: " + e.Message);
                throw new Exception("cant get client: " + e.Message);
            }
        }

        /// <summary>
        /// get client value by client id - calculated by numberOfLines, receiptSum and callsToCenter
        /// </summary>
        public double GetClientValue(int clientId)
        {
            double clientValue = 0;
            int numberOfLines = 0;
            double recieptsSum = 0;
            int callsToCenter = 0;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);

                    var result = client.GetAsync("api/optimal/lines/" + clientId).Result;
                    string response = result.Content.ReadAsStringAsync().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        numberOfLines = JsonConvert.DeserializeObject<int>(response);
                    }
                    else
                    {
                        throw new Exception(response);
                    }

                    result = client.GetAsync("api/optimal/reciepts" + clientId).Result;
                    response = result.Content.ReadAsStringAsync().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        recieptsSum = JsonConvert.DeserializeObject<double>(response);
                    }
                    else
                    {
                        throw new Exception(response);
                    }

                    result = client.GetAsync("api/optimal/callsToCenter" + clientId).Result;
                    response = result.Content.ReadAsStringAsync().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        callsToCenter = JsonConvert.DeserializeObject<int>(response);
                    }
                    else
                    {
                        throw new Exception(response);
                    }
                }

                clientValue = numberOfLines * 0.2 > 4 ? 4 : numberOfLines * 0.2;
                clientValue += recieptsSum / 1000 > 6 ? 6 : recieptsSum / 1000;
                clientValue += callsToCenter * -0.1 < -3 ? -3 : callsToCenter * -0.1;

                return clientValue;
            }
            catch (Exception e)
            {
                throw new Exception("Get client value exception: " + e.Message);
            }
        }
    }
}
