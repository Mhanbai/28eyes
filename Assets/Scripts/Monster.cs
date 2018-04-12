using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
	CharController player;
	//Max speed the monster will move at
	[SerializeField] protected float maxSpeed;

	//Variables to determine wantering behaviour
	[SerializeField] protected float wanderRadius = 20.0f;
	[SerializeField] protected float wanderDistance = 5.0f;
	[SerializeField] protected float wanderVariance = 10.0f;
	protected float wanderTimer;
	protected Vector3 wanderTarget;

	//How close the monster will come to an obstacle before avoiding it
	[SerializeField] protected float avoidDistance;

	protected Vector3 velocityVector;
	protected Vector3 rightVector;
	protected Vector3 forwardVector;
	protected Vector3 steeringForce;
	protected Vector3 avoidanceForce;

	float yPos;

	GameObject debugCube;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<CharController> ();
		yPos = gameObject.GetComponent<Collider> ().bounds.size.y / 2;
		wanderTimer = wanderVariance;
		debugCube = GameObject.CreatePrimitive (PrimitiveType.Cube);
	}
	
	// Update is called once per frame
	void Update () {
		//Reset velocity vector
		velocityVector = new Vector3(0.0f, 0.0f, 0.0f);
		//Add forces to velocity vector
		velocityVector += Seek (Wander ());
		velocityVector += avoidanceForce;

		//Add velocity vector to position
		transform.position += velocityVector;

		//Ensure monster is always touching the ground
		transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
	}

	public Vector3 Seek(Vector3 targetPos) {
		return (Vector3.Normalize (targetPos - transform.position) * maxSpeed) - velocityVector;
	}

	public Vector3 Avoid(Vector3 targetPos) {
		if (Vector3.SqrMagnitude (transform.position - targetPos) > avoidDistance) {
			return Vector3.zero;
		}

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

		return wanderTarget - transform.position;
	}

	void OnTriggerStay(Collider obstacle) {
		if ((!obstacle.CompareTag ("Player")) && (!obstacle.CompareTag ("Enemy"))) {
			Vector3 point = obstacle.ClosestPointOnBounds (transform.position);
			debugCube.transform.position = point;

			Vector3 between = point - transform.position;
			float proximity = Vector3.Magnitude (between);
			float proportion = 1 / (proximity * 2);

			between = new Vector3 (between.z, 0.0f, -between.x);

			avoidanceForce = between * proportion;
		}
	}
}
