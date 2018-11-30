using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class TemplatePackage
    {
        public int Id { get; set; }
        public string PackageName { get; set; }
        public double TotalPrice { get; set; }
        public int MaxMinute { get; set; }
        public double MinutePrice { get; set; }
        public double Discount { get; set; }
        public bool FavoriteNumber { get; set; }
        public bool MostCalledNumbers { get; set; }
        public bool FamilyDiscount { get; set; }
    }
}
