using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    private Transform target;
    public bool lockX = false;

    private Vector3 velocity = Vector3.zero;
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
            newPosition.z = transform.position.z;
            if (lockX)
                newPosition.x = 0;

            //transform.position = Vector3.Slerp(transform.position, newPosition, followSpeed * Time.deltaTime);
            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, followSpeed);
        }
        else
        {
            Debug.LogError("Cant find hero!!!!!");
        }
    }
}
