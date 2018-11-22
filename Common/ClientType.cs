using Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Class that contain the pricing information according to type (vip, business, private..)
    /// </summary>
    public class ClientType
    {
        private int clientTypeId;
        private string typeName;
        private double minutePrice;
        private int smsPrice;
        private LogWriter log = new LogWriter();

        /// <summary>
        /// constructor that initializes the ClientType data member 
        /// </summary>
        public ClientType(string typeName, double minutePrice, int smsPrice)
        {
            TypeName = typeName;
            MinutePrice = minutePrice;
            SmsPrice = smsPrice;
        }

        // for ef
        public ClientType()
        {

        }

        /// <summary>
        /// get and set for id, if value is nagetive throw exception
        /// </summary>
        public int Id
        {
            get { return clientTypeId; }
            set
            {
                try
                {
                    if (value > 0)
                    {
                        clientTypeId = value;
                    }
                    else
                    {
                        throw new Exception("value must be non-negative");
                    }
                }
                catch (Exception ex)
                {
                    log.LogWrite("Assigning id error: " + ex.Message);
                    throw new Exception("assigning id error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// get and set for type name
        /// </summary>
        public string TypeName
        {
            get { return typeName; }
            set
            {
                try
                {
                    typeName = value;
                }
                catch (Exception ex)
                {
                    log.LogWrite("Assigning typeName error");
                    throw new Exception("assigning typeName error " + ex.Message);
                }
            }
        }

        /// <summary>
        /// get and set for call minute price, if value is nagetive throw exception
        /// </summary> 
        public double MinutePrice
        {
            get { return minutePrice; }
            set
            {
                try
                {
                    if (value > 0)
                    {
                        minutePrice = value;
                    }
                    else
                    {
                        throw new Exception("value must be non-negative");
                    }
                }
                catch (Exception ex)
                {
                    log.LogWrite("Assigning minutePrice error: " + ex.Message);
                    throw new Exception("assigning minutePrice error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// get and set for sms price, if value is nagetive throw exception
        /// </summary>
        public int SmsPrice
        {
            get { return smsPrice; }
            set
            {
                try
                {
                    if (value > 0)
                    {
                        smsPrice = value;
                    }
                    else
                    {
                        throw new Exception("value must be non-negative");
                    }
                }
                catch (Exception ex)
                {
                    log.LogWrite("Assigning smsPrice error: " + ex.Message);
                    throw new Exception("assigning smsPrice error: " + ex.Message);
                }
            }
        }
    }
}
