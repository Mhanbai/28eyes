using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterClass : MonoBehaviour {
	public SpriteRenderer head;
	public SpriteRenderer body;
	public SpriteRenderer tail;
	public SpriteRenderer fLeg1;
	public SpriteRenderer fLeg2;
	public SpriteRenderer bLeg1;
	public SpriteRenderer bLeg2;

	// Update is called once per frame
	public void UpdateParts (GameObject head_in, GameObject body_in, GameObject tail_in, GameObject f_leg_in, GameObject b_leg_in) {
		head.sprite = head_in.GetComponent<SpriteRenderer> ().sprite;
		body.sprite = body_in.GetComponent<SpriteRenderer> ().sprite;
		tail.sprite = tail_in.GetComponent<SpriteRenderer> ().sprite;
		fLeg1.sprite = f_leg_in.GetComponent<SpriteRenderer> ().sprite;
		fLeg2.sprite = f_leg_in.GetComponent<SpriteRenderer> ().sprite;
		bLeg1.sprite = b_leg_in.GetComponent<SpriteRenderer> ().sprite;
		bLeg2.sprite = b_leg_in.GetComponent<SpriteRenderer> ().sprite;
	}
}
