using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimal.Common
{
    public class MostValueClient
    {
        double clientValue;
        Client client;

        public double ClientValue { get => clientValue; set => clientValue = value; }
        public Client Client { get => client; set => client = value; }
    }
}
