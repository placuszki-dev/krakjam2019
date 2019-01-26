using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine

public class Patroling : MonoBehaviour
{
	private Hero hero;
	private GameObject body;
	// Forward means moving towards partolEnd
	private bool isMovingForward = true;
	private bool isAttacking = false;

	private Vector3 initialPos;

	public float moveSpeed = 1f;
	public float attackingSpeed = 2f;

	public bool isPartolling = false;
	public GameObject patrolStart;
	public GameObject patrolEnd;
	
	void Start () {
		body = transform.parent.gameObject;
		initialPos = body.transform.position;
		hero = FindObjectOfType<Hero>();
		rotateTowardsAim((patrolEnd.transform.position - body.transform.position).normalized);
	}

	void Update () {
		if (isAttacking) {
			UpdateAttack();
		} else if (isPartolling) {
			UpdatePatrol();
		} else {
			UpdateGoBack();
		}
	}

	void UpdatePatrol() {
		Vector2 moveDirection = ((isMovingForward ? patrolEnd : patrolStart).transform.position - body.transform.position).normalized * moveSpeed * Time.deltaTime;

		Vector3 newPosition = new Vector3 (body.transform.position.x + moveDirection.x, body.transform.position.y + moveDirection.y, 0);

		Vector2 patrolStartPos = patrolStart.transform.position;
		Vector2 patrolEndPos = patrolEnd.transform.position;

		if (
			// Handle cases when start pos is after end pos
			(patrolStartPos.x > patrolEndPos.x ?
		patrolEndPos.x < newPosition.x && newPosition.x < patrolStartPos.x :
		patrolStartPos.x < newPosition.x && newPosition.x < patrolEndPos.x) &&
		(patrolStartPos.y > patrolEndPos.y ? 
		patrolEndPos.y < newPosition.y && newPosition.y < patrolStartPos.y :
		patrolStartPos.y < newPosition.y && newPosition.y < patrolEndPos.y)
		) {
			body.transform.position = newPosition;
		} else {
			// Change sprite orientation
			rotateTowardsAim(-moveDirection);
			// Change moving direction
			isMovingForward = !isMovingForward;
		}
	}

	void UpdateAttack() {
		Vector2 attackDirection = (hero.transform.position - transform.position).normalized * Time.deltaTime * attackingSpeed;
		body.transform.position = new Vector3(body.transform.position.x + attackDirection.x, body.transform.position.y + attackDirection.y);
		rotateTowardsAim(attackDirection.normalized);
	}

	void UpdateGoBack() {
		Vector2 backDirection = (initialPos - transform.position).normalized * Time.deltaTime * attackingSpeed;
		body.transform.position = new Vector3(body.transform.position.x + backDirection.x, body.transform.position.y + backDirection.y);
	}

	void rotateTowardsAim(Vector2 aim) {
		// Rotate forward aim direction
        float rot_z = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;

		body.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col && col.GetComponent<Hero>()) {
			rotateTowardsAim((hero.transform.position - body.transform.position).normalized);
			isAttacking = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col && col.GetComponent<Hero>()) {
			rotateTowardsAim(
				((isPartolling ? initialPos : (isMovingForward ? patrolEnd : patrolStart).transform.position) - body.transform.position).normalized
				);
			isAttacking = false;
		}
	}
}
