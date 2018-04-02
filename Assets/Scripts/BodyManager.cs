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

	public bool IsHeadActive(int headCheck){
		return heads [headCheck].activeSelf;
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

	public bool IsArmsActive(int armCheck){
		return (r_arms [armCheck].activeSelf && l_arms[armCheck].activeSelf);
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

	public bool IsLegsActive(int legCheck){
		return (r_legs [legCheck].activeSelf && l_legs[legCheck].activeSelf);
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
