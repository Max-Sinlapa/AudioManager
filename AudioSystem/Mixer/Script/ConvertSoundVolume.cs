using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvertSoundVolume : MonoBehaviour
{
    [Header("Data")]
    public ScriptableMixerVolumeData mixerVolumeData;

    [Header("Silder UI")]
    public Slider UiSilderMusic;
    public Slider UiSilderSFX;

    private void Update()
    {
        ConvertMusicVolume();
        ConvertSFXVolume();
    }

    private void ConvertMusicVolume()
    {
        mixerVolumeData.MusicVolume = UiSilderMusic.value;
    }

    private void ConvertSFXVolume()
    {
        mixerVolumeData.SFXVolume = UiSilderSFX.value;
    }


}
