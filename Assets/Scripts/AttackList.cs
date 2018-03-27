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
	public Attack[] attackType = new Attack[6];

	void Start() {
		// Name, Damage, Speed, Range, Movement, Number, Uses, Relaod Time, Prefab, Bleed, Poison, SLow
		attackType [0] 	= new Attack ("Gun", 3.5f, 150.0f, 50.0f, 0, 1, 6, 2.0f, projectiles [0], true, false, false);
		attackType [1] = new Attack ("Web", 1.0f, 15.0f, 15.0f, 1, 1, 6, 2.0f, projectiles [1], false, false, true);
		attackType [2] = new Attack ("Claw", 6.0f, 10.0f, 20.0f, 0, 2, 6, 4.0f, projectiles [2], false, false, false);
        attackType [3] = new Attack("S.Claw", 3.0f, 25.0f, 10.0f, 1, 4, 3, 2.0f, projectiles[3], false, true, true);
        attackType [4] = new Attack("Wing", 1.5f, 25.0f, 10.0f, 1, 3, 2, 2.0f, projectiles[4], true, false, true);
        attackType [5] = new Attack("Cat Claws", 4.5f, 10.0f, 10.0f, 1, 3, 1, 4.0f, projectiles[4], true, false, false);
    }
}
