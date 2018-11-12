using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Log;

namespace CRM.Common.Classes
{
    /// <summary>
    /// Class person of type Service agent contain fields that only agent have
    /// </summary>
    public class ServiceAgent : Person
    {
        private string password;
        private int salesAmount;
        private LogWriter log = new LogWriter();

        /// <summary>
        /// constructor that initializes the ServiceAgent data member 
        /// </summary>
        public ServiceAgent(string name, string pass) : base(name)
        {
            Password = pass;
            SalesAmount = 0;
        }

        /// <summary>
        /// get and set for password
        /// </summary>
        public string Password
        {
            get { return password; }
            set
            {
                try
                {
                    password = value;
                }
                catch (Exception ex)
                {
                    log.LogWrite("Assigning password error");
                    throw new Exception("assigning password error " + ex.Message);
                }
            }
        }

        /// <summary>
        /// get and set for sales amount, if value is nagetive throw exception
        /// </summary>
        public int SalesAmount
        {
            get { return salesAmount; }
            set
            {
                try
                {
                    if (value > 0)
                    {
                        salesAmount = value;
                    }
                    else
                    {
                        throw new Exception("value must be non-negative");
                    }
                }
                catch (Exception ex)
                {
                    log.LogWrite("Assigning salesAmount error: " + ex.Message);
                    throw new Exception("assigning salesAmount error: " + ex.Message);
                }
            }
        }
    }
}
