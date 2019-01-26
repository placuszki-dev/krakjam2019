using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    internal void changeMusic(AudioClip newMusic)
    {
        audioSource.clip = newMusic;
        audioSource.Play();
    }
}
