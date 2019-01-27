using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private LadderMiniGame currentMiniGame;

    internal void SetCurrentMiniGame(LadderMiniGame miniGame)
    {
        currentMiniGame = miniGame;
    }

    internal LadderMiniGame GetCurrentMiniGame()
    {
        return currentMiniGame;
    }
}
