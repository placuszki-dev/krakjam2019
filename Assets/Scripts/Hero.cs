using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public float speed = 60;
    public bool canMove = true;

    private Rigidbody2D rigid;
    private Animator animator;

    public float animationRunningMinimumVelocity = 0.1f;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("velocity", rigid.velocity.sqrMagnitude);
    }
}
