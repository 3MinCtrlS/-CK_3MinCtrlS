using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUnits : MonoBehaviour
{
    [SerializeField] public AudioSource m_sound;
    [SerializeField] public AudioClip[] m_clips;

    private void Start()
    {
        //m_clips = new AudioClip[10];
    }
    public void PlayAudio() 
    {
        m_sound.Play();
    }

    public void PlayAudioArray(int soundNumber) 
    {
        GetComponent<AudioSource>().clip = m_clips[soundNumber];
        m_sound.Play();
    }
}
