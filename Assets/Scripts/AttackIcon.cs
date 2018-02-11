using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIcon : MonoBehaviour {
	CharController player;
	public UnityEngine.UI.Image icon;
	
	// Update is called once per frame
	void Update () {
		if (PlayerInfo.Instance.IsAttackReady () == true) {
			icon.color = Color.red;
		} else {
			icon.color = Color.white;
		}
	}
}
