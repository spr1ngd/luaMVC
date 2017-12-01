
namespace LuaMVC
{
    public class SkillTimer : Timer, ISkillTimer
    {
        public float cdTime { get; set; }
        public bool ready { get; set; }
        public float cdPercent
        {
            get { return elapsed / cdTime; }
        }

        public SkillTimer(string timerName, bool timeScaleEffect = false, float cdTime = 0.0f) : base(timerName, timeScaleEffect)
        {
            this.cdTime = cdTime;
            this.ready = true;
        }

        public override void StartTime()
        {
            base.StartTime();
            ready = false;
        }
        public override void CloseTime()
        {
            base.CloseTime();
            ready = true;
        }
        
        public virtual void TimeBuff(float buff)
        {
            this.elapsed += buff;
            if (elapsed > cdTime)
                elapsed = cdTime;
        }
        public virtual void TimeDsbuff(float dsbuff)
        {
            this.elapsed -= dsbuff;
            if (elapsed < 0)
                elapsed = 0.0f;
        }
    }
}