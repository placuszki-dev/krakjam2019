using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderEndGame : MonoBehaviour
{
    public GameObject adderMiniGame;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<LadderMiniGameButton>())
        {
            adderMiniGame.GetComponent<LadderMiniGame>().FinishGameFail();
        }
    }
}
