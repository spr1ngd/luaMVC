
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class TimeMaster : MonoBehaviour
    {
        public UnityAction ActTimeUp = null;
        public UnityAction<TimeEvent> ActTimeRemain = null;
        public UnityAction ActTimeWarning = null;

        // todo 这个值可以由外界来配置
        public float UpperLimitTime = 10.0f;
        public float RewardSecond = 0.3f;
        public float PunishmentSeconds = 2.0f;

        private float Timing = 0.0f;
        private TimeEvent timeEvent ;
        private bool timing = false;

        private void Awake()
        {
            timeEvent = new TimeEvent(UpperLimitTime,UpperLimitTime);
        }

        private void Update()
        {
            if (timing)
            {
                Timing += Time.deltaTime;
                if (Timing >= UpperLimitTime)
                {
                    ActTimeUp();
                    Timing = 0;
                    timeEvent.RemainTime = 0;
                    ActTimeRemain(timeEvent);
                    timing = false;
                }
                timeEvent.RemainTime = UpperLimitTime - Timing;
                ActTimeRemain(timeEvent);
                if (timeEvent.RemainTime < timeEvent.UpperLimitTime * .2f)
                    ActTimeWarning();
            }
        }

        public void TimeIn()
        {
            timing = true;
        }

        public void AddUpperTime( float addTime )
        {
            UpperLimitTime += addTime;
        }

        public void AddTime()
        {
            Timing -= RewardSecond;
            if (Timing <= 0)
                Timing = 0;
        }

        public void ReduceTime()
        {
            Timing += PunishmentSeconds;
        }

        public void Restart()
        {
            timeEvent = new TimeEvent(UpperLimitTime, UpperLimitTime);
            timing = true;
        }
    }
}