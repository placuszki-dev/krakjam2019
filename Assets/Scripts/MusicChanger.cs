using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    public AudioClip newMusic;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (newMusic)
            FindObjectOfType<MusicManager>().changeMusic(newMusic);
    }
}
