using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MiniGame { NONE, LADDER };

public class Player : MonoBehaviour
{

    private Hero hero;
    private Rigidbody2D heroRigid;
    private Hand hand;

    public float speed = 10;
    public Color timerColor;

    private MiniGame currentMiniGame = MiniGame.LADDER;


    void Start()
    {
        hero = FindObjectOfType<Hero>();
        heroRigid = hero.GetComponent<Rigidbody2D>();
        hand = FindObjectOfType<Hand>();
    }

    void FixedUpdate()
    {
        if (hero.canMove)
        {
            handleLeftAnalog();
            handleRightAnalog();
        }

        if (currentMiniGame != MiniGame.NONE)
            handleMiniGames();
    }

    private void handleMiniGames()
    {
        if (currentMiniGame == MiniGame.LADDER)
            handleMiniGameLadder();
    }

    private void handleMiniGameLadder()
    {
        if (Input.GetButtonDown(gameObject.name + "Action"))
        {
            LadderMiniGameTarget target = FindObjectOfType<LadderMiniGameTarget>();
            LadderMiniGameButton button = target.GetHeldButton();
            if (button != null)
            {
                print("GOOD");
                Destroy(button.gameObject);
                target.VisualizeGood();
            }
        }
    }

    private void handleLeftAnalog()
    {
        float moveHorizontal = Input.GetAxis(gameObject.name + "Horizontal");
        float moveVertical = Input.GetAxis(gameObject.name + "Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical);
        movement *= speed;

        // Rotate forward gamepad direction
        Vector3 lookAt = movement.normalized;
        float rot_z = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg;

        if (movement.magnitude > 0.00001)
            hero.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        // Move hero
        // MoveWithoutPhysics(movement)
        MoveWithPhysics(movement);
    }

    private void MoveWithPhysics(Vector3 movement)
    {
        heroRigid.AddForce(movement);
    }

    private void MoveWithoutPhysics(Vector3 movement)
    {
        hero.transform.Translate(movement, Space.World);
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
}

