using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {
	public Explosion explosion;
	public bool causesBleed;
	public bool causesSlow;
	public bool causesPosion;
	public float speed;
	public Vector3 direction;
	public Vector3 startingLocation;
	public int trajectoryType;
	public float range;
	public float damage;
	float distance;

	//Additional variables for calculating point on arc
	float angle = 180.0f;
	float circumferenceDistancePerSecond;
	float unitsToMove;

	SpriteRenderer mySprite;

	void Start() {
		mySprite = gameObject.GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance (new Vector3(transform.position.x, 0.0f, transform.position.z), 
											new Vector3(startingLocation.x, 0.0f, startingLocation.z));

		switch (trajectoryType) {
		case 0:
			transform.position += direction * speed * Time.deltaTime;
			break;
		case 1:
			circumferenceDistancePerSecond = range / (speed * Time.deltaTime);
			unitsToMove = 180.0f / circumferenceDistancePerSecond;

			float yPos = startingLocation.y + (range / 2) * Mathf.Sin (angle / 57.295779513f);
			angle = angle - unitsToMove;

			transform.position += direction * speed * Time.deltaTime;
			transform.position = new Vector3 (transform.position.x, yPos, transform.position.z);
			break;
		}

		if (direction.x < 0) {
			mySprite.flipX = true;
		}

		if (distance > range) {
			Die();
		}
	}

	public void Die() {
		if (explosion != null) {
			Explosion exp = GameObject.Instantiate (explosion);
			exp.transform.position = gameObject.transform.position + new Vector3(0.0f, exp.GetComponentInChildren<Collider>().bounds.size.y / 2, 0.0f);
		}
		GameObject.Destroy (gameObject);
	}
}
