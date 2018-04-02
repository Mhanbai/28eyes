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
		GameObject.Find ("Player").GetComponent<CharController> ().headAnimator = heads [partNo].GetComponent<Animator>();
	}

	public void ActivateArms(int partNo) {
		foreach (GameObject part in r_arms) {
			part.SetActive (false);
		}

		foreach (GameObject part in l_arms) {
			part.SetActive (false);
		}

		l_arms [partNo].SetActive (true);
		GameObject.Find ("Player").GetComponent<CharController> ().lArmAnimator = l_arms [partNo].GetComponentInChildren<Animator>();
		r_arms [partNo].SetActive (true);
		GameObject.Find ("Player").GetComponent<CharController> ().rArmAnimator = r_arms [partNo].GetComponentInChildren<Animator>();
		PlayerInfo.Instance.firePoint = GameObject.Find ("R_Hand_IK");
	}

	public void ActivateLegs(int partNo) {
		foreach (GameObject part in r_legs) {
			part.SetActive (false);
		}

		foreach (GameObject part in l_legs) {
			part.SetActive (false);
		}

		l_legs [partNo].SetActive (true);
		GameObject.Find ("Player").GetComponent<CharController> ().lLegAnimator = l_legs [partNo].GetComponentInChildren<Animator>();
		r_legs [partNo].SetActive (true);
		GameObject.Find ("Player").GetComponent<CharController> ().rLegAnimator = r_legs [partNo].GetComponentInChildren<Animator>();
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
