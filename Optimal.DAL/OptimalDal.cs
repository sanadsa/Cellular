using Common;
using DAL;
using Optimal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimal.DAL
{
    /// <summary>
    /// Class that applies the optimal system, implements the interface IOptimalRepository using entity framework (DBcontext CellularModel)
    /// </summary>
    public class OptimalDal : IOptimalRepository
    {
        /// <summary>
        /// get number of calls to center by the client
        /// </summary>
        public int GetCallsToCenter(int clientId)
        {
            try
            {
                int calls = 0;

                return calls;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        /// <summary>
        /// get client to login by id and number
        /// </summary>
        public Client GetClient(int clientId, string contactNumber)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var clientFromDb = context.Clients.SingleOrDefault(c => (c.IdNumber == clientId && c.ContactNumber == contactNumber));
                    if (clientFromDb == null)
                    {
                        return null;
                    }

                    return clientFromDb;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Get client exception: " + e.Message);
            }
        }

        /// <summary>
        /// gets number of lines that the client have - by client id
        /// </summary>
        public int GetNumberOfLines(int clientId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// gets total price of the receipt for the client
        /// </summary>
        public double GetRecieptsSum(int clientId)
        {
            throw new NotImplementedException();
        }
    }
}
