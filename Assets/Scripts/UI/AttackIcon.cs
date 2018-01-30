using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIcon : MonoBehaviour {
	CharController player;
	public UnityEngine.UI.Image icon;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<CharController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.IsAttackReady () == true) {
			icon.color = Color.red;
		} else {
			icon.color = Color.white;
		}
	}
}
