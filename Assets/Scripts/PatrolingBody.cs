using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolingBody : MonoBehaviour
{

    private PlayerManager playerManager;

    public int damage = 10;
    private Animator animator;

    // Start is called before the first frame update
    void Start()

    {
        playerManager = FindObjectOfType<PlayerManager>();
        animator = GetComponent<Animator>();
    }

    void Hurt()
    {
        if(animator) animator.SetTrigger("attack");
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
