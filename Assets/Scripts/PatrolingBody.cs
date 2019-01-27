using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolingBody : MonoBehaviour
{

    private PlayerManager playerManager;

    public int damage = 10;
    void Start()

    {
        playerManager = FindObjectOfType<PlayerManager>();
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
