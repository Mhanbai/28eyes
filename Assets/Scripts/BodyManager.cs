using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour {
	public GameObject[] heads;
	public GameObject[] r_arms;
	public GameObject[] l_arms;
	public GameObject[] r_legs;
	public GameObject[] l_legs;

	void Start() {
		DeactivateAll ();
	}

	public void ActivateHead(int partNo) {
		foreach (GameObject part in heads) {
			part.SetActive (false);
		}

		heads [partNo].SetActive (true);
	}

	public void ActivateArms(int partNo) {
		foreach (GameObject part in r_arms) {
			part.SetActive (false);
		}

		foreach (GameObject part in l_arms) {
			part.SetActive (false);
		}

		l_arms [partNo].SetActive (true);
		r_arms [partNo].SetActive (true);
	}

	public void ActivateLegs(int partNo) {
		foreach (GameObject part in r_legs) {
			part.SetActive (false);
		}

		foreach (GameObject part in l_legs) {
			part.SetActive (false);
		}

		l_legs [partNo].SetActive (true);
		r_legs [partNo].SetActive (true);
	}

	void DeactivateAll() {
		foreach (GameObject part in heads) {
			part.SetActive (false);
		}

		foreach (GameObject part in r_arms) {
			part.SetActive (false);
		}

		foreach (GameObject part in l_arms) {
			part.SetActive (false);
		}

		foreach (GameObject part in r_legs) {
			part.SetActive (false);
		}

		foreach (GameObject part in l_legs) {
			part.SetActive (false);
		}
	}
}
