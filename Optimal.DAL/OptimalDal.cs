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
    public class OptimalDal : IOptimalRepository
    {
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

        public int GetNumberOfLines(int clientId)
        {
            throw new NotImplementedException();
        }

        public double GetRecieptsSum(int clientId)
        {
            throw new NotImplementedException();
        }
    }
}
