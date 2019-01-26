using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stain : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        print("Stain collision with " + col.gameObject.name);
    }
}
