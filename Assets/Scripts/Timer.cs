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
    private PillsIndicator pillsIndicator;

    private bool canPlayChangePlayerEffect = true;
    public float secondsBeforeRoundEndStartChangePlayerAnimation = 2;

    private PostProcessingEffectsManager postProcessingEffectsManager;


    void Awake()
    {
        postProcessingEffectsManager = FindObjectOfType<PostProcessingEffectsManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        pillsIndicator = FindObjectOfType<PillsIndicator>();
        timeLeft = roundTime;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        Visualize();
        print(timeLeft);

        if (timeLeft <= 0)
            OnTimeLeft();

        if (timeLeft <= secondsBeforeRoundEndStartChangePlayerAnimation && canPlayChangePlayerEffect)
        {
            canPlayChangePlayerEffect = false;

            postProcessingEffectsManager.BloomBoom();
            postProcessingEffectsManager.VignetteBoom();
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
        restartTimer();
        canPlayChangePlayerEffect = true;
    }

    public void restartTimer() {
        timeLeft = roundTime;
    }
}
