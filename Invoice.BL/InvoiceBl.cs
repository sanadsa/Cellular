using Common;
using Invoice.Common;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using Log;
using System.Collections.Generic;

namespace Invoice.BL
{
    public class InvoiceBl : IInvoiceManager
    {
        string url = "http://localhost:11248/";
        LogWriter log = new LogWriter();

        /// <summary>
        /// Calculate the minutes that left for line in the package by month
        /// gets all calls in the selected month and divide each call duration from the minutes in package
        /// </summary>
        /// <param name="lineId">the line to check minutes left in package</param>
        /// <param name="month">the month to check minutes left in package</param>
        /// <returns>the minutes left in package, if the return is minus so its the minutes out of package</returns>
        public double GetMinutesLeft(int lineId, DateTime month)
        {
            double minLeft = 0;
            Package package;
            List<Call> calls;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);

                    var callResult = client.GetAsync("api/invoice/calls/"+lineId+"/"+month.Month).Result;
                    if (callResult.IsSuccessStatusCode)
                    {
                        string response = callResult.Content.ReadAsStringAsync().Result;
                        calls = JsonConvert.DeserializeObject<List<Call>>(response);
                    }
                    else
                    {
                        throw new Exception("cant get calls");
                    }

                    var result = client.GetAsync("api/invoice/package/" + lineId).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        package = JsonConvert.DeserializeObject<Package>(response);
                    }
                    else
                    {
                        throw new Exception("cant get package");
                    }
                }

                minLeft = package.MaxMinute;
                if (minLeft == null)
                {
                    throw new Exception("package dont contains minutes");
                }
                foreach (var call in calls)
                {
                    minLeft -= call.Duration;
                }

                return minLeft;
            }
            catch (Exception e)
            {
                log.LogWrite("Get minutes left error: " + e.Message);
                throw new Exception("Get minutes left exception: " + e.Message);
            }
        }

        /// <summary>
        /// get payment of line in the chosen month, calculates all calls and sms of the month 
        /// considering the package and client type
        /// </summary>
        /// <returns>the total payment of the month for the line</returns>
        public double GetCallsPayment(int lineId, DateTime month)
        {
            Package package;
            List<Call> calls;
            List<SMS> sms;
            double payment = 0;
            ClientType type;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);

                    var callResult = client.GetAsync("api/invoice/calls/" + lineId + "/" + month.Month).Result;
                    if (callResult.IsSuccessStatusCode)
                    {
                        string response = callResult.Content.ReadAsStringAsync().Result;
                        calls = JsonConvert.DeserializeObject<List<Call>>(response);
                    }
                    else
                    {
                        throw new Exception("cant get calls");
                    }

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

                    result = client.GetAsync("api/invoice/clientType/" + lineId).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        type = JsonConvert.DeserializeObject<ClientType>(response);
                    }
                    else
                    {
                        throw new Exception("cant get client type");
                    }

                    result = client.GetAsync("api/invoice/sms/" + lineId + "/" + month.Month).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        sms = JsonConvert.DeserializeObject<List<SMS>>(response);
                    }
                    else
                    {
                        throw new Exception("sms: " + result.Content.ReadAsStringAsync().Result);
                    }
                }

                if (package != null)
                {
                    double minutes = package.MaxMinute;
                    payment += package.MinutePrice;
                    if (minutes > 0)
                    {
                        foreach (var c in calls)
                        {
                            if (c.Duration < minutes)
                            {
                                minutes -= c.Duration;
                            }
                            else
                            {
                                c.Duration -= minutes;
                                payment += c.Duration * type.MinutePrice;
                            }
                        }
                    }
                }
                else
                {
                    foreach (var c in calls)
                    {
                        payment += c.Duration * type.MinutePrice;
                    }
                }

                foreach (var s in sms)
                {
                    payment += type.SmsPrice;
                }

                return payment;
            }
            catch (Exception e)
            {
                log.LogWrite("Get payment error: " + e.Message);
                throw new Exception("Get payment exception: " + e.Message);
            }
        }

        public Payment AddPayment(int clientID, DateTime month, double totalPayment)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var payment = new Payment(clientID, month, totalPayment);
                    string json = JsonConvert.SerializeObject(payment);
                    var result = client.PostAsync("api/crm/payment", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<Payment>(response);
                    }
                    else
                    {
                        throw new Exception(result.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Add payment error: " + e.Message);
                throw new Exception("Add payment exception: " + e.Message);
            }
        }

        public Call SimulateCall(int lineId, double duration, DateTime month, string destination, eCallTo callTo)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var call = new Call(lineId, duration, month, destination, callTo);
                    string json = JsonConvert.SerializeObject(call);
                    var result = client.PostAsync("api/crm/call", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<Call>(response);
                    }
                    else
                    {
                        throw new Exception(result.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Add call error: " + e.Message);
                throw new Exception("Add call exception: " + e.Message);
            }
        }

        public SMS SimulateSms(int lineId, DateTime month, string destinationNum)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var sms = new SMS(lineId, month, destinationNum);
                    string json = JsonConvert.SerializeObject(sms);
                    var result = client.PostAsync("api/crm/sms", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<SMS>(response);
                    }
                    else
                    {
                        throw new Exception(result.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Add sms error: " + e.Message);
                throw new Exception("Add sms exception: " + e.Message);
            }
        }

    }
}
