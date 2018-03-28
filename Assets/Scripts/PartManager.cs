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
	}

	public void ActivateHead(int toActivate) {
		Debug.Log ("activate head");
		bodies [currentBodyNo].GetComponent<BodyManager>().ActivateHead (toActivate);
	}

	public void ActivateArms(int toActivate) {
		bodies [currentBodyNo].GetComponent<BodyManager>().ActivateArms (toActivate);
	}

	public void ActivateLegs(int toActivate) {
		bodies [currentBodyNo].GetComponent<BodyManager>().ActivateLegs (toActivate);
	}
	
	void DeactivateAll() {
		foreach (GameObject body in bodies) {
			body.SetActive (false);
		}
	}
}
