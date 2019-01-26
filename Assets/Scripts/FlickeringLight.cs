using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private float timeLeftToFlicker = 0;

    private Light light;
    public float minIntensity = 0.9f;
    public float maxIntensity = 1.1f;
    public float maxRange = 10;
    public float minRange = 10;
    public float minTimeLeftToFlicker = 0.1f;
    public float maxTimeLeftToFlicker = 0.4f;

    void Start()
    {
        light = GetComponent<Light>();
        timeLeftToFlicker = 1f;
    }

    void Update()
    {
        timeLeftToFlicker -= Time.deltaTime;
        if (timeLeftToFlicker < 0f)
        {
            light.intensity = Random.Range(minIntensity, maxIntensity);
            light.range = Random.Range(minRange, maxRange);
            timeLeftToFlicker = Random.Range(minTimeLeftToFlicker, maxTimeLeftToFlicker);
        }
    }
}