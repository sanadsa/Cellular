using Common;
using Invoice.Common;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using Log;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;

namespace Invoice.BL
{
    /// <summary>
    /// Class that applies the invoice system, implements the interface IInvoiceManager and calls methods by http call
    /// </summary>
    public class InvoiceBl : IInvoiceManager
    {
        string url = "http://localhost:11248/";
        LogWriter log = new LogWriter();

        /// <summary>
        /// Get receipt for line by month
        /// </summary>
        public Receipt GetReceipt(int lineId, DateTime month)
        {
            Receipt receipt;
            Package package;
            Line line;
            ClientType type;
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

                    result = client.GetAsync("api/crm/line/" + lineId).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string response = result.Content.ReadAsStringAsync().Result;
                        line = JsonConvert.DeserializeObject<Line>(response);
                    }
                    else
                    {
                        throw new Exception("cant get line");
                    }
                }

                receipt = new Receipt();
                var payment = GetCallsPayment(lineId, month);
                double minsLeft = 0;
                if (package != null)
                {
                    minsLeft = GetMinutesLeft(lineId, month);
                    receipt.MinutesLeft = minsLeft >= 0 ? minsLeft : 0;
                    receipt.PackageMinutes = package.MaxMinute;
                    receipt.PackagePrice = package.TotalPrice;
                    receipt.PackageUsage = receipt.MinutesLeft > 0 ? ((package.MaxMinute - receipt.MinutesLeft) / package.MaxMinute) * 100 : 0;
                    receipt.TotalPrice = payment + package.TotalPrice - package.MinutePrice;
                }
                else
                {
                    minsLeft = 0;
                    receipt.PackageMinutes = 0;
                    receipt.PackagePrice = 0;
                    receipt.PackageUsage = 0;
                    receipt.MinutesLeft = 0;
                    receipt.TotalPrice = payment;
                }
                receipt.LineNumber = line.Number;
                receipt.PricePerMinute = type.MinutePrice;

                if (minsLeft < 0)
                {
                    receipt.MinutesOutOfPackage = minsLeft * -1;
                    receipt.Extra = payment - package.TotalPrice;
                }
                else
                {
                    receipt.MinutesOutOfPackage = 0;
                    receipt.Extra = 0;
                }

                return receipt;
            }
            catch (Exception e)
            {
                log.LogWrite("Get Receipt error: " + e.Message);
                throw new Exception("Get Receipt Exception: " + e.Message);
            }
        }

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
                        throw new Exception("no package for line " + lineId);
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
                            else if (package.FamilyDiscount && c.CallTo == eCallTo.family)
                            {
                                payment += c.Duration * type.MinutePrice * package.DiscountPercentage;
                            }
                            else if (package.MostCalledNums && c.CallTo == eCallTo.friends)
                            {
                                payment += c.Duration * type.MinutePrice * package.DiscountPercentage;
                            }
                            else if (package.FavoriteNumber && c.CallTo == eCallTo.mostCalled)
                            {
                                payment += 0;
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

        /// <summary>
        /// add payment to client for selected month
        /// </summary>
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

        /// <summary>
        /// add a call to line in selected day 
        /// </summary>
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

        /// <summary>
        /// add sms to line in selected day
        /// </summary>
        public SMS SimulateSms(int lineId, DateTime month, string destinationNum, eCallTo callTo)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var sms = new SMS(lineId, month, destinationNum, callTo);
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
