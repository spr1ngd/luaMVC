
using System.Collections.Generic;
using LuaMVC;
using UnityEngine;

namespace Game
{
    public class AudioEntry : MonoBehaviour
    {
        public AudioSource SoundEffectAudioSource = null;
        public AudioSource BackgroundAudioSource = null;
        
        public void PlayOneShot( string audioName , bool loop = false)
        {
            // 根据名字从配置表里获取对应的音频文件进行播放
            // todo 这些东西移动到Medaitor层进行管理
            Debug.Log(audioName);
            PopAudio(audioName);
        }

        public void SetSoundEffectVolumn( float volumn )
        {
            SoundEffectAudioSource.volume = volumn;
        }

        public void SetBackgroundAudioVolumn( float volumn )
        {
            BackgroundAudioSource.volume = volumn;
        }

        // todo 1.音频从配置文件加载到字典中
        private Dictionary<string,AudioClip> m_audioClips = new Dictionary<string, AudioClip>();
        private Dictionary<string,string> m_audioKV = new Dictionary<string, string>();
        
        private void PopAudio( string audioName )
        {
            AudioClip audioClip = null;
            if (!m_audioClips.TryGetValue(audioName, out audioClip))
            {
                AssetLoader.LoadAudioClip(m_audioKV[audioName], ac => 
                {
                    Debug.Log("匿名函数执行");
                    m_audioClips.Add(audioName, ac);
                    BackgroundAudioSource.PlayOneShot(audioClip);
                });
            }
            if (null != audioClip)
            {
                BackgroundAudioSource.PlayOneShot(audioClip);
                Debug.Log("播放声音");
            }
        }
        
        private void LoadAudioFiles()
        {
            m_audioKV = JsonFS.Instance.File2Object<Dictionary<string, string>>("AudioClips");
        }

        public void OnInitialize()
        {
            LoadAudioFiles();
        }

        public void OnRelease()
        {

        }

        #region Audio Setting Window

        private bool hasAudio = true;
        private bool hasBackgroundAudio = true;
        private bool hasEffecAudio = true;
        private float audioVolume = 1;
        private float backgroundVolume = 0.8f;
        private float effectAudioVoleme = 1;
        private bool drawAudioWindow = false;
        private void OnGUI()
        {
            if (drawAudioWindow)
            {
                GUILayout.Window(1, new Rect(
                        new Vector2(Screen.width / 2.0f - 120, Screen.height / 2.0f - 80), new Vector2(240, 160)),
                    DrawAudioWindow, "声音管理器");
            }
        }
        public void EnableAudioWindow( bool enable = true )
        {
            drawAudioWindow = enable;
        }
        private void DrawAudioWindow(int windowID)
        {
            hasAudio = GUILayout.Toggle(hasAudio, "主音乐");
            if (hasAudio)
                audioVolume = GUILayout.HorizontalSlider(audioVolume, 0, 1);
            hasBackgroundAudio = GUILayout.Toggle(hasBackgroundAudio, "音乐");
            if(hasBackgroundAudio)
                backgroundVolume = GUILayout.HorizontalSlider(backgroundVolume, 0, 1);
            hasEffecAudio = GUILayout.Toggle(hasEffecAudio, "音效");
            if(hasEffecAudio)
                effectAudioVoleme = GUILayout.HorizontalSlider(effectAudioVoleme, 0, 1);
            if (GUILayout.Button("确定"))
            {
                // 保存到本地记录
                Debug.Log("保存本地");
            }
            if (GUILayout.Button("取消"))
            {
                // 用原有记录覆盖本次操作
            }
        }

        #endregion
    }
}