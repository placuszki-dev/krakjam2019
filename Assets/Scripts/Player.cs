using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;


public class Player : MonoBehaviour
{
    private Hero hero;
    private Rigidbody2D heroRigid;
    private Hand hand;
    public XboxController controller;

    public Color timerColor;

    private PlayerManager playerManager;
    private int health = 100;

    void Start()
    {
        hero = FindObjectOfType<Hero>();
        heroRigid = hero.GetComponent<Rigidbody2D>();
        hand = FindObjectOfType<Hand>();
        playerManager = FindObjectOfType<PlayerManager>();
        XCI.DEBUG_LogControllerNames();
    }

    void FixedUpdate()
    {
        if (hero.canMove)
        {
            handleLeftAnalog();
            //handleRightAnalog();
        }
    }

    void Update()
    {
        handleMiniGames();
    }

    private void handleMiniGames()
    {
        if (PlayerManager.currentMiniGame == MiniGame.LADDER)
            handleMiniGameLadder();
    }

    private void handleMiniGameLadder()
    {
        if (XCI.GetButtonDown(XboxButton.A, controller)
            || (Input.GetKeyDown("z") && playerManager.GetActivePlayer().name == "Player1")
            || (Input.GetKeyDown("m") && playerManager.GetActivePlayer().name == "Player2"))
        {
            print("ACTION");
            LadderMiniGame miniGame = FindObjectOfType<LadderMiniGame>();
            miniGame.OnUserActionButtonPressed();
        }
    }

    internal void GetDamage(int damage)
    {
        health -= damage;
    }

    private void handleLeftAnalog()
    {
        float moveHorizontal = XCI.IsPluggedIn(controller) ? XCI.GetAxis(XboxAxis.LeftStickX, controller) : 0;
        float moveVertical = XCI.IsPluggedIn(controller) ? XCI.GetAxis(XboxAxis.LeftStickY, controller) : 0;

        if (Input.GetKey("a") && playerManager.GetActivePlayer().name == "Player1") moveHorizontal = -1;
        if (Input.GetKey("d") && playerManager.GetActivePlayer().name == "Player1") moveHorizontal = 1;
        if (Input.GetKey("s") && playerManager.GetActivePlayer().name == "Player1") moveVertical = -1;
        if (Input.GetKey("w") && playerManager.GetActivePlayer().name == "Player1") moveVertical = 1;

        if (Input.GetKey("j") && playerManager.GetActivePlayer().name == "Player2") moveHorizontal = -1;
        if (Input.GetKey("l") && playerManager.GetActivePlayer().name == "Player2") moveHorizontal = 1;
        if (Input.GetKey("k") && playerManager.GetActivePlayer().name == "Player2") moveVertical = -1;
        if (Input.GetKey("i") && playerManager.GetActivePlayer().name == "Player2") moveVertical = 1;


        Vector3 movement = new Vector3(moveHorizontal, moveVertical);
        movement *= hero.speed;

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

