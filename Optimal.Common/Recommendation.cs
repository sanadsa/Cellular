using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimal.Common
{
    public class Recommendation
    {
        public double TotalPrice { get; set; }
        public double TotalMins { get; set; }
        public int TotalSMS { get; set; }
        public double TopMinsTopNum { get; set; }
        public double TopMinsMostCalled { get; set; }
        public double TopMinsFamily { get; set; }
        public TemplatePackage FirstRecommendation { get; set; }
        public TemplatePackage SecondRecommendation { get; set; }
        public TemplatePackage ThirdRecommendation { get; set; }
    }
}
