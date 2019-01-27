using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    public int damage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Hero>())
        {
            FindObjectOfType<PlayerManager>().GetDamage(damage);
        }
    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();
    }
}
