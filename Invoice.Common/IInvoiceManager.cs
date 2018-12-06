using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Common
{
    /// <summary>
    /// interface for the ui of the invoice, implemented by the invoiceBl
    /// </summary>
    public interface IInvoiceManager
    {
        // simulator
        Call SimulateCall(int lineId, double duration, DateTime month, string destination, eCallTo callTo);
        SMS SimulateSms(int lineId, DateTime month, string destinationNum, eCallTo callTo);
        // receipt
        Payment AddPayment(int clientID, DateTime month, double totalPayment);
        double GetMinutesLeft(int lineId, DateTime month);
        double GetCallsPayment(int lineId, DateTime month);
        Receipt GetReceipt(int lineId, DateTime month);
    }
}
