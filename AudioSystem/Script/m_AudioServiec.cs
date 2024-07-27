using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaxDev.Sound
{
    public interface IAudioInterface
    {
        public void PlayAudio(string audioName);

        public void PlayAudio(string audioName, Vector3 audioPosition, float soundRange);

        public void StopAudio(string audioName);

        public void StopAudioOutput(SoundType outputType);

        public m_Sound FindSoundInListData(string audioName, m_SoundObjectData soundListData);
    }

    public class m_AudioServiec : IAudioInterface
    {
        public void PlayAudio(string audioName)
        {
            CheckSoundManagerInstance();
            AudioManager.instance.PlaySound(audioName);
        }

        public void PlayAudio(string audioName, Vector3 audioPosition, float soundRange)
        {
            CheckSoundManagerInstance();
            AudioManager.instance.PlaySoundAtPosition(audioName, audioPosition, soundRange);
        }

        public void StopAudio(string audioName)
        {
            CheckSoundManagerInstance();
            AudioManager.instance.StopSound(audioName);
        }

        public void StopAudioOutput(SoundType outputType)
        {
            CheckSoundManagerInstance();
            AudioManager.instance.StopSoundFromOutput(outputType);
        }

        public m_Sound FindSoundInListData(string audioName, m_SoundObjectData soundListData)
        {
            m_Sound sound = soundListData.SoundList.Find(m_Sound => m_Sound.Name == audioName);
            if (sound == null)
            {
                Debug.LogError("No '" + audioName + "' In "+ soundListData.name +" SoundListData");
                return null;
            }

            return sound;
        }

        void CheckSoundManagerInstance()
        {
            if (AudioManager.instance == null)
            {
                Debug.LogError("No SoundManager In This Scene");
                return;
            }
        }
        
    }
}