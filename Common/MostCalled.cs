﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MostCalled
    {
        private int mostCalledId;
        private string firstNumber;
        private string secondNumber;
        private string thirdNumber;

        public MostCalled(string num1, string num2, string num3)
        {
            FirstNumber = num1;
            SecondNumber = num2;
            ThirdNumber = num3;
        }

        public string FirstNumber { get => firstNumber; set => firstNumber = value; }
        public string SecondNumber { get => secondNumber; set => secondNumber = value; }
        public string ThirdNumber { get => thirdNumber; set => thirdNumber = value; }
        public int MostCalledId { get => mostCalledId; set => mostCalledId = value; }
    }
}