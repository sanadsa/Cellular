using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// most called numbers for package class
    /// </summary>
    public class MostCalled
    {
        private int mostCalledId;
        private Package package;
        private int packageId;
        private string firstNumber;
        private string secondNumber;
        private string thirdNumber;

        
        public MostCalled(int packageId, string num1, string num2, string num3)
        {
            PackageId = packageId;
            FirstNumber = num1;
            SecondNumber = num2;
            ThirdNumber = num3;
        }

        public MostCalled()
        {

        }

        public string FirstNumber { get => firstNumber; set => firstNumber = value; }
        public string SecondNumber { get => secondNumber; set => secondNumber = value; }
        public string ThirdNumber { get => thirdNumber; set => thirdNumber = value; }
        public int MostCalledId { get => mostCalledId; set => mostCalledId = value; }
        public int PackageId { get => packageId; set => packageId = value; }
        public Package Package { get => package; set => package = value; }
    }
}
