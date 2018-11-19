using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Common
{
    public interface IInvoiceRepository
    {
        // simulator
        Call AddCall(Call call);
        SMS AddSms(SMS sms);
        // receipt
        Payment AddPayment(Payment payment);
        Package GetPackage(int lineId);
        IEnumerable<Call> GetCalls(int lineId, DateTime month);
    }
}
