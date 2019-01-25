using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Hero hero;

    public float speed = 10;
    public Color timerColor;

    void Start()
    {
        hero = FindObjectOfType<Hero>();
    }

    void FixedUpdate()
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
