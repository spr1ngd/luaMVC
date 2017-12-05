
namespace LuaMVC
{
    public class Countdown : SkillTimer , ICountdown
    {
        public Countdown(string timerName, bool timeScaleEffect = false, float cdTime = 0) : base(timerName, timeScaleEffect, cdTime)
        {

        }

        public float remainingTime
        {
            get { return cdTime - elapsed; }
        }

        public override void TimeBuff(float buff)
        {
            this.elapsed -= buff;
        }

        public override void TimeDsbuff(float dsbuff)
        {
            this.elapsed += dsbuff;
            if (elapsed >= cdTime)
            {
                this.elapsed = cdTime;
                CloseTime();
            }
        }
    }
}