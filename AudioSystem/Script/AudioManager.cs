using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Audio;

namespace MaxDev.Sound
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        ///-soundData
        [Header("SoundListData")]
        [SerializeField] private List<m_SoundObjectData> SoundListData;

        [Header("Setup-SoundOutput")]
        [SerializeField] private AudioSource outputSFX;
        [SerializeField] private AudioSource outputMusic;
    
        //-For Create New SoundObject
        [Header("Prefab-SoundObject")]
        [SerializeField]private GameObject SoundObject;

        /// ---AudioService
        private IAudioInterface audioService = new m_AudioServiec();
        public ScriptableMixerVolumeData mixerVolumeData;

        /// ---Mixer
        /*public float volumeMusic = 0.5f;
        public float volumeSFX = 0.5f;*/
        
        //--Debug
        public bool debugThis;
    
        #region Unity Funtion

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                ///--Add SoundListData to Instance
                foreach (m_SoundObjectData soundObjectData in SoundListData)
                {
                    instance.SoundListData.Add(soundObjectData);
                }
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }

        #endregion
        #region public Funtion

        public void PlaySound(string soundName)
        {
            m_Sound sound = FindSoundInList(soundName);
            if (sound == null)
                return;
            OutputSound(sound);
        }

        public void PlaySoundAtPosition(string soundName, Vector3 audioPosition, float soundRange)
        {
            m_Sound sound = FindSoundInList(soundName);
            if (sound == null)
                return;
            CreateSoundObject(sound, audioPosition, soundRange);
        }

        public void StopSound(string soundName)
        {
            m_Sound sound = FindSoundInList(soundName);
            if (sound == null)
                return;
            StopPlaySound(sound);
        }

        public void StopSoundFromOutput(SoundType outputType)
        {
            switch (outputType)
            {
                case SoundType.SFX:
                    ///--Stop outputSFX
                    outputSFX.Stop();
                    break;
                case SoundType.Music:
                    ///--Stop outputMusic
                    outputMusic.Stop();
                    break;
            }
        }

        #endregion
        #region private Funtion

        private m_Sound FindSoundInList(string soundName)
        {
            m_Sound soundData = new m_Sound();
            foreach (m_SoundObjectData soundObjectDataData in SoundListData)
            {
                soundData = audioService.FindSoundInListData(soundName, soundObjectDataData);
                if (soundData != null)
                    break;
            }

            if (soundData == null)
                Debug.LogError("No "+soundName+" In SoundListData");

            return soundData;
        }
    
        private void OutputSound(m_Sound sound)
        {
            switch (sound.Type)
            {
                case SoundType.SFX:
                    ///--Setup AudioSourc before play
                    outputSFX.clip = sound.SoundClip;
                    outputSFX.volume = sound.Volume;
                    outputSFX.loop = sound.Loop;
                
                    ///--Play AudioSourc
                    outputSFX.Play();
                    Log(sound.Name + "- Is playing On " + outputMusic.name);
                    break;
                case SoundType.Music:
                    ///--Setup AudioSourc before play
                    outputMusic.clip = sound.SoundClip;
                    outputMusic.volume = sound.Volume;
                    outputMusic.loop = sound.Loop;
                
                    ///--Play AudioSourc
                    outputMusic.Play();
                    Log(sound.Name + "- Is playing On " + outputMusic.name);
                    break;
            }
        }

        private void CreateSoundObject(m_Sound sound, Vector3 audioPosition, float soundRange)
        {
            if (SoundObject == null || SoundObject.GetComponent<AudioSource>() == null)
            {
                Debug.LogError("SoundObject Not Add In AudioManager or SoundObject Prefab not have 'AudioSource' component");
                return;
            }
        
            GameObject newSoundObject = Instantiate(SoundObject, audioPosition, Quaternion.identity);
        
            ///--Setup Audio Output
            switch (sound.Type)
            {
                case SoundType.SFX:
                    newSoundObject.GetComponent<AudioSource>().outputAudioMixerGroup = outputSFX.outputAudioMixerGroup;
                    break;
                case SoundType.Music:
                    newSoundObject.GetComponent<AudioSource>().outputAudioMixerGroup = outputMusic.outputAudioMixerGroup;
                    break;
            }
            ///--Setup AudioSourc before play
            newSoundObject.GetComponent<AudioSource>().clip = sound.SoundClip;
            newSoundObject.GetComponent<AudioSource>().volume = sound.Volume;
            newSoundObject.GetComponent<AudioSource>().loop = sound.Loop;
            newSoundObject.GetComponent<AudioSource>().maxDistance = soundRange;
        
            ///--Play AudioSourc
            newSoundObject.GetComponent<AudioSource>().Play();
            Log(newSoundObject.name + " is playing sound '" + sound.Name + "' on " + newSoundObject.GetComponent<AudioSource>().outputAudioMixerGroup + " output");

            if (sound.Loop == false)
            {
                Destroy(newSoundObject, sound.SoundClip.length);
            }
        }

        private void StopPlaySound(m_Sound sound)
        {
            ///--Stop Play Audio by output type
            switch (sound.Type)
            {
                case SoundType.SFX:
                    ///-Check sound output have soundClip
                    if (outputSFX.clip == null)
                    {
                        Log("No sound is playing in " + outputSFX.name);
                        return;
                    }
                        
                    if (sound.SoundClip == outputSFX.clip)
                        outputSFX.Stop();
                    else
                    {
                        LogWarning("Can't Stop Play '" + sound.Name + "' whild other sound is playing");
                        LogWarning(outputSFX.clip.name + "- Currently play In " + outputSFX.name);
                        LogWarning("Please use correct sound name to stop");
                    }
                    break;
                case SoundType.Music:
                    ///-Check sound output have soundClip
                    if (outputMusic.clip == null)
                    {
                        Log("No sound is playing in " + outputMusic.name);
                        return;
                    }
                    
                    if (sound.SoundClip == outputMusic.clip)
                        outputMusic.Stop();
                    else
                    {
                        LogWarning("Can't Stop Play '" + sound.Name + "' whild other sound is playing");
                        LogWarning(outputMusic.clip.name + "- Currently play In " + outputMusic.name);
                        LogWarning("Please use correct sound name to stop");
                    }
                    break;
            }
        }

        private void Log(string log)
        {
            if (debugThis) 
                Debug.Log(log);
        }

        private void LogWarning(string log)
        {
            if (debugThis) 
                Debug.LogWarning(log);
        }
        
    
        #endregion
    }
}