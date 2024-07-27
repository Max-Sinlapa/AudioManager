using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Audio;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace MaxDev.Sound
{
    public class SoundMixerManager : MonoBehaviour
    {
        [SerializeField]public AudioMixer AudioMixer;
        public ScriptableMixerVolumeData mixerVolumeData;

        [Header("Silder UI")]
        [SerializeField]public Slider UiSilderMusic;
        [SerializeField]public Slider UiSilderSFX;

        #region UnityFuntion

        private void OnEnable()
        {
            if (UiSilderMusic != null)
                UiSilderMusic.value = AudioManager.instance.mixerVolumeData.MusicVolume;
            if (UiSilderSFX != null)
                UiSilderSFX.value = AudioManager.instance.mixerVolumeData.SFXVolume;
           
            
        }

        private void Awake()
        {
            AudioMixer.SetFloat("MixerSFXVolume" , Mathf.Log10(mixerVolumeData.SFXVolume) * 20f);
            AudioMixer.SetFloat("MixerMusicVolume" , Mathf.Log10(mixerVolumeData.MusicVolume) * 20f);
        }

        #endregion
        

        /// <summary>
        /// Set Slider Range 0.0001 to 1 
        /// </summary>

        #region PublicFuntion

        public void SetMasterVolume(float sliderValue)
        {
            AudioMixer.SetFloat("MixerMasterVolume" , Mathf.Log10(sliderValue) * 20f);
        }
    
        public void SetSFXVolume(float sliderValue)
        {
            AudioMixer.SetFloat("MixerSFXVolume" , Mathf.Log10(sliderValue) * 20f);
            AudioManager.instance.mixerVolumeData.SFXVolume = sliderValue;
        }
    
        public void SetMusicVolume(float sliderValue)
        {
            AudioMixer.SetFloat("MixerMusicVolume" , Mathf.Log10(sliderValue) * 20f);
            AudioManager.instance.mixerVolumeData.MusicVolume = sliderValue;
        }

        #endregion
    }
}