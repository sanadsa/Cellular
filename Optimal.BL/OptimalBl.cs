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
    public class OptimalBl : IOptimalManager
    {
        string url = "http://localhost:11248/";

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
