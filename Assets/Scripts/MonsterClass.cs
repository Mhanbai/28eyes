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
	public float deathTimer = 2.0f;

	public float attackStrength = 10.0f;
	public float attackTimer = 0.5f;
	public float attackRange = 5.0f;
	public float delayBetweenAttacks = 1.0f;
	public bool shouldMoveWhileAttacking;

	float attackOngoingTime;
	float attackDelayTime;

	float bleedCounter = 0.0f;
	float poisonCounter = 0.0f;
	int poisonTracker = 0;

	float corpseTimer = 0.0f;

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

	Animator myAnimator;
	SpriteRenderer mySprite;

	void Start() {
		attackOngoingTime = attackTimer;
		myMovement = GetComponent<NPCMovement> ();
		myAnimator = GetComponentInChildren<Animator> ();
		mySprite = GetComponentInChildren<SpriteRenderer> ();
		player = GameObject.Find ("Player").GetComponent<NPCMovement> ();
		targetPosition = PickNewTarget ();
	}

	void Update() {
		if (health > 0.0f) {
			if ((Vector3.Magnitude (transform.position - player.transform.position) > 50.0f) && (state != 2)) {
				state = 0;
			} else if (state != 2) {
				state = 1;
			}

			if (myMovement.GetSteeringForce ().x < 0) {
				mySprite.flipX = true;
			} else {
				mySprite.flipX = false;
			}

			switch (state) {
			case 0:
				if (Vector3.Magnitude (transform.position - targetPosition) > 2.0f) {
					myMovement.SetSteeringForce (myMovement.Seek (targetPosition));
				} else {
					targetPosition = PickNewTarget ();
				}
				break;
			case 1:
				if ((Vector3.Magnitude (transform.position - player.transform.position) >= attackRange) || (attackDelayTime > 0.0f)) {
					myMovement.SetSteeringForce (myMovement.Pursue (player));

					if (attackDelayTime > 0.0f) {
						attackDelayTime -= Time.deltaTime;
					}
				} else {
					state = 2;
				}
				break;
			case 2:
				if (shouldMoveWhileAttacking == false) {
					myMovement.SetSteeringForce (new Vector3 (0.0f, 0.0f, 0.0f));
				} else {
					myMovement.SetSteeringForce (myMovement.Pursue (player));
				}

				if (player.transform.position.x < transform.position.x) {
					mySprite.flipX = true;
				} else {
					mySprite.flipX = false;
				}

				myAnimator.SetBool ("isAttacking", true);
				attackOngoingTime -= Time.deltaTime;

				if (attackOngoingTime < 0.0f) {
					if (Vector3.Magnitude (transform.position - player.transform.position) <= attackRange) {
						PlayerInfo.Instance.TakeHit (attackStrength);
					}
					attackDelayTime = delayBetweenAttacks;
					myAnimator.SetBool ("isAttacking", false);
					attackOngoingTime = attackTimer;
					state = 0;
				}
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

			if (Vector3.Magnitude (myMovement.GetSteeringForce ()) > 0.0f) {
				myAnimator.SetBool ("isRunning", true);
			} else {
				myAnimator.SetBool ("isRunning", false);
			}

		} else {
			myMovement.SetSteeringForce(new Vector3(0.0f, 0.0f, 0.0f));
			mySprite.flipY = true;
			myAnimator.SetBool("isDead", true);
			corpseTimer += Time.deltaTime;

			if (corpseTimer > deathTimer) {
				GameObject.Destroy (gameObject);
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
		} else if ((hit.causesPosion == true) && (poisoned == true)) {
			poisonCounter = 0.0f;
		}

		if ((hit.causesBleed == true) && (bleeding == false)) {
			bleeding = true;
		} else if ((hit.causesBleed == true) && (bleeding == true)) {
			bleedCounter = 0.0f;
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
