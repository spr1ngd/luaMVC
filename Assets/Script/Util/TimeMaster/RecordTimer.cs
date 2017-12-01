
using System;

namespace LuaMVC
{
    public interface IRecordTimer : ITimer
    {
        float recordTime { get; set; }
        float remainTime { get; }
        DateTime recordDate { get; set; }
        bool ready { get; set; }
    }

    public class RecordTimer : Timer , IRecordTimer
    {
        public float recordTime { get; set; }
        public float remainTime
        {
            get { return recordTime - elapsed; }
        }
        public DateTime recordDate { get; set; }
        public bool ready { get; set; }

        public RecordTimer(string timerName, float recordTime, bool timeScaleEffect = true) : base(timerName, timeScaleEffect)
        {
            this.recordTime = recordTime;
        }
    }
}