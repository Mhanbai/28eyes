using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartManager : MonoBehaviour {
	int currentBodyNo;
	public GameObject[] bodies;

	// Use this for initialization
	void Start () {
		DeactivateAll ();
	}

	public void ActivateBody(int bodyNo) {
		DeactivateAll ();
		bodies [bodyNo].SetActive (true);
		currentBodyNo = bodyNo;
		GameObject.Find ("Player").GetComponent<CharController> ().bodyAnimator = bodies [bodyNo].GetComponent<Animator>();
	}

	public bool IsBodyActive(int bodyCheck) {
		return bodies [bodyCheck].activeSelf;
	}

	public void ActivateHead(int toActivate) {
		bodies [currentBodyNo].GetComponent<BodyManager>().ActivateHead (toActivate);
	}

	public bool IsHeadActive(int headCheck) {
		return bodies[currentBodyNo].GetComponent<BodyManager>().IsHeadActive(headCheck);
	}

	public void ActivateArms(int toActivate) {
		bodies [currentBodyNo].GetComponent<BodyManager>().ActivateArms (toActivate);
	}

	public bool IsArmsActive(int armCheck) {
		return bodies[currentBodyNo].GetComponent<BodyManager>().IsArmsActive(armCheck);
	}

	public void ActivateLegs(int toActivate) {
		bodies [currentBodyNo].GetComponent<BodyManager>().ActivateLegs (toActivate);
	}

	public bool IsLegsActive(int legCheck) {
		return bodies[currentBodyNo].GetComponent<BodyManager>().IsLegsActive(legCheck);
	}
	
	void DeactivateAll() {
		foreach (GameObject body in bodies) {
			body.SetActive (false);
		}
	}

	public BodyManager GetBody() {
		return bodies [currentBodyNo].GetComponent<BodyManager> ();
	}
}
