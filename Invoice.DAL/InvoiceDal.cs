using Common;
using DAL;
using Invoice.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using Log;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Metadata.Edm;

namespace Invoice.DAL
{
    public class InvoiceDal : IInvoiceRepository
    {
        LogWriter log = new LogWriter();

        public Call AddCall(Call call)
        {
            throw new NotImplementedException();
        }

        public Payment AddPayment(Payment payment)
        {
            throw new NotImplementedException();
        }

        public SMS AddSms(SMS sms)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Call> GetCalls(int lineId, DateTime month)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var calls = context.Calls.Where(c => (c.LineID == lineId && c.Month.Month == month.Month)).ToList();
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

        public Package GetPackage(int lineId)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var package = context.Packages.SingleOrDefault(p => p.LineId == lineId);
                    if (package == null)
                    {
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    }

                    return package;
                }
            }
            catch (Exception e)
            {
                log.LogWrite("Get package Dal error: " + e.Message);
                throw new Exception("Get package exception: " + e.Message);
            }
        }
    }
}
