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
    public Slider timerSlider;

    void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        timeLeft = roundTime;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        Visualize();

        if (timeLeft <= 0)
            OnTimeLeft();
    }

    private void Visualize()
    {
        timerSlider.value = timeLeft / roundTime;
    }

    private void OnTimeLeft()
    {
        Debug.Log("Round time left");
        playerManager.SwitchPlayers();
        restartTimer();
    }

    public void restartTimer() {
        timeLeft = roundTime;
    }
}
