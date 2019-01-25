using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{

    private Transform target;

    public float followSpeed = 3f;

    void Start()
    {
        target = FindObjectOfType<Hero>().transform;
    }

    void Update()
    {
        if (target)
        {
            Vector3 newPosition = target.position;
            newPosition.z = -10;
            transform.position = Vector3.Slerp(transform.position, newPosition, followSpeed * Time.deltaTime);
        }
        else
        {
            Debug.LogError("Cant find hero!!!!!");
        }
    }
}
