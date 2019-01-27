using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PillsIndicator : MonoBehaviour
{
    public PlayerManager playerManager;
    private RawImage player1PillImage, player2PillImage;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        player1PillImage = transform.Find("Player1Pill").GetComponent<RawImage>();
        player2PillImage = transform.Find("Player2Pill").GetComponent<RawImage>();
    }

    internal void SetProgress(float progress)
    {
        // ale gunwo
        if (playerManager.GetActivePlayer().name == "Player1")
        {
            player1PillImage.color = new Color(1, 1, 1, 1-progress);
            player2PillImage.color = new Color(1, 1, 1, progress);
        }
        else // Player2 is active
        {
            player2PillImage.color = new Color(1, 1, 1, 1 - progress);
            player1PillImage.color = new Color(1, 1, 1, progress);
        }
    }

    internal void OnePlayerLeft()
    {
        throw new NotImplementedException();
    }
}
