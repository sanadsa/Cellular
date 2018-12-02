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
        private eCallTo callTo;

        public SMS(int lineId, DateTime month, string destinationNum, eCallTo callTo)
        {
            LineID = lineId;
            Month = month;
            DestinationNum = destinationNum;
            CallTo = callTo;
        }

        public SMS()
        {

        }

        public int LineID { get => lineId; set => lineId = value; }
        public DateTime Month { get => month; set => month = value; }
        public string DestinationNum { get => destinationNum; set => destinationNum = value; }
        public int SmsID { get => smsID; set => smsID = value; }
        public Line Line { get => line; set => line = value; }
        public eCallTo CallTo { get => callTo; set => callTo = value; }
    }
}
