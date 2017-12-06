
using LuaMVC;
using UnityEngine;

public class NormalTimerTest : MonoBehaviour
{
    public Timer timer;

    private void Start()
    {
        timer = new Timer("SimpleTimer");
        timer.OnTimingAction = time => {    };
        timer.OnStartTimeAction = () => { Debug.Log("开始计时"); };
        timer.OnCloseTimeAction = time => { Debug.Log("本次共计时:" + time); };
        timer.AddIntervalEvent(5, () => { Debug.Log("出一波小兵");},true);
        timer.AddIntervalEvent(10, () => { Debug.Log("出一波超级兵");},true);
    }
    
    private void OnGUI()
    {
        if (GUILayout.Button("开始计时"))
        {
            timer.StartTime();
        }
        GUILayout.Label("已经开始" + timer.elapsed);
        if (timer.timing)
        {
            if (GUILayout.Button("暂停计时"))
            {
                timer.PauseTime();
            }
        }
        else
        {
            if (GUILayout.Button("恢复计时"))
            {
                timer.ResumeTime();
            }
        }
        if (GUILayout.Button("时间加速"))
        {
            Time.timeScale++;
        }
        if (GUILayout.Button("时间恢复正常"))
        {
            Time.timeScale--;
        }

        if (GUILayout.Button("结束计时"))
        {
            timer.CloseTime();
        }
        if (GUILayout.Button("清空计时"))
        {
            timer.ClearTime();
        }
    }
}