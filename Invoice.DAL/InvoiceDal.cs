﻿using Common;
using DAL;
using Invoice.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net;
using Log;

namespace Invoice.DAL
{
    /// <summary>
    /// Class that applies the invoice system, implements the interface IInvoiceRepository using entity framework (DBcontext CellularModel)
    /// </summary>
    public class InvoiceDal : IInvoiceRepository
    {
        LogWriter log = new LogWriter();

        /// <summary>
        /// adds call to db
        /// </summary>
        public Call AddCall(Call call)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {

                    var lineFromDb = context.Lines.SingleOrDefault(l => l.LineId == call.LineID);
                    if (lineFromDb == null)
                    {
                        throw new Exception("Line not exits, choose another line id");
                    }
                    context.Calls.Add(call);
                    context.SaveChanges();
                    return call;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Add call Dal error: " + e.Message);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// adds payment to db
        /// </summary>
        public Payment AddPayment(Payment payment)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {

                    var clientFromDb = context.Clients.SingleOrDefault(l => l.ClientID == payment.ClientID);
                    if (clientFromDb == null)
                    {
                        throw new Exception("Client not exits, choose another line id");
                    }
                    context.Payments.Add(payment);
                    context.SaveChanges();
                    return payment;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Add payment Dal error: " + e.Message);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// adds sms to sms table in db
        /// </summary>
        public SMS AddSms(SMS sms)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {

                    var lineFromDb = context.Lines.SingleOrDefault(l => l.LineId == sms.LineID);
                    if (lineFromDb == null)
                    {
                        throw new Exception("Line not exits, choose another line id");
                    }
                    context.SMS.Add(sms);
                    context.SaveChanges();
                    return sms;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Add sms Dal error: " + e.Message);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// gets all sms from sms table in db by lineid and month
        /// </summary>
        /// <returns>list of sms</returns>
        public IEnumerable<SMS> GetAllSms(int lineId, int month)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var sms = context.SMS.Where(c => (c.LineID == lineId && c.Month.Month == month)).ToList();
                    if (sms == null)
                    {
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    }

                    return sms;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Get sms Dal error: " + e.Message);
                throw new Exception("Get sms exception: " + e.Message);
            }
        }

        /// <summary>
        /// gets all calls from Calls table in db by lineid and month
        /// </summary>
        /// <returns>list of call</returns>
        public IEnumerable<Call> GetCalls(int lineId, int month)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var calls = context.Calls.Where(c => (c.LineID == lineId && c.Month.Month == month)).ToList();
                    if (calls == null)
                    {
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    }

                    return calls;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Get calls Dal error: " + e.Message);
                throw new Exception("Get calls exception: " + e.Message);
            }
        }

        /// <summary>
        /// get client type (vip,bussines...) by line id
        /// </summary>
        public ClientType GetClientType(int lineId)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var clientId = context.Lines.SingleOrDefault(l => l.LineId == lineId).ClientId;
                    var clientTypeId = context.Clients.SingleOrDefault(c => c.ClientID == clientId).ClientTypeId;
                    var type = context.ClientTypes.SingleOrDefault(ct => ct.Id == clientTypeId);
                    if (type == null)
                    {
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    }

                    return type;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Get client type Dal error: " + e.Message);
                throw new Exception("Get client type exception: " + e.Message);
            }
        }

        /// <summary>
        /// get package from db by line id
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
        public Package GetPackage(int lineId)
        {
            using (CellularModel context = new CellularModel())
            {
                var package = context.Packages.SingleOrDefault(p => p.LineId == lineId);

                return package;
            }
            //try
            //{
            //    using (CellularModel context = new CellularModel())
            //    {
            //        var package = context.Packages.SingleOrDefault(p => p.LineId == lineId);
            //        if (package == null)
            //        {
            //            throw new HttpResponseException(HttpStatusCode.NotFound);
            //        }

            //        return package;
            //    }
            //}
            //catch (Exception e)
            //{
            //    log.LogWrite("Get package Dal error: " + e.Message);
            //    //throw new Exception("Get package exception: " + e.Message);
            //    throw new HttpResponseException(HttpStatusCode.NotFound);
            //}
        }
    }
}
