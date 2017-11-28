
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioEntry : MonoBehaviour
    {
        // 声音文件做一个管理 ，用配置的方式加载进去
        public AudioClip ClickAudio = null;
        public AudioClip ErrorAudio = null;
        public AudioClip TimeEndAudio = null;
        public AudioClip WinAudio = null;
        public AudioSource SoundEffectAudioSource = null;
        public AudioSource WarningAudioSource = null;
        public AudioSource BackgroundAudioSource = null;
        
        public void PlayOneShot( string audioName , bool loop = false)
        {
            // 根据名字从配置表里获取对应的音频文件进行播放
            // todo 这些东西移动到Medaitor层进行管理
            if (audioName == "Error")
            {
                SoundEffectAudioSource.PlayOneShot(ErrorAudio);
            }
            else if (audioName == "Click")
            {
                SoundEffectAudioSource.PlayOneShot(ClickAudio);
            }
            else
            {
                SoundEffectAudioSource.PlayOneShot(WinAudio);
            }
        }

        public void SetSoundEffectVolumn( float volumn )
        {
            SoundEffectAudioSource.volume = volumn;
        }

        public void SetBackgroundAudioVolumn( float volumn )
        {
            BackgroundAudioSource.volume = volumn;
        }
    }
}