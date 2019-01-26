using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMiniGame : MonoBehaviour
{
    public AudioClip successClip;
    public AudioClip failClip;
    public int damageOnFail = 10;
    public int requiredPointsToWin = 10;

    private int leftPointsToWin;

    Hero hero;

    void Start()
    {
        hero = FindObjectOfType<Hero>();
    }

    void Update()
    {
        print(leftPointsToWin);

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        StartLadderMiniGame();
    }

    private void StartLadderMiniGame()
    {
        FindObjectOfType<Hero>().canMove = false;
        transform.GetChild(0).gameObject.SetActive(true);
        PlayerManager.currentMiniGame = MiniGame.LADDER;
        leftPointsToWin = requiredPointsToWin;
    }

    internal void FinishGameFail()
    {
        DestroyAllButtons();
        PlayerManager.currentMiniGame = MiniGame.NONE;
        print("FinishGameFail");
        FindObjectOfType<Hero>().canMove = true;
        FindObjectOfType<SoundPlayer>().PlaySound(successClip);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        FindObjectOfType<PlayerManager>().GetDamage(damageOnFail);
    }

    internal void FinishGameSuccess()
    {
        DestroyAllButtons();
        PlayerManager.currentMiniGame = MiniGame.NONE;
        print("FinishGameSuccess");
        FindObjectOfType<Hero>().canMove = true;
        FindObjectOfType<SoundPlayer>().PlaySound(failClip);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        hero.transform.position = transform.Find("WinPosition").position;
    }

    private void DestroyAllButtons()
    {
        foreach (LadderMiniGameButton b in FindObjectsOfType<LadderMiniGameButton>())
        {
            print("Destrroing " + b.name);
            Destroy(b.gameObject);
        }
    }

    internal void OnUserActionButtonPressed()
    {
        LadderMiniGameTarget target = transform.Find("UI").Find("Target").GetComponent<LadderMiniGameTarget>();
        LadderMiniGameButton button = target.GetHeldButton();

        if (button != null)
        {
            Destroy(button.gameObject);
            target.VisualizeGood();
            leftPointsToWin--;
            if (leftPointsToWin <= 0)
            {
                FinishGameSuccess();
            }
        }
        else
        {
            FinishGameFail();
            hero.transform.position = transform.Find("LosePosition").position;
        }
    }

    public int GetLeftPointsToWin()
    {
        return leftPointsToWin;
    }
}
