using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Stain : MonoBehaviour
{
    private float initialLinearDrag;
    private float initialAngularDrag;
    private float initialSpeed;
    private Hero hero;
    private Rigidbody2D heroRigidBody;

    public float slowSpeed = 5f;
    public float linearDrag = 1f;
    public float angularDrag = 10f;

    void Start() {
        hero = FindObjectOfType<Hero>();
        heroRigidBody = hero.GetComponent<Rigidbody2D>();
        initialLinearDrag = heroRigidBody.drag;
        initialAngularDrag = heroRigidBody.angularDrag;
        initialSpeed = hero.speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col && col.GetComponent<Hero>()) {
            heroRigidBody.drag = linearDrag;
            heroRigidBody.angularDrag = angularDrag;
            hero.speed = slowSpeed;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col && col.GetComponent<Hero>()) {
            heroRigidBody.drag = initialLinearDrag;
            heroRigidBody.angularDrag = initialAngularDrag;
            hero.speed = initialSpeed;        
        }
    }
}
