﻿using Common;
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
        /// gets list of most value clients - by their value from the optimal system
        /// </summary>
        public List<MostValueClient> GetMostValue(List<Client> clients)
        {
            List<MostValueClient> mostValueClients = new List<MostValueClient>();
            try
            {
                foreach (var client in clients)
                {
                    mostValueClients.Add(new MostValueClient
                    {
                        Client = client,
                        ClientValue = GetClientValue(client.ClientID)
                    });
                }
                return mostValueClients.OrderBy(m => m.ClientValue).ToList();
            }
            catch (Exception e)
            {
                log.LogWrite("Get most value error: " + e.Message);
                throw new Exception("cant most value client: " + e.Message);
            }
        }

        /// <summary>
        /// gets list of most calling clients to center
        /// </summary>
        public List<Client> GetMostCallingToCenter()
        {
            List<Client> mostCalling;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = client.GetAsync("api/optimal/callingToCenter").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        mostCalling = JsonConvert.DeserializeObject<List<Client>>(response);
                    }
                    else
                    {
                        return null;
                    }
                }

                return mostCalling;
            }
            catch (NullReferenceException e)
            {
                log.LogWrite("null most calling to center error: " + e.Message);
                throw new Exception("null most calling to center: " + e.Message);
            }
            catch (Exception e)
            {
                log.LogWrite("Get most calling to center error: " + e.Message);
                throw new Exception("cant get most calling to center");
            }
        }

        /// <summary>
        /// gets list of best agents - the ones who sold highest amount of lines
        /// </summary>
        public List<ServiceAgent> GetBestSellers()
        {
            List<ServiceAgent> bestSellers;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = client.GetAsync("api/optimal/bestSellers").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        bestSellers = JsonConvert.DeserializeObject<List<ServiceAgent>>(response);
                    }
                    else
                    {
                        return null;
                    }
                }

                return bestSellers;
            }
            catch (NullReferenceException e)
            {
                log.LogWrite("null best sellers: " + e.Message);
                throw new Exception("null best sellers: " + e.Message);
            }
            catch (Exception e)
            {
                log.LogWrite("Get best sellers error: " + e.Message);
                throw new Exception("cant get best sellers: ");
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

                    result = client.GetAsync("api/optimal/reciepts/" + clientId).Result;
                    response = result.Content.ReadAsStringAsync().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        recieptsSum = JsonConvert.DeserializeObject<double>(response);
                    }
                    else
                    {
                        throw new Exception(response);
                    }

                    result = client.GetAsync("api/optimal/callsToCenter/" + clientId).Result;
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

                return clientValue * 10;
            }
            catch (Exception e)
            {
                throw new Exception("Get client value exception: " + e.Message);
            }
        }

        /// <summary>
        /// get all values for optimal package by line
        /// </summary>
        public Recommendation GetOptimalCalc(int lineId)
        {
            Recommendation recommendation;
            List<Call> calls;
            List<SMS> sms;
            Package package;
            List<TemplatePackage> templates;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = client.GetAsync("api/invoice/package/" + lineId).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        package = JsonConvert.DeserializeObject<Package>(response);
                    }
                    else
                    {
                        package = null;
                    }

                    result = client.GetAsync("api/crm/templates").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        templates = JsonConvert.DeserializeObject<List<TemplatePackage>>(response);
                    }
                    else
                    {
                        throw new Exception(result.Content.ReadAsStringAsync().Result);
                    }

                    result = client.GetAsync("api/invoice/calls/" + lineId + "/" + DateTime.Now.Month).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        calls = JsonConvert.DeserializeObject<List<Call>>(response);
                    }
                    else
                    {
                        throw new Exception("cant get calls");
                    }

                    result = client.GetAsync("api/invoice/sms/" + lineId + "/" + DateTime.Now).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        sms = JsonConvert.DeserializeObject<List<SMS>>(response);
                    }
                    else
                    {
                        sms = null;
                    }
                }

                recommendation = new Recommendation
                {
                    TotalMins = 0,
                    TotalSMS = 0,
                    TopMinsTopNum = 0,
                    TopMinsFamily = 0,
                    TopMinsMostCalled = 0
                };

                foreach (var item in calls)
                {
                    recommendation.TotalMins += item.Duration;
                }
                if (sms != null)
                {
                    recommendation.TotalSMS = sms.Capacity;
                }

                if (package != null)
                {
                    foreach (var call in calls)
                    {
                        if (call.CallTo == eCallTo.mostCalled)
                        {
                            recommendation.TopMinsTopNum += call.Duration;
                        }
                        else if (call.CallTo == eCallTo.family)
                        {
                            recommendation.TopMinsFamily += call.Duration;
                        }
                        else if (call.CallTo == eCallTo.friends)
                        {
                            recommendation.TopMinsMostCalled += call.Duration;
                        }
                    }
                }

                if (recommendation.TopMinsFamily > recommendation.TopMinsMostCalled && recommendation.TopMinsFamily > recommendation.TopMinsTopNum)
                {
                    recommendation.FirstRecommendation = templates[2];
                    recommendation.SecondRecommendation = templates[1];
                    recommendation.ThirdRecommendation = templates[0];
                }
                else if (recommendation.TopMinsTopNum > recommendation.TopMinsMostCalled && recommendation.TopMinsTopNum > recommendation.TopMinsFamily)
                {
                    recommendation.FirstRecommendation = templates[0];
                    recommendation.SecondRecommendation = templates[1];
                    recommendation.ThirdRecommendation = templates[2];
                }
                else
                {
                    recommendation.FirstRecommendation = templates[1];
                    recommendation.SecondRecommendation = templates[0];
                    recommendation.ThirdRecommendation = templates[2];
                }

                return recommendation;
            }
            catch (Exception e)
            {
                log.LogWrite("Get Recommendation error: " + e.Message);
                throw new Exception("Get Recommendation Exception: " + e.Message);
            }
        }
    }
}
