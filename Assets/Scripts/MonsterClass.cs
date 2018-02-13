using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterClass : MonoBehaviour {
	Vector3 topLeftBoundary;
	Vector3 bottomLeftBoundary;
	Vector3 topRightBoundary;
	Vector3 bottomRightBoundary;

	Vector3 targetPosition;

	NPCMovement myMovement;
	NPCMovement player;

	int state = 0;

	void Start() {
		myMovement = GetComponent<NPCMovement> ();
		player = GameObject.Find ("Player").GetComponent<NPCMovement> ();
		targetPosition = PickNewTarget ();
	}

	void Update() {
		if (Vector3.Magnitude (transform.position - player.transform.position) > 50.0f) {
			state = 0;
		} else {
			state = 1;
		}

		switch(state) {
		case 0:
			if (Vector3.Magnitude (transform.position - targetPosition) > 2.0f) {
				myMovement.SetSteeringForce (myMovement.Seek (targetPosition));
			} else {
				targetPosition = PickNewTarget ();
			}
			break;
		case 1:
			myMovement.SetSteeringForce (myMovement.Pursue (player));
			break;
		}
	}

	public void TakeHit() {
		GameObject.Destroy (gameObject);
	}

	public void EstablishBoundaries(Vector3 topRight, Vector3 bottomLeft) {
		bottomLeftBoundary = bottomLeft;
		bottomRightBoundary = new Vector3 (topRight.x, 0, bottomLeft.z);
		topLeftBoundary = new Vector3 (bottomLeft.x, 0, topRight.z);
		topRightBoundary = topRight;
	}

	Vector3 PickNewTarget() {
		return new Vector3 (Random.Range (bottomLeftBoundary.x, bottomRightBoundary.x), 0.0f, Random.Range (bottomLeftBoundary.z, topLeftBoundary.z));
	}
}
