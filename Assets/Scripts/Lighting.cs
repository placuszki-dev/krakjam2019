using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Lighting hit " + other.name);
    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();
    }
}
