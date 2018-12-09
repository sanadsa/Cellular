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
        public List<ServiceAgent> GetBestSellers()
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var bestSellers = context.ServiceAgents.OrderByDescending(s => s.SalesAmount);
                    if (bestSellers == null)
                    {
                        return null;
                    }

                    return bestSellers.ToList();
                }
            }
            catch (NullReferenceException)
            {
                throw new Exception("Get best sellers null");
            }
            catch (Exception e)
            {
                throw new Exception("Get best sellers exception: " + e.Message);
            }
        }

        /// <summary>
        /// get number of calls to center by the client
        /// </summary>
        public int GetCallsToCenter(int clientId)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var callsToCenter = context.Clients.SingleOrDefault(c => c.ClientID == clientId);
                    if (callsToCenter == null)
                    {
                        return 0;
                    }

                    return callsToCenter.CallsToCenter;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Get calls to center exception: " + e.Message);
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
        /// get most calling clients to center call
        /// </summary>
        /// <returns></returns>
        public List<Client> GetMostCalling()
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var mostCallingToCenter = context.Clients.OrderBy(c => c.CallsToCenter);
                    if (mostCallingToCenter == null)
                    {
                        return null;
                    }

                    return mostCallingToCenter.ToList();
                }
            }
            catch (NullReferenceException e)
            {
                throw new Exception("Get most calling to cetner null");
            }
            catch (Exception e)
            {
                throw new Exception("Get most calling to cetner exception: " + e.Message);
            }
        }

        /// <summary>
        /// gets number of lines that the client have - by client id
        /// </summary>
        public int GetNumberOfLines(int clientId)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var linesAmount = context.Lines.Where(l => l.ClientId == clientId);
                    if (linesAmount == null)
                    {
                        return 0;
                    }

                    return linesAmount.Count();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Get number of lines exception: " + e.Message);
            }
        }

        /// <summary>
        /// gets total price of the receipt for the client for last 3 month
        /// </summary>
        public double GetRecieptsSum(int clientId)
        {
            try
            {
                using (CellularModel context = new CellularModel())
                {
                    var payment1 = context.Payments.SingleOrDefault(p => (p.ClientID == clientId && p.Month.Month == DateTime.Now.Month));
                    var payment2 = context.Payments.SingleOrDefault(p => (p.ClientID == clientId && p.Month.Month - 1 == DateTime.Now.Month - 1));
                    var payment3 = context.Payments.SingleOrDefault(p => (p.ClientID == clientId && p.Month.Month - 2 == DateTime.Now.Month - 2));
                    if (payment1 == null && payment2 == null && payment3 == null)
                    {
                        return 0;
                    }

                    return payment1.TotalPayment + payment2.TotalPayment + payment3.TotalPayment;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Get payment exception: " + e.Message);
            }
        }
       
    }
}
