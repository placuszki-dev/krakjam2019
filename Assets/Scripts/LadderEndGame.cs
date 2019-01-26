using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderEndGame : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<LadderMiniGameButton>())
        {
            FindObjectOfType<LadderMiniGame>().FinishGameFail();
        }
    }
}
