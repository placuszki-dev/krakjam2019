using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsSound : MonoBehaviour
{
    public Rigidbody2D relatedRigidBody2D;
    public float minimumVelocityToPlaySteps = 1f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    void Update()
    {
        if(relatedRigidBody2D.velocity.magnitude > minimumVelocityToPlaySteps)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}
