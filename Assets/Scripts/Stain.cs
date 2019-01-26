using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stain : MonoBehaviour
{
    private float previousLinearDrag = 0f;
    private float previousAngularDrag = 0f;
    private float previousSpeed = 0f;
    private Hero hero;
    private Rigidbody2D heroRigidBody;

    public float slowSpeed = 5f;
    public float linearDrag = 1f;
    public float angularDrag = 10f;

    void Start() {
        hero = FindObjectOfType<Hero>();
        heroRigidBody = hero.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col && col.GetComponent<Hero>()) {
            previousLinearDrag = heroRigidBody.drag;
            previousAngularDrag = heroRigidBody.angularDrag;
            previousSpeed = hero.speed;
            heroRigidBody.drag = linearDrag;
            heroRigidBody.angularDrag = angularDrag;
            hero.speed = slowSpeed;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col && col.GetComponent<Hero>()) {
            heroRigidBody.drag = previousLinearDrag;
            heroRigidBody.angularDrag = previousAngularDrag;
            hero.speed = previousSpeed;        
        }
    }
}
