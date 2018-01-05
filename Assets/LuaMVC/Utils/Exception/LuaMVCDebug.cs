 
namespace LuaMVC
{ 
    public static class LuaMVCDebug
    {
        /// <summary>
        /// 设置为false屏蔽富文本格式输出
        /// </summary>
        private static bool m_supportRichText = true;
        /// <summary>
        /// 设置为false屏蔽框架自身输出的日志
        /// </summary>
        private static bool m_luaMVCLog = true;

        public static void Debug(object message)
        {
            string input = message.ToString();
            if (m_supportRichText)
                input = "<b><color=black>LuaMVC : </color></b>" + message.ToString();
            UnityEngine.Debug.Log(input);
        }

        public static void DebugSuccess(object message)
        {
            if(m_luaMVCLog)
            {
                string input = message.ToString();
                if (m_supportRichText)
                    input = "<b><color=green>LuaMVC : </color>" + message.ToString() + "</b>";
                UnityEngine.Debug.Log(input);
            }
        }

        public static void DebugWarning(object message)
        { 
            string input = message.ToString();
            if (m_supportRichText)
                input = "<b><color=orange>LuaMVC : </color>" + message.ToString() + "</b>";
            UnityEngine.Debug.LogWarning(input);
        }

        public static void DebugError(object message)
        {
            string input = message.ToString();
            if (m_supportRichText)
                input = "<b><color=red>LuaMVC : </color>" + message.ToString() + "</b>";
            UnityEngine.Debug.LogError(input);
        }
    }
}