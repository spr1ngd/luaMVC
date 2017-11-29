

namespace Game
{
    public class TimeEvent
    {
        public float RemainTime = 0.0f;
        public float UpperLimitTime = 0.0f;

        public TimeEvent(){}
        public TimeEvent( float remainTime,float upperLimit )
        {
            this.RemainTime = remainTime;
            this.UpperLimitTime = upperLimit;
        }
    }
}