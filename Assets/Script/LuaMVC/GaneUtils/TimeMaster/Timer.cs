
namespace LuaMVC
{
    using System;
    using System.Collections.Generic;

    public class IntervalEvent
    {
        public float intervalTime { get; private set; }
        public bool actionRepeat { get; private set; }
        public Action intervalAction { get; private set; }
        public int intervalPointer { get; set; }

        public IntervalEvent( float interval,Action intervalMethod,bool actionRepeat = false )
        {
            this.intervalTime = interval;
            this.intervalAction = intervalMethod;
            this.actionRepeat = actionRepeat;
            intervalPointer = 0;
        }
    }
    
    public class Timer : ITimer
    {
        public string name { get; private set; }
        public float elapsed { get; set; }
        public bool timing { get; private set; }
        public bool timeScaleEffect { get; private set; }
        public IList<IntervalEvent> intervalEvents { get; set; }

        public Action OnStartTimeAction { get; set; }
        public Action<float> OnCloseTimeAction { get; set; }
        public Action<float> OnTimingAction { get; set; }

        public Timer(string timerName, bool timeScaleEffect = true)
        {
            this.name = timerName;
            this.timing = false;
            this.timeScaleEffect = timeScaleEffect;
            this.elapsed = 0.0f;
            TimeMaster.Instance.AddTimer(this);
        }

        public void AddIntervalEvent( IntervalEvent intervalEvent )
        { 
            if( null == intervalEvents)
                intervalEvents = new List<IntervalEvent>();
            intervalEvents.Add(intervalEvent);
        }
        public void AddIntervalEvent(float interval, Action intervalAction, bool repeat = false)
        {
            AddIntervalEvent(new IntervalEvent(interval, intervalAction, repeat));
        }

        public virtual void StartTime()
        {
            elapsed = 0.0f;
            timing = true;
            if (null != OnStartTimeAction)
                OnStartTimeAction();
        }
        public virtual void CloseTime()
        {
            timing = false;
            if (null != OnCloseTimeAction)
                OnCloseTimeAction(elapsed);
        }
        public virtual void PauseTime()
        {
            timing = false;
        }
        public virtual void ResumeTime()
        {
            timing = true;
        }
        public virtual void ClearTime()
        {
            CloseTime();
            elapsed = 0.0f;
        }
    }
}