using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int activePlayer;
    public Player[] players;

    void Start()
    {
        activePlayer = 0;
        players[activePlayer].gameObject.SetActive(true);
        players[1].gameObject.SetActive(false);
    }

    public void SwitchPlayers()
    {
        players[activePlayer].gameObject.SetActive(false);

        activePlayer++;
        if (activePlayer >= 2)
            activePlayer = 0;

        players[activePlayer].gameObject.SetActive(true);
        print("Activated player: " + players[activePlayer].name);
    }

    public Player GetActivePlayer()
    {
        return players[activePlayer];
    }


    public void GetDamage(float damage) {
        Debug.Log("Get damage: " + damage);
    }
}
