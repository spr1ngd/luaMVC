
using LuaMVC;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTest : MonoBehaviour
{
    private Countdown Countdown = null;
    public Text TimeText = null;
    public Button RightButton = null;
    public Button ErrorButton = null;
    public Button StartButton = null;
    private int rightCount = 0;
    private int errorCount = 0;

    private void Start()
    {
        Countdown = new Countdown("GameTimer",false,40);
        Countdown.OnTimingAction = (time) => { TimeText.text = "剩余时间:" + Countdown.remainingTime.ToString("F1"); };
        Countdown.OnCloseTimeAction = (time) =>
        {
            Debug.Log("答对:" + rightCount + "次");
            Debug.Log("答错:" + errorCount + "次");
            TimeText.text = "剩余时间:" + 0.ToString("F1");
            RightButton.interactable = false;
            ErrorButton.interactable = false;
        };
        RightButton.onClick.AddListener(() => { rightCount++; Countdown.TimeBuff(2); });
        ErrorButton.onClick.AddListener(() => { errorCount++; Countdown.TimeDsbuff(6); });
        StartButton.onClick.AddListener(() =>
        {
            RightButton.interactable = true;
            ErrorButton.interactable = true;
            Countdown.StartTime();
        });
    }
}