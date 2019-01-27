using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroling : MonoBehaviour
{
    private Hero hero;

    private bool isMoving = true;
    // Forward means moving towards partolEnd
    private bool isMovingForward = true;
    private bool isAttacking = false;

    private float currentSpeed;

    private Vector3 initialPos;

    public float rotatingSpeed = 10f;
    public float movingSpeed = 1f;

    public float attackingSpeed = 2f;

    public float accelerationSpeed = 1f;

    public bool isPartolling = false;

    public bool canAttack = true;

    public GameObject body;
    public GameObject patrolStart;
    public GameObject patrolEnd;

    void Start()
    {
        initialPos = body.transform.position;
        hero = FindObjectOfType<Hero>();
    }

    void Update()
    {
        if (!isMoving || !canAttack) return;

        Vector3 targetPos = GetCurrentTargetPosition();

        if (!isPartolling && Vector2.Distance(body.transform.position, targetPos) < 0.2f) return;

        currentSpeed = isAttacking ? Mathf.Min(currentSpeed + Time.deltaTime * accelerationSpeed, attackingSpeed) : movingSpeed;

        if (isAttacking)
        {
            // When attacking look and move even if it is not yet rotated
            LookAt2D(targetPos);
            MoveTowards(targetPos, currentSpeed);
        }
        else if (LookAt2D(targetPos))
        {
            MoveTowards(targetPos, currentSpeed);
        }

        if (!isAttacking && isPartolling)
        {
            UpdatePatrolDirection();
        }
    }

    void MoveTowards(Vector3 target, float speed)
    {
        body.transform.position = Vector3.MoveTowards(body.transform.position, target, Time.deltaTime * currentSpeed);
    }

    Vector3 GetCurrentTargetPosition()
    {
        if (isAttacking) return hero.transform.position;
        if (isPartolling)
        {
            if (isMovingForward) return patrolStart.transform.position;
            return patrolEnd.transform.position;
        }
        return initialPos;
    }

    void UpdatePatrolDirection()
    {
        Vector3 target = (isMovingForward ? patrolStart : patrolEnd).transform.position;

        if (Vector2.Distance(body.transform.position, target) < 0.1f)
        {
            // Change moving direction
            isMovingForward = !isMovingForward;
        }
    }

    bool LookAt2D(Vector3 lookAtPosition)
    {
        float distanceX = lookAtPosition.x - body.transform.position.x;
        float distanceY = lookAtPosition.y - body.transform.position.y;
        float angle = Mathf.Atan2(distanceX, distanceY) * Mathf.Rad2Deg;

        Quaternion endRotation = Quaternion.AngleAxis(angle, Vector3.back);
        body.transform.rotation = Quaternion.Slerp(body.transform.rotation, endRotation, Time.deltaTime * rotatingSpeed);

        if (Quaternion.Angle(body.transform.rotation, endRotation) < 1f)
            return true;

        return false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col && col.GetComponent<Hero>())
        {
            isAttacking = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col && col.GetComponent<Hero>())
        {
            isAttacking = false;
        }
    }

    public void OnAttack()
    {
        Debug.Log("On attack!");
        StopMoving();
        canAttack = false;
        currentSpeed = movingSpeed;
        Invoke("StartMoving", 1);
    }

    public void OnAttackEnd()
    {
        Invoke("CanAttack", 0.1f);
    }

    void CanAttack() {
        canAttack = true;
    }

    void StopMoving()
    {
        isMoving = false;
    }

    void StartMoving()
    {
        isMoving = true;
    }
}
