using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Hero hero;
    private Hand hand;

    public float speed = 10;
    public Color timerColor;

    void Start()
    {
        hero = FindObjectOfType<Hero>();
        hand = FindObjectOfType<Hand>();
    }

    void FixedUpdate()
    {
        handleLeftAnalog();
        handleRightAnalog();
    }

    private void handleRightAnalog()
    {
        float moveHorizontal = Input.GetAxis(gameObject.name + "HorizontalRight");
        float moveVertical = Input.GetAxis(gameObject.name + "VerticalRight");
        Vector3 dir = new Vector3(moveHorizontal * Time.deltaTime, moveVertical * Time.deltaTime);
        if (dir.magnitude > 0.00001)
        {
            dir = dir.normalized;
            float rot_z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            hand.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
    }

    private void handleLeftAnalog()
    {
        float moveHorizontal = Input.GetAxis(gameObject.name + "Horizontal");
        float moveVertical = Input.GetAxis(gameObject.name + "Vertical");

        Vector3 movement = new Vector3(moveHorizontal * Time.deltaTime, moveVertical * Time.deltaTime);
        movement *= speed;

        // Rotate forward gamepad direction
        Vector3 lookAt = movement.normalized;
        float rot_z = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg;

        if (movement.magnitude > 0.00001)
            hero.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        // Move hero
        hero.transform.Translate(movement, Space.World);
    }
}

