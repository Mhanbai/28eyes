using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackList : MonoBehaviour {
	//The following code ensures that there is a single global ItemList class available for other objects to use 
	public static AttackList Instance { get; private set; }

	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else {
			Debug.Log ("Warning, there is more than one AttackList class in the scene!");
		}
	}

	[SerializeField] GameObject[] projectiles;
	public Attack[] attackType = new Attack[2];

	void Start() {
		attackType [0] 	= new Attack ("Gun", 5.0f, 150.0f, 50.0f, 0, 1, 6, 2.0f, projectiles [0], true, false, false);
		attackType[1] 	= new Attack ("Web", 1.0f, 15.0f, 15.0f, 1, 1, 6, 2.0f, projectiles[1], false, true, false);
	}
}
