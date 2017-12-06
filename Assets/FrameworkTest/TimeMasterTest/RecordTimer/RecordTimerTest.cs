
using System;
using LuaMVC;
using UnityEngine;
using UnityEngine.UI;

public class RecordTimerTest : MonoBehaviour
{
    public IRecordTimer recordTimer = null;
    public Button GoldenBox = null;
    public Text RemainText = null;

    private void Awake()
    {
        recordTimer = (IRecordTimer)TimeMaster.Instance.GetTimer("GlodenTreasure");

        if (null == recordTimer)
        {
            recordTimer = new RecordTimer("GlodenTreasure", 300, false);
            recordTimer.OnStartTimeAction = () => { Debug.Log("开始计时"); };
            recordTimer.OnCloseTimeAction = (time) => { Debug.Log("解锁完成"); };
            recordTimer.OnTimingAction = OnTiming;
            GoldenBox.onClick.AddListener(recordTimer.StartTime);
        }
        else
        {
            recordTimer.OnStartTimeAction = () => { Debug.Log("开始计时"); };
            recordTimer.OnTimingAction = OnTiming;
            if (!recordTimer.ready)
                recordTimer.StartTime();
            else
                RemainText.text = "宝箱已经解锁";
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("清空记录"))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    private void OnTiming(float time)
    {
        RemainText.text = "剩余时间：" + (recordTimer.recordTime - recordTimer.elapsed);
    }
}