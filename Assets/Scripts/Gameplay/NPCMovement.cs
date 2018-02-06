using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour {
	protected Vector3 velocityVector;
	protected Vector3 rightVector;
	protected Vector3 forwardVector;
	protected Vector3 steeringForce;

	[SerializeField] protected bool playerControlled;

	[Header("Non Player Controlled Values")]
	[SerializeField] protected float mass;
	[SerializeField] protected float maxSpeed;
	[SerializeField] protected float legPower;
	[SerializeField] protected float turnSpeed;
	[SerializeField] protected float fleeDistance;

	CharController myPlayer;
	NPCMovement player; //Debug
	Vector3 targetInput; //Debug

	void Start() {
		if (playerControlled == true) {
			myPlayer = GetComponentInParent<CharController> ();
		} else {
			fleeDistance = fleeDistance * fleeDistance;
			player = GameObject.Find ("Player(Clone)").GetComponent<NPCMovement> (); //Debug
		}
	}

	void Update() {
		if (playerControlled == false) {
			if (steeringForce.y != 0) {
				steeringForce = new Vector3 (steeringForce.x, 0.0f, steeringForce.z);
			}

			velocityVector = steeringForce / mass;

			if (velocityVector.magnitude > maxSpeed) {
				velocityVector = velocityVector.normalized * maxSpeed;
			}

			if (velocityVector.magnitude > 0.00001f) {
				forwardVector = velocityVector.normalized;
				rightVector = new Vector3 (forwardVector.x, forwardVector.y, -forwardVector.x);
			}

			transform.position += (velocityVector * Time.deltaTime);
		} else {
			forwardVector = myPlayer.GetForwardVector ();
			maxSpeed = myPlayer.GetSpeed ();
		}
	}

	public void CalculateSteering(Vector3 target) {
		steeringForce = Seek(target);
	}

	public void CalculateSteering(NPCMovement target) {
		steeringForce = Pursue(target);
	}

	public Vector3 Seek(Vector3 targetPos) {
		return (Vector3.Normalize (targetPos - transform.position) * maxSpeed) - velocityVector;
	}

	public Vector3 Flee(Vector3 targetPos) {
		if (Vector3.SqrMagnitude (transform.position - targetPos) > fleeDistance) {
			return Vector3.zero;
		}

		return (Vector3.Normalize (transform.position - targetPos) * maxSpeed) - velocityVector;
	}

	public Vector3 Pursue(NPCMovement target) {
		Vector3 distanceToTarget = target.transform.position - transform.position;
		float relativeHeading = Vector3.Dot (forwardVector, target.forwardVector);

		//If difference in direction is less that 18 degrees, simply head towards the target
		if ((Vector3.Dot (forwardVector, distanceToTarget) > 0) && (relativeHeading > 0.9)) {
			return Seek (target.transform.position);
		} else {
			//Calculate seconds ahead to predict
			float predictionTime = distanceToTarget.magnitude / (maxSpeed + target.maxSpeed);
			//Head towards targets current heading
			return Seek (target.transform.position + target.velocityVector * predictionTime);
		}
	}

	public Vector3 Evade(NPCMovement target) {
		Vector3 distanceToTarget = target.transform.position - transform.position;

		float predictionTime = distanceToTarget.magnitude / (maxSpeed + target.maxSpeed);

		return Flee (target.transform.position + (target.velocityVector * predictionTime));
	}
}
