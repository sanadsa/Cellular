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
