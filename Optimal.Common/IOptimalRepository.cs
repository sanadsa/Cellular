using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimal.Common
{
    public interface IOptimalRepository
    {
        int GetNumberOfLines(int clientId);
        double GetRecieptsSum(int clientId);
        int GetCallsToCenter(int clientId);
        Client GetClient(int clientId, string contactNumber);
    }
}
