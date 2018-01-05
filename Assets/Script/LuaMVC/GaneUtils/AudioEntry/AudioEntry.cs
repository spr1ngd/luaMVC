
using LuaMVC;
using UnityEngine;

namespace LuaMVC
{
    // todo 这里播放是的系统级别的声音，声音并不属于某个游戏物体，如果是需要某个游戏物体发出声音，请将AudioSource挂在游戏物体上
    // todo 背景音乐可以在此播放
    public class AudioEntry : BaseView
    {
        private AudioRecord audioSetting = null;
        private bool drawAudioWindow = false;
        public AudioSource source = null;

        public override void Initialize()
        { 
            base.Initialize();
            this.ViewName = E_ViewType.AudioEntry;
            Read4PlayerPrefs();
        }
        public override void Open()
        {
            base.Open();
            drawAudioWindow = true;
        }
        public override void Close()
        {
            base.Close();
            drawAudioWindow = false;
        }

        private void Update()
        {
            if (drawAudioWindow)
            {
                if (Input.GetKeyDown(KeyCode.F1))
                    Close();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F1))
                    Open();
            }
        }  
        private void OnGUI()
        {
            if (drawAudioWindow)
            {
                GUILayout.Window(1, new Rect(
                        new Vector2(Screen.width / 2.0f - 120, Screen.height / 2.0f - 80), new Vector2(240, 160)),
                    DrawAudioWindow, "声音管理器");
            }
        }
        private void EnableAudioWindow( bool enable = true )
        {
            drawAudioWindow = enable;
        }
        private void DrawAudioWindow(int windowID)
        {
            audioSetting.hasBackgroundAudio = GUILayout.Toggle(audioSetting.hasBackgroundAudio, "音乐");
            if(audioSetting.hasBackgroundAudio)
                audioSetting.backgroundAudioVolume = GUILayout.HorizontalSlider(audioSetting.backgroundAudioVolume, 0, 1);
            audioSetting.hasEffectAudio = GUILayout.Toggle(audioSetting.hasEffectAudio, "音效");
            if(audioSetting.hasEffectAudio)
                audioSetting.effectAudioVolume = GUILayout.HorizontalSlider(audioSetting.effectAudioVolume, 0, 1);
            if (GUILayout.Button("确定"))
            {
                Write2PlayerPrefs();
                drawAudioWindow = false;
            }
            if (GUILayout.Button("取消"))
            {
                drawAudioWindow = false;
            }
        }
        // 读取操作
        private void Write2PlayerPrefs()
        {
            if (null == audioSetting)
                return;
            string audioInfo = SimpleJson.SimpleJson.SerializeObject(audioSetting);
            PlayerPrefs.SetString("AudioSetting", audioInfo);
        }
        private void Read4PlayerPrefs()
        {
            string audioInfo = PlayerPrefs.GetString("AudioSetting");
            if (string.IsNullOrEmpty(audioInfo))
            {
                audioSetting = new AudioRecord()
                {
                    hasBackgroundAudio = true,
                    hasEffectAudio = true,
                    backgroundAudioVolume = 0.6f,
                    effectAudioVolume = 0.75f
                };
                return;
            }
            audioSetting = SimpleJson.SimpleJson.DeserializeObject<AudioRecord>(audioInfo);
        }
    }

    public class AudioRecord
    {
        public bool hasBackgroundAudio = true;
        public float backgroundAudioVolume = 0.6f;
        public bool hasEffectAudio = true;
        public float effectAudioVolume = 0.75f;
    }
}