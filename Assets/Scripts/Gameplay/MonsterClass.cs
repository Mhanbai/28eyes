﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterClass : MonoBehaviour {
	public GameObject head;
	public GameObject body;
	public GameObject tail;
	public GameObject fLeg1;
	public GameObject fLeg2;
	public GameObject bLeg1;
	public GameObject bLeg2;

	void Start() {
		head.SetActive(false);
		body.SetActive(false);
		tail.SetActive(false);
		fLeg1.SetActive(false);
		fLeg2.SetActive(false);
		bLeg1.SetActive(false);
		bLeg2.SetActive(false);
	}

	public void TakeHit() {
		GameObject.Destroy (gameObject);
	}

	// Update is called once per frame
	public void UpdateParts (GameObject head_in, GameObject body_in, GameObject tail_in, GameObject f_leg_in, GameObject b_leg_in) {
		GameObject.Instantiate (head_in, head.transform.position, head.transform.rotation, transform);
		GameObject.Instantiate (body_in, body.transform.position, body.transform.rotation, transform); 
		GameObject.Instantiate (tail_in, tail.transform.position, tail.transform.rotation, transform); 
		GameObject.Instantiate (f_leg_in, fLeg1.transform.position, fLeg1.transform.rotation, transform); 
		GameObject.Instantiate (f_leg_in, fLeg2.transform.position, fLeg2.transform.rotation, transform); 
		GameObject.Instantiate (b_leg_in, bLeg1.transform.position, bLeg1.transform.rotation, transform); 
		GameObject.Instantiate (b_leg_in, bLeg2.transform.position, bLeg2.transform.rotation, transform);

		transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
	}
}