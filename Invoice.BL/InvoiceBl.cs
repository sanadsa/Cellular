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

        public Payment AddPayment(int clientID, DateTime month, double totalPayment)
        {
            throw new NotImplementedException();
        }

        public Call SimulateCall(int lineId, double duration, DateTime month, string destination)
        {
            throw new NotImplementedException();
        }

        public SMS SimulateSms(int lineId, DateTime month, string destinationNum)
        {
            throw new NotImplementedException();
        }
    }
}
