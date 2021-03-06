﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// payment for line class
    /// </summary>
    public class Payment
    {
        private int paymentId;
        private int clientId;
        private Client client;
        private DateTime month;
        private double totalPayment;

        /// <summary>
        /// constructor that initializes the Payment data member 
        /// </summary>
        public Payment(int clientID, DateTime month, double totalPayment)
        {
            ClientID = clientID;
            Month = month;
            TotalPayment = totalPayment;
        }

        public Payment()
        {

        }

        public int ID { get => paymentId; set => paymentId = value; }
        public int ClientID { get => clientId; set => clientId = value; }
        public DateTime Month { get => month; set => month = value; }
        public double TotalPayment { get => totalPayment; set => totalPayment = value; }
        public Client Client { get => client; set => client = value; }
    }
}
