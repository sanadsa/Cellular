using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Log;

namespace CRM.Common.Classes
{
    /// <summary>
    /// Class person of type Client contain fields that only client have
    /// </summary>
    public class Client : Person
    {
        private string lastName;
        private int idNumber;
        private int clientTypeId;
        private string address;
        private string contactNumber;
        private int callsToCenter;
        private LogWriter log = new LogWriter();

        /// <summary>
        /// constructor that initializes the Client data member 
        /// </summary>
        public Client(string name, string lastName, int idNumber, int clientTypeId, 
            string address, string contactNumber, int callsToCenter): base(name)
        {
            LastName = lastName;
            IdNumber = idNumber;
            ClientTypeId = clientTypeId;
            Address = address;
            ContactNumber = contactNumber;
            CallsToCenter = callsToCenter;
        }

        /// <summary>
        /// get and set for lastName
        /// </summary>
        public string LastName
        {
            get { return lastName; }
            set
            {
                try
                {
                    lastName = value;
                }
                catch (Exception ex)
                {
                    log.LogWrite("Assigning last name error: " + ex.Message);
                    throw new Exception("Assigning last name error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// get id number
        /// set id number - if value is negative throw exception
        /// </summary>
        public int IdNumber
        {
            get { return idNumber; }
            set
            {
                try
                {
                    if (value > 0)
                    {
                        idNumber = value;
                    }
                    else
                    {
                        throw new Exception("value must be non-negative");
                    }
                }
                catch (Exception ex)
                {
                    log.LogWrite("Assigning idNumber error: " + ex.Message);
                    throw new Exception("Assigning idNumber error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// get client type id
        /// set client type id - if value is negative throw exception
        /// </summary>
        public int ClientTypeId
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
                    log.LogWrite("Assigning clientTypeId error: " + ex.Message);
                    throw new Exception("Assigning clientTypeId error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// get and set for address
        /// </summary>
        public string Address
        {
            get { return address; }
            set
            {
                try
                {
                    address = value;
                }
                catch (Exception ex)
                {
                    log.LogWrite("Assigning address error: " + ex.Message);
                    throw new Exception("Assigning address error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// get and set for contact number
        /// </summary>
        public string ContactNumber
        {
            get { return contactNumber; }
            set
            {
                try
                {
                    contactNumber = value;
                }
                catch (Exception ex)
                {
                    log.LogWrite("Assigning contactNumber error: " + ex.Message);
                    throw new Exception("Assigning contactNumber error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// get calls to center       
        /// set calls to center - if value is negative throw exception
        /// </summary>
        public int CallsToCenter
        {
            get { return callsToCenter; }
            set
            {
                try
                {
                    if (value > 0)
                    {
                        callsToCenter = value;
                    }
                    else
                    {
                        throw new Exception("value must be non-negative");
                    }
                }
                catch (Exception ex)
                {
                    log.LogWrite("Assigning callsToCenter error: " + ex.Message);
                    throw new Exception("Assigning callsToCenter error: " + ex.Message);
                }
            }
        }
    }
}
