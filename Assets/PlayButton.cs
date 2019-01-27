using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("x"))
            SceneManager.LoadScene(1);
    }
}
