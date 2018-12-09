using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum eCallTo
    {
        friends = 1,
        family,
        mostCalled,
        center,
        general
    };

    /// <summary>
    /// contains the call information for the call of the line
    /// </summary>
    public class Call
    {
        private int callId;
        private int lineId;
        private Line line;
        private double duration;
        private DateTime month;
        private string destinationNum;
        private eCallTo callTo;

        public Call(int lineId, double duration, DateTime month, string destination, eCallTo callTo)
        {
            LineID = lineId;
            Duration = duration;
            Month = month;
            DestinationNum = destination;
            CallTo = callTo;
        }

        private Call() { }

        public int LineID { get => lineId; set => lineId = value; }
        public DateTime Month { get => month; set => month = value; }
        public string DestinationNum { get => destinationNum; set => destinationNum = value; }
        public double Duration { get => duration; set => duration = value; }
        public int CallId { get => callId; set => callId = value; }
        public Line Line { get => line; set => line = value; }
        public eCallTo CallTo { get => callTo; set => callTo = value; }
    }
}
