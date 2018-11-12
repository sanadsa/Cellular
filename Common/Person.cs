using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log;

namespace Common
{
    public class Person
    {
        private string name;
        private int id;
        private LogWriter log = new LogWriter();

        /// <summary>
        /// constructor that initializes the person data member 
        /// </summary>
        /// <param name="name">name of the person</param>
        public Person(string name)
        {
            Name = name;
        }

        /// <summary>
        /// get and set for person name
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                try
                {
                    name = value;
                }
                catch (Exception ex)
                {
                    log.LogWrite("Assigning person name error");
                    throw new Exception("assigning person name error " + ex.Message);
                }
            }
        }

        /// <summary>
        /// get and set for person id, if value is nagetive throw exception
        /// </summary>
        public int Id
        {
            get { return id; }
            set
            {
                try
                {
                    if (value > 0)
                    {
                        id = value;
                    }
                    else
                    {
                        throw new Exception("value must be non-negative");
                    }
                }
                catch (Exception ex)
                {
                    log.LogWrite("Assigning person id error: " + ex.Message);
                    throw new Exception("assigning person id error: " + ex.Message);
                }
            }
        }
    }
}
