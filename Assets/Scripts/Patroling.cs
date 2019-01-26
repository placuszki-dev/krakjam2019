using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine

public class Patroling : MonoBehaviour
{

	private CircleCollider2D collider;
	public float moveSpeed = 1f;
	public GameObject patrolArea;
	void Start () {
		collider = patrolArea.GetComponent(typeof(CircleCollider2D)) as CircleCollider2D;
	}

	void Update () {
		
		Vector3 newPosition = new Vector3 (transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, 0);

		float start = patrolArea.transform.position.x - collider.radius;
		float end = patrolArea.transform.position.x + collider.radius;

		if (newPosition.x >= start && newPosition.x <= end) {
			transform.position = newPosition;
		} else {
			// Change sprite orientation
			transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			// Change moving direction
			moveSpeed = -moveSpeed;
		}
	}
}
