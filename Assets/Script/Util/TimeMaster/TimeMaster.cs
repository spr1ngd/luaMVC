
namespace LuaMVC
{
    using System.Collections.Generic;
    using UnityEngine;

    public class TimeMaster : MonoBehaviour
    {
        private static TimeMaster m_instance = null;
        private IDictionary<string,ITimer> m_timers = new Dictionary<string, ITimer>();

        public static TimeMaster Instance
        {
            get
            {
                if (null == m_instance)
                {
                    // todo 需不需要分担到另一个游戏物体？可有效预防游戏物体被误删之后，自动恢复
                    GameObject timeMaster = new GameObject("TimeMaster");
                    m_instance = timeMaster.AddComponent<TimeMaster>();
                }
                return m_instance;
            }
        }

        public void AddTimer( string timerName,ITimer timer )
        {
            if (m_timers.ContainsKey(timerName))
                return;
            m_timers.Add(timerName,timer);
        }

        public void RemoveTimer( string timerName )
        {
            if (m_timers.ContainsKey(timerName))
            {
                m_timers[timerName].TimeOut();
                m_timers.Remove(timerName);
            }
        }
    }
}