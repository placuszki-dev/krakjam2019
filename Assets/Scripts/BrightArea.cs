using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightArea : MonoBehaviour
{
    private PlayerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col && col.GetComponent<Hero>())
        {
            playerManager.InBrightArea();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col && col.GetComponent<Hero>())
        {
            playerManager.InDarkArea();
        }
    }
}
