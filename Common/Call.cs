using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Call
    {
        private int callId;
        private int lineId;
        private Line line;
        private double duration;
        private DateTime month;
        private string destinationNum;

        public Call(int lineId, double duration, DateTime month, string destination)
        {
            LineID = lineId;
            Duration = duration;
            Month = month;
            DestinationNum = destination;
        }

        public int LineID { get => lineId; set => lineId = value; }
        public DateTime Month { get => month; set => month = value; }
        public string DestinationNum { get => destinationNum; set => destinationNum = value; }
        public double Duration { get => duration; set => duration = value; }
        public int CallId { get => callId; set => callId = value; }
        public Line Line { get => line; set => line = value; }
    }
}
