using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMiniGame : MonoBehaviour
{
    public AudioClip successClip;
    public AudioClip failClip;
    public int damageOnFail = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        StartLadderMiniGame();
    }

    private void StartLadderMiniGame()
    {
        FindObjectOfType<Hero>().canMove = false;
        transform.GetChild(0).gameObject.SetActive(true);
        PlayerManager.currentMiniGame = MiniGame.LADDER;
    }

    internal void FinishGameFail()
    {
        PlayerManager.currentMiniGame = MiniGame.NONE;
        print("FinishGameFail");
        FindObjectOfType<Hero>().canMove = true;
        FindObjectOfType<SoundPlayer>().PlaySound(successClip);
        gameObject.SetActive(false);
        FindObjectOfType<PlayerManager>().GetDamage(damageOnFail);
    }

    internal void FinishGameSuccess()
    {
        PlayerManager.currentMiniGame = MiniGame.NONE;
        print("FinishGameSuccess");
        FindObjectOfType<Hero>().canMove = true;
        FindObjectOfType<SoundPlayer>().PlaySound(failClip);
        gameObject.SetActive(false);
    }
}
