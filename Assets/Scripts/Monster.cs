using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;

public class Monster : MonoBehaviour {
	CharController player;
	//Max speed the monster will move at
	[SerializeField] protected float maxSpeed;

	//Variables to determine wantering behaviour
	[SerializeField] protected float wanderRadius = 20.0f;
	[SerializeField] protected float wanderDistance = 5.0f;
	[SerializeField] protected float wanderVariance = 10.0f;
	[SerializeField] protected float pursueDistance = 25.0f;
	protected float wanderTimer;
	protected Vector3 wanderTarget;

	//Attack settings
	[SerializeField] protected bool shouldMoveWhileAttacking = false;
	[SerializeField] protected float attackRange;

	[SerializeField] float attackLength = 0.5f;
	protected float attackTimer = 0.0f;
	[SerializeField] float delayBetweenAttacks = 1.0f;
	protected float attackDelayTimer = 0.0f;

	//How close the monster will come to an obstacle before avoiding it
	[SerializeField] protected float avoidDistance;
	protected List<Collider> obstacleList = new List<Collider>();

	protected Vector3 velocityVector;
	protected Vector3 rightVector;
	protected Vector3 forwardVector;
	protected Vector3 steeringForce;
	protected Vector3 avoidanceForce;

	float yPos;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<CharController> ();
		yPos = gameObject.GetComponent<Collider> ().bounds.size.y / 2;
		attackRange = GameObject.Find ("Player").GetComponent<Collider> ().bounds.size.x * 2;
		wanderTimer = wanderVariance;
	}
	
	// Update is called once per frame
	void Update () {
		//Reset velocity vector
		velocityVector = new Vector3(0.0f, 0.0f, 0.0f);

		//Find array of obstacles and create line between two furthest collision points
		if (obstacleList.Count > 1) {
			Vector3 avoidanceForce;
			Vector3 pointOne = FindFurthest (transform.position);
			Vector3 pointTwo = FindFurthest (pointOne);
			Vector3 pointToTarget = FindClosest (pointOne, pointTwo);
			if (pointToTarget == pointOne) {
				avoidanceForce = pointOne - pointTwo;
			} else {
				avoidanceForce = pointTwo - pointOne;
			}
			Vector3.Normalize (avoidanceForce);
			avoidanceForce = avoidanceForce;
			wanderTimer = wanderVariance;

			velocityVector += avoidanceForce * Time.deltaTime;
		} else if (obstacleList.Count == 1) {
			Vector3 between = obstacleList [0].transform.position - transform.position;
			between = new Vector3 (between.z, 0.0f, -between.x);
			Vector3.Normalize (between);
			avoidanceForce = between * maxSpeed;
			wanderTimer = wanderVariance;

			velocityVector += avoidanceForce;
		} else {
			Debug.Log(Vector3.Distance (transform.position, player.transform.position) + " / " + attackRange );
			if (Vector3.Distance (transform.position, player.transform.position) < pursueDistance) {
				if (!(Vector3.Distance (transform.position, player.transform.position) < attackRange)) {
					velocityVector += Pursue (player);
				} else {
					if (shouldMoveWhileAttacking == true) {
						velocityVector += Pursue (player);
					}
				}
			} else {
				velocityVector += Seek (Wander ());
			}
		}

		//Add velocity vector to position
		transform.position += velocityVector;

		//Ensure monster is always touching the ground
		transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
	}

	public Vector3 Seek(Vector3 targetPos) {
		if (Vector3.Distance (targetPos, transform.position) < 0.2f) {
			return new Vector3 (0.0f, 0.0f, 0.0f);
		} else {
			return (Vector3.Normalize (targetPos - transform.position) * maxSpeed) - velocityVector;
		}
	}

	public Vector3 Avoid(Vector3 targetPos) {
		return (Vector3.Normalize (transform.position - targetPos) * maxSpeed) - velocityVector;
	}

	public Vector3 Pursue(CharController target) {
		Vector3 distanceToTarget = target.transform.position - transform.position;
		float relativeHeading = Vector3.Dot (forwardVector, target.GetForwardVector());

		//If difference in direction is less that 18 degrees, simply head towards the target
		if ((Vector3.Dot (forwardVector, distanceToTarget) > 0) && (relativeHeading > 0.9)) {
			return Seek (target.transform.position);
		} else {
			//Calculate seconds ahead to predict
			float predictionTime = distanceToTarget.magnitude / (maxSpeed + PlayerInfo.Instance.MaxSpeed());
			//Head towards targets current heading
			return Seek (target.transform.position + target.GetVelocityVector() * predictionTime);
		}
	}

	public Vector3 Wander() {
		if (wanderTimer < wanderVariance) {
			wanderTimer += Time.deltaTime;
		} else {
			Vector2 movement = Random.insideUnitCircle * wanderRadius;
			wanderTarget = transform.position + (new Vector3 (movement.x, 0.0f, movement.y) * wanderDistance);
			wanderTimer = 0.0f;
		}

		return wanderTarget;
	}

	void OnTriggerEnter(Collider obstacle) {
		if ((!obstacle.CompareTag ("Player")) && (!obstacle.CompareTag ("Enemy"))) {
			obstacleList.Add (obstacle);
		}
	}

	void OnTriggerExit(Collider obstacle) {
		obstacleList.Remove (obstacle);
	}

	Vector3 FindFurthest(Vector3 furthestFrom) {
		Vector3 furthestObstacle = furthestFrom;
		float distance = 0.0f;
		foreach (Collider obs in obstacleList) {
			float compareDist = Vector3.Distance (furthestFrom, obs.transform.position);
			if (compareDist > distance) {
				distance = compareDist;
				furthestObstacle = obs.transform.position;
			}
		}

		return furthestObstacle;
	}

	Vector3 FindClosest(Vector3 pointOne, Vector3 pointTwo) {
		Vector3 closestPoint = pointOne;
		float distance = Vector3.Distance (transform.position, pointOne);
		float compareDist = Vector3.Distance (transform.position, pointTwo);
		if (compareDist < distance) {
			closestPoint = pointTwo;
		}

		return closestPoint;
	}
}

/*
 * 		if ((!obstacle.CompareTag ("Player")) && (!obstacle.CompareTag ("Enemy"))) {
			Vector3 point = obstacle.ClosestPointOnBounds (transform.position);
			debugCube.transform.position = point;

			Vector3 between = point - transform.position;
			float proximity = Vector3.Magnitude (between);
			float proportion = 1 / (proximity * 4);

			between = new Vector3 (between.z, 0.0f, -between.x);

			avoidanceForce = between * proportion;
		}
		*/