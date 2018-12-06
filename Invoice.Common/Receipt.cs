using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Common
{
    /// <summary>
    /// class that contains the fields of the receipt
    /// </summary>
    public class Receipt
    {
        public string LineNumber { get; set; }
        public int PackageMinutes { get; set; }
        public double MinutesLeft { get; set; }
        public double PackageUsage { get; set; }
        public double PackagePrice { get; set; }
        public double MinutesOutOfPackage { get; set; }
        public double PricePerMinute { get; set; }
        public double Extra { get; set; }
        public double TotalPrice { get; set; }
    }
}
