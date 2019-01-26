using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingLight : MonoBehaviour
{
    public Vector3 rotation;
    public float rotationSpeed = 1;
    public Space space;

    void Update()
    {
        transform.Rotate(rotation * rotationSpeed, space);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Rotating light " + name + " hit " + other.name);
    }
}
