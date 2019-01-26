using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMiniGameButton : MonoBehaviour
{
    public float moveSpeed = 1f;

    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }

    internal void SetSpeed(float buttonMoveSpeed)
    {
        moveSpeed = buttonMoveSpeed;
    }
}
