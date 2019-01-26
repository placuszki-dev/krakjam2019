using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flitting : MonoBehaviour
{
    private PlayerManager playerManager;
    private Vector3 initialPos;
    private Hero hero;
    private bool isAttacking = false;

    private CircleCollider2D attackingArea;

    private Vector2 newDirection;

	public float movingSpeed = 10f;
	public float attackingSpeed = 15f;
    public float attackingRadius = 2f;

    public float rotatingSpeed = 40f;

    public float damage = 1;

    public GameObject body;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        initialPos = body.transform.position;
		hero = FindObjectOfType<Hero>();
        attackingArea = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update ()
    {
        if(Vector2.Distance(body.transform.position, newDirection) < 0.5f)
            UpdateDirection();
 
        if(LookAt2D(newDirection))
            body.transform.position = Vector3.MoveTowards(body.transform.position, newDirection, Time.deltaTime * speed);
    }

    void UpdateDirection() {
        // Get random position inside the attacking area
        float angle = Random.Range(0f, 360f);
        float newRadius = radius * Mathf.Sqrt(Random.Range(0f, 1f));
        newDirection = targetCenter + new Vector3(
            newRadius * Mathf.Cos(angle),
            newRadius * Mathf.Sin(angle),
            0
        );
    }

    bool LookAt2D(Vector3 lookAtPosition)
    {
        float distanceX = lookAtPosition.x - body.transform.position.x;
        float distanceY = lookAtPosition.y - body.transform.position.y;
        float angle = Mathf.Atan2(distanceX, distanceY) * Mathf.Rad2Deg;

        Quaternion endRotation = Quaternion.AngleAxis(angle, Vector3.back);
        body.transform.rotation = Quaternion.Slerp(body.transform.rotation, endRotation, Time.deltaTime * rotatingSpeed);
 
        if(Quaternion.Angle(body.transform.rotation, endRotation) < 1f)
            return true;
 
        return false;
    }


    void GiveDamage() {
        playerManager.GetDamage(damage);
        Invoke("GiveDamage", 1f);
    }

    void OnTriggerEnter2D(Collider2D col) {
		if (col && col.GetComponent<Hero>()) {
			isAttacking = true;
            UpdateDirection();
            Invoke("GiveDamage", 1f);
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col && col.GetComponent<Hero>()) {
            isAttacking = false;
            // Go back to area after stop attacking
            UpdateDirection();
            CancelInvoke("GiveDamage");
		}
	}

    public float speed {
        get {
            return isAttacking ? attackingSpeed : movingSpeed;
        }
    }

    public float radius {
        get {
            return isAttacking ? attackingRadius : attackingArea.radius;
        }
    }

    public Vector3 targetCenter {
        get {
            return isAttacking ? hero.transform.position : attackingArea.transform.position;
        }
    }
}
