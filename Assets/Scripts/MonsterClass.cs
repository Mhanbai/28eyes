using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterClass : MonoBehaviour {
	public float health = 100.0f;
	public float bleedDamage = 0.5f;
	public float bleedTime = 5.0f;
	public float poisonDamage = 10.0f;
	public float poisonFrequency = 5.0f;
	public int poisonHits = 5;

	float bleedCounter = 0.0f;
	float poisonCounter = 0.0f;
	int poisonTracker = 0;

	Vector3 topLeftBoundary;
	Vector3 bottomLeftBoundary;
	Vector3 topRightBoundary;
	Vector3 bottomRightBoundary;

	Vector3 targetPosition;

	NPCMovement myMovement;
	NPCMovement player;

	bool bleeding = false;
	bool poisoned = false;

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

		if (bleeding == true) {
			bleedCounter += Time.deltaTime;
			health -= bleedDamage;

			if (bleedCounter > bleedTime) {
				bleeding = false;
			}
		}

		if (poisoned == true) {
			poisonCounter += Time.deltaTime;

			if (poisonCounter > poisonFrequency) {
				poisonCounter = 0.0f;
				health = health - poisonDamage;
				poisonTracker++;
			}

			if (poisonTracker > poisonHits) {
				poisonTracker = 0;
				poisoned = false;
			}
		}
	}

	void OnTriggerEnter(Collider collision) {
		try {
			ProjectileBehaviour projectile = collision.transform.GetComponent<ProjectileBehaviour> ();
			TakeHit(projectile);
			projectile.Die ();
		} catch { }
	}

	public void TakeHit(ProjectileBehaviour hit) {
		health = health - hit.damage;

		if ((hit.causesPosion == true) && (poisoned == false)) {
			poisoned = true;
		}

		if ((hit.causesBleed == true) && (bleeding == false)) {
			bleeding = true;
		}

		if (hit.causesSlow == true) {
			myMovement.Slow ();
		}
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
