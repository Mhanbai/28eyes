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
    [System.NonSerialized] public Attack[] attackType = new Attack[5];

	void Start() {
									// Name,            Damage, Speed,  Range, Movement, Number, Uses, Relaod Time, Prefab,             Bleed, Poison, SLow
		attackType [0] = new Attack ("Starting Pistol", 0.5f,   150.0f,  30.0f,  0,        1,      6,    1.0f,       projectiles [0],    false, false, false);
		attackType [1] = new Attack ("Web",             30.0f,   25.0f,  15.0f,  1,        1,      8,    2.0f,       projectiles [1],    false, true, true);
		attackType [2] = new Attack ("Crab Claw",       10.0f,   20.0f,  50.0f,  0,        1,      2,    3.0f,       projectiles [2],    false, false, false);
        attackType [3] = new Attack ("Wing",            10.0f,   50.0f,  20.0f,  0,        3,      4,    2.0f,       projectiles [4],    true, false, false);
        attackType [4] = new Attack ("Ultimate",        50.0f,   10.0f,  50.0f,  0,        1,      7,    3.0f,       projectiles [3],    true, true, true);
    }
}
