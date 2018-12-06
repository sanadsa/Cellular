using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimal.Common
{
    /// <summary>
    /// interface for the ui of the optimal system
    /// </summary>
    public interface IOptimalManager
    {
        Client GetClient(int clientId, string contactNumber);
        double GetClientValue(int clientId);
    }
}
