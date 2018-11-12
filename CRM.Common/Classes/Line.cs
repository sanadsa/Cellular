﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log;

namespace CRM.Common.Classes
{
    public enum eStatus
    {
        available = 1,
        user,
        stolen,
        blocked
    };

    /// <summary>
    /// contains information of the line and the client it belongs to
    /// </summary>
    public class Line
    {
        private int lineId;
        private int clientId;
        private string number;
        private eStatus status;
        private int packageId;
        private LogWriter log = new LogWriter();

        /// <summary>
        /// constructor that initializes the Line data member 
        /// </summary>
        /// <param name="clientId">id of the owner</param>
        /// <param name="number">number of the line</param>
        /// <param name="status">info about the line</param>
        /// <param name="packageId">package of the line</param>
        public Line(int clientId, string number, eStatus status, int packageId)
        {
            ClientId = clientId;
            Number = number;
            Status = status;
            PackageId = packageId;
        }

        /// <summary>
        /// get clientId
        /// set clientId - if value is negative throw exception
        /// </summary>
        public int ClientId
        {
            get { return clientId; }
            set
            {
                try
                {
                    if (value > 0)
                    {
                        clientId = value;
                    }
                    else
                    {
                        throw new Exception("value must be non-negative");
                    }
                }
                catch (Exception ex)
                {
                    log.LogWrite("Assigning clientId error: " + ex.Message);
                    throw new Exception("Assigning clientId error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// get and set for Number
        /// </summary>
        public string Number
        {
            get { return number; }
            set
            {
                try
                {
                    if (value.Length < 10)
                    {
                        foreach (char c in number)
                        {
                            if (c < '0' || c > '9')
                            {
                                throw new Exception("enter digits only");
                            }
                        }
                        number = value;
                    }
                    else
                    {
                        throw new Exception("value must be 10 digits");
                    }
                }
                catch (Exception ex)
                {
                    log.LogWrite("Assigning number error: " + ex.Message);
                    throw new Exception("Assigning number error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// get and set for status
        /// </summary>
        public eStatus Status
        {
            get { return status; }
            set
            {
                try
                {
                    status = value;
                }
                catch (Exception ex)
                {
                    log.LogWrite("Assigning status error: " + ex.Message);
                    throw new Exception("Assigning status error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// get package id
        /// set package id - if value is negative throw exception
        /// </summary>
        public int PackageId
        {
            get { return packageId; }
            set
            {
                try
                {
                    if (value > 0)
                    {
                        packageId = value;
                    }
                    else
                    {
                        throw new Exception("value must be non-negative");
                    }
                }
                catch (Exception ex)
                {
                    log.LogWrite("Assigning packageId error: " + ex.Message);
                    throw new Exception("Assigning packageId error: " + ex.Message);
                }
            }
        }
    }
}
