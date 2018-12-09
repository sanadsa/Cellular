using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimal.Common
{
    /// <summary>
    /// interface for the dal of the optimal system
    /// </summary>
    public interface IOptimalRepository
    {
        int GetNumberOfLines(int clientId);
        double GetRecieptsSum(int clientId);
        int GetCallsToCenter(int clientId);
        Client GetClient(int clientId, string contactNumber);
        List<Client> GetMostCalling();
        List<ServiceAgent> GetBestSellers();
    }
}
