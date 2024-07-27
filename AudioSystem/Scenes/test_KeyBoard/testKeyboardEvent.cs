using System;
using System.Collections;
using System.Collections.Generic;
using MaxDev.Sound;
using UnityEngine;
using UnityEngine.Events;

public class testKeyboardEvent : MonoBehaviour
{
    public KeyCode MyKeyCode;
    public KeyCode MyKeyCode_2;

    public UnityEvent A_Event;
    public UnityEvent B_Event;

    private IAudioInterface AudioServiec = new m_AudioServiec();

    private void Start()
    {
        //AudioServiec.PlayAudio("DICE");
        //AudioServiec.PlayAudio("CoinSFX", transform.position, 10f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            A_Event.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            B_Event.Invoke();
        }

        if (Input.GetKeyDown(MyKeyCode))
        {
            AudioServiec.PlayAudio("DICE");
        }
        if (Input.GetKeyDown(MyKeyCode_2))
        {
            AudioServiec.PlayAudio("CoinSFX");
        }
    }
}
