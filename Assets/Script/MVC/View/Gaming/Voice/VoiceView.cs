

using UnityEngine;
using UnityEngine.UI;

public class VoiceView : MonoBehaviour
{
    private AndroidJavaClass jc;
    private AndroidJavaObject jo;
    public Button button = null;
    public Text VoiceText = null;

    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            jo.Call("startSpeechListener");
        });
    }

    public void Result(string recognizerResult)
    { 
        VoiceText.text = recognizerResult + "\n";
    }

    public void SpeechState( string speechState )
    {

    }
}