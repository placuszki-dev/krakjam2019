using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Light light;

    void Awake()
    {
        light = GetComponent<Light>();
    }

    public void Switch()
    {
        light.gameObject.SetActive(!light.isActiveAndEnabled);
    }
}
