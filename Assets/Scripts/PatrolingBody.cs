﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolingBody : MonoBehaviour
{

    private PlayerManager playerManager;

    public float damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Hurt()
    {
        playerManager.GetDamage(damage);
        Invoke("Hurt", 1f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col && col.GetComponent<Hero>())
        {
            Hurt();
            GetComponentInChildren<Patroling>().OnAttack();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col && col.GetComponent<Hero>())
        {
            CancelInvoke("Hurt");
            GetComponentInChildren<Patroling>().OnAttackEnd();
        }
    }

}
