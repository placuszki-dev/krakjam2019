using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyUp("x"))
            FindObjectOfType<PlayerManager>().SwitchPlayers();
    }
}
