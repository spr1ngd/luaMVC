
namespace LuaMVC
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class TimeMaster : MonoBehaviour
    {
        // 构造一个计时器池，用于优化计时器的构造
        private ObjectPool<ITimer> m_timersPool = null;
        private static TimeMaster m_instance = null;
        private IList<ITimer> m_timers = new List<ITimer>();
        public static TimeMaster Instance
        {
            get
            {
                if (null == m_instance)
                {
                    GameObject timeMaster = new GameObject("TimeMaster");
                    m_instance = timeMaster.AddComponent<TimeMaster>();
                    m_instance.Read();
                }
                return m_instance;
            }
        }

        private void Update()
        {
            TimeRun();
        }
        private void TimeRun()
        {
            if (m_timers.Count <= 0)
                return;
            for (int i = 0; i < m_timers.Count; i++)
            {
                ITimer timer = m_timers[i];
                if (timer.timing)
                {
                    if (timer.timeScaleEffect)
                        timer.elapsed += Time.deltaTime;
                    else
                        timer.elapsed += Time.unscaledDeltaTime;
                    if (null != timer.OnTimingAction)
                        timer.OnTimingAction(timer.elapsed);
                    // skill timer?
                    if (timer.GetType() == typeof(SkillTimer) || timer.GetType() == typeof(Countdown))
                    {
                        ISkillTimer skillTimer = (ISkillTimer) timer;
                        if (skillTimer.elapsed >= skillTimer.cdTime)
                            skillTimer.CloseTime();
                    }
                    // invoke the interval event
                    if (null == timer.intervalEvents || timer.intervalEvents.Count <= 0)
                        continue;
                    for (int m = 0; m < timer.intervalEvents.Count; m++)
                    {
                        var intervalEvent = timer.intervalEvents[m];
                        if (!intervalEvent.actionRepeat)
                        {
                            if (intervalEvent.intervalTime >= timer.elapsed)
                            {
                                if (null != intervalEvent.intervalAction)
                                    intervalEvent.intervalAction();
                            }
                        }
                        else
                        {
                            // repeat mode
                            int curPointer = (int)(timer.elapsed / intervalEvent.intervalTime);
                            if (curPointer > intervalEvent.intervalPointer)
                            {
                                if (null != intervalEvent.intervalAction)
                                {
                                    intervalEvent.intervalAction();
                                    intervalEvent.intervalPointer++;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void AddTimer(ITimer timer )
        {
            if( !m_timers.Contains(timer) )
                m_timers.Add(timer);
        }
        public void RemoveTimer( string timerName )
        {
            for (int i = 0; i < m_timers.Count; i++)
            {
                if (m_timers[i].name.Equals(timerName))
                {
                    m_timers.RemoveAt(i);
                    return;
                }
            }
        }
        public void RemoveTimer( ITimer timer )
        {
            if (m_timers.Contains(timer))
                m_timers.Remove(timer);
        }
        public ITimer GetTimer( string timerName )
        {
            for (int i = 0; i < m_timers.Count; i++)
            {
                if (m_timers[i].name.Equals(timerName))
                    return m_timers[i];
            }
            return null;
        }
        
        public void Read()
        {
            string recordTimers = PlayerPrefs.GetString("RecordTimers");
            if (string.IsNullOrEmpty(recordTimers))
                return;
            IList<RecordTimerData> datas = SimpleJson.SimpleJson.DeserializeObject<List<RecordTimerData>>(recordTimers);
            // playerprefs - > json - > list<records>
            for (int i = 0; i < datas.Count; i++)
            {
                var data = datas[i];
                float recordTime = 0;
                string[] dates = data.RecordDate.Split('|');
                DateTime recordDate = new DateTime(int.Parse(dates[0]), int.Parse(dates[1]), int.Parse(dates[2]), int.Parse(dates[3]),
                    int.Parse(dates[4]), int.Parse(dates[5]));
                float elapsedTime = (DateTime.Now - recordDate).Seconds;
                if (data.RemainTime > elapsedTime)
                    recordTime = data.RemainTime - elapsedTime;
                IRecordTimer record = new RecordTimer(data.TimerName, recordTime, false);
                if (recordTime.Equals(0))
                    record.ready = true;
                AddTimer(record);
            }
        }
        public void Record()
        {
            List<RecordTimer> timers = new List<RecordTimer>();
            for (int i = 0; i < m_timers.Count; i++)
            {
                ITimer timer = m_timers[i];
                if (timer.GetType() == typeof(RecordTimer))
                {
                    RecordTimer recordTimer = (RecordTimer) timer;
                    recordTimer.recordDate = DateTime.Now;
                    timers.Add(recordTimer);
                }
            }
            // timers -> json -> playeroprefs
            if (timers.Count <= 0)
                return;
            List<RecordTimerData> datas = new List<RecordTimerData>();
            for (int i = 0; i < timers.Count; i++)
            {
                RecordTimerData data = new RecordTimerData();
                data.TimerName = timers[i].name;
                var date = timers[i].recordDate;
                string dateString = date.Year + "|" + date.Month + "|" + date.Day + "|" + date.Hour + "|" +  date.Minute + "|" + date.Second;
                data.RecordDate = dateString;
                data.RemainTime = timers[i].remainTime;
                datas.Add(data);
            }
            string recordTimers = SimpleJson.SimpleJson.SerializeObject(datas);
            PlayerPrefs.SetString("RecordTimers", recordTimers);
        }

        private void OnApplicationQuit()
        {
            Record();
        }
    }

    public class RecordTimerData
    {
        public string TimerName;
        public string RecordDate;
        public float RemainTime;
    }
}