using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// package info for the line, package can have:
    /// 1. minutes per price
    /// 2. discount for 3 lines
    /// 3. free calls to favorite number
    /// 4. discount for family
    /// 5. combination of the sales
    /// </summary>
    public class Package
    {
        private int packageId;
        private string packageName;
        private int lineId;
        private Line line;
        private double totalPrice;
        private DateTime month;
        private int maxMinute;
        private double minutePrice;
        private double discountPercentage;
        private bool favoriteNumber;
        private bool mostCalledNums;
        private bool familyDiscount;
        // sales cost
        private const int secondSale = 10;
        private const int thirdSale = 15;
        private const int forthSale = 12;

        /// <summary>
        /// package info for the line - contain all sales and their cost
        /// </summary>
        /// <param name="name">name of  the package</param>
        /// <param name="lineId">the id of the line that have this package</param>
        /// <param name="price">total price of the package</param>
        /// <param name="maxMinute">first sale - max call minutes for the line in month</param>
        /// <param name="minutePrice">the price for all call minutes</param>
        /// <param name="discount">second sale - discount percentage for favorite numbers</param>
        /// <param name="favoritNumId">id of the numbers that have dicsount (3 numbers)</param>
        /// <param name="mostCalled">third sale - discount for most called number (bool)</param>
        /// <param name="famDis">fourth sale - discount for the family(lines for same client)</param>
        public Package(string name, int lineId, double price, DateTime month, int maxMinute, double minutePrice,
            double discount,bool favoriteNum, bool mostCalled, bool famDis)
        {
            PackageName = name;
            LineId = lineId;
            TotalPrice = price;
            Month = month;
            MaxMinute = maxMinute;
            MinutePrice = minutePrice;
            DiscountPercentage = discount;
            FavoriteNumber = favoriteNum;
            MostCalledNums = mostCalled;
            FamilyDiscount = famDis;
        }

        // for entity framework
        public Package()
        {

        }

        public string PackageName { get => packageName; set => packageName = value; }
        public int LineId { get => lineId; set => lineId = value; }
        public double TotalPrice { get => totalPrice; set => totalPrice = value; }
        public int MaxMinute { get => maxMinute; set => maxMinute = value; }
        public double DiscountPercentage { get => discountPercentage; set => discountPercentage = value; }
        public bool MostCalledNums { get => mostCalledNums; set => mostCalledNums = value; }
        public bool FamilyDiscount { get => familyDiscount; set => familyDiscount = value; }
        public double MinutePrice { get => minutePrice; set => minutePrice = value; }
        public int PackageId { get => packageId; set => packageId = value; }
        public DateTime Month { get => month; set => month = value; }
        public Line Line { get => line; set => line = value; }
        public bool FavoriteNumber { get => favoriteNumber; set => favoriteNumber = value; }

        public int SecondSale => secondSale;

        public int ThirdSale => thirdSale;

        public int ForthSale => forthSale;
    }
}
