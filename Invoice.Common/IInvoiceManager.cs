using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Common
{
    public interface IInvoiceManager
    {
        // simulator
        Call SimulateCall(int lineId, double duration, DateTime month, string destination);
        SMS SimulateSms(int lineId, DateTime month, string destinationNum);
        // receipt
        Payment AddPayment(int clientID, DateTime month, double totalPayment);
        double GetMinutesLeft(int lineId, DateTime month);
    }
}
