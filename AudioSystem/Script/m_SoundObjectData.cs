using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace MaxDev.Sound
{
    [CreateAssetMenu(fileName = "SoundData", menuName = "Sound/soundListData")]
    public class m_SoundObjectData : ScriptableObject
    {
        public List<m_Sound> SoundList;
    }
}

[System.Serializable]
public class m_Sound
{
    public string Name;
    public SoundType Type;
    public AudioClip SoundClip;
    
    [Range(0f,1f)]
    public float Volume;
    public bool Loop;
}

[System.Serializable]
public enum SoundType
{
    SFX,
    Music
}