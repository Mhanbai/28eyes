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

	public Attack[] attackType;
	[SerializeField] Sprite[] projectiles;

	void Start() {
		attackType[0] = new Attack ("Gun", 5.0f, 20.0f, 1, 6, 2.0f, projectiles[0], false, false, false);
	}
}
