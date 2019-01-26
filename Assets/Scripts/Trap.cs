using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private Hero hero;
    public bool isActive = true;

    public float trapDuration = 3f;

    public float damage = 15;

    // Start is called before the first frame update
    void Start()
    {
        hero = FindObjectOfType<Hero>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (isActive && col && col.GetComponent<Hero>())
        {
            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("trap");
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            isActive = false;
            FindObjectOfType<PlayerManager>().GetDamage(damage);
            hero.canMove = false;
            Invoke("ReleaseHero", trapDuration);
        }
    }

    void ReleaseHero() {
        hero.canMove = true;
    }
}
