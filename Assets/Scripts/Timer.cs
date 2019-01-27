using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float roundTime = 10f; // In seconds
    private float timeLeft;

    private PlayerManager playerManager;

    public PillsIndicator pillsIndicator;
    public RectTransform timerFill;

    private bool canPlayChangePlayerEffect = true;
    public float secondsBeforeRoundEndStartChangePlayerAnimation = 2;

    void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        pillsIndicator = FindObjectOfType<PillsIndicator>();
        timeLeft = roundTime;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        Visualize();

        if (timeLeft <= 0)
            OnTimeLeft();

        if (timeLeft <= secondsBeforeRoundEndStartChangePlayerAnimation && canPlayChangePlayerEffect)
        {
            canPlayChangePlayerEffect = false;
        }
    }

    private void Visualize()
    {
        pillsIndicator.SetProgress(timeLeft / roundTime);
    }

    internal void DisableTimer()
    {
        gameObject.SetActive(false);
        pillsIndicator.OnePlayerLeft();
    }

    private void OnTimeLeft()
    {
        Debug.Log("Round time left");
        playerManager.SwitchPlayers();
        ChangeTimerFillColor();
        restartTimer();
        canPlayChangePlayerEffect = true;
    }

    private void ChangeTimerFillColor()
    {
        timerFill.GetComponent<Image>().color = playerManager.GetActivePlayer().timerColor;
    }

    public void restartTimer() {
        timeLeft = roundTime;
    }
}
