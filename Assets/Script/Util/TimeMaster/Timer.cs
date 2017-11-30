
using System;

namespace LuaMVC
{
    public interface ITimer
    {
        string name { get; set; }
        float havetime { get; set; }
        bool timeScaleEffect { get; set; }
        void TimeIn();
        void TimeOut();
        void PauseTimer();
        void ResumeTimer();
    }

    public interface ISkillTimer : ITimer
    {
        float maxtime { get; set; }
        void TimeBuff(float buff);
        void TimeDsbuff(float dsbuff);
    }

    // todo example 1.做一个游戏计时器
    // todo example 2.做一个技能冷却
    // todo example 3.做一个倒计时功能 countdown
    // todo example 4.做一个可记录的计时器 利用系统事件来处理即可
    public class Timer : ITimer
    {
        public string name { get; set; }
        public float havetime { get; set; }
        public bool timeScaleEffect { get; set; }
        public Action TimeInAction { private get; set; }
        public Action<float> TimingAction { private get; set; }
        public Action<float> TimeOutAction { private get; set; }

        // todo 构造完之后将其自动注入到TimeMaster，TimeMaster可用Instance和消息来通知
        public Timer()
        {
            
        }
        public Timer(string timerName, bool timeScaleEffect = true, Action timeInAction = null, Action<float> timeOutAction = null, Action<float> timingAction = null)
        {
            this.name = timerName;
            this.timeScaleEffect = timeScaleEffect;
            this.havetime = 0.0f;
            this.TimeInAction = timeInAction;
            this.TimeOutAction = timeOutAction;
            this.TimingAction = timingAction;
        }

        public virtual void TimeIn()
        {
            havetime = 0.0f;
            if (null != TimeInAction)
                TimeInAction();
        }

        public virtual void TimeOut()
        {
            if (null != TimeOutAction)
                TimeOutAction(havetime);
        }

        public virtual void PauseTimer()
        {
            throw new NotImplementedException();
        }

        public virtual void ResumeTimer()
        {
            throw new NotImplementedException();
        }
    }

    public class SkillTimer : Timer , ISkillTimer
    {
        public float maxtime { get; set; }
        public void TimeBuff(float buff)
        {
            throw new NotImplementedException();
        }

        public void TimeDsbuff(float dsbuff)
        {
            throw new NotImplementedException();
        }
    }

    public class Countdown : Timer
    {

    }
}