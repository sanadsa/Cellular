using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SMS
    {
        private int smsID;
        private int lineId;
        private Line line;
        private DateTime month;
        private string destinationNum;

        public SMS(int lineId, DateTime month, string destinationNum)
        {
            LineID = lineId;
            Month = month;
            DestinationNum = destinationNum;
        }

        public SMS()
        {

        }

        public int LineID { get => lineId; set => lineId = value; }
        public DateTime Month { get => month; set => month = value; }
        public string DestinationNum { get => destinationNum; set => destinationNum = value; }
        public int SmsID { get => smsID; set => smsID = value; }
        public Line Line { get => line; set => line = value; }
    }
}
