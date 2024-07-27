using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "SoundData", menuName = "Sound/soundSetting")]
public class ScriptableMixerVolumeData : ScriptableObject
{
    public float MusicVolume;
    public float SFXVolume;
}
