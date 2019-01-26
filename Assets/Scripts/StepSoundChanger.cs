using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSoundChanger : MonoBehaviour
{
    public AudioClip stepsAudioClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (stepsAudioClip)
            FindObjectOfType<Hero>().transform.GetComponentInChildren<StepsSound>().GetComponent<AudioSource>().clip = stepsAudioClip; // long line...
    }
}
