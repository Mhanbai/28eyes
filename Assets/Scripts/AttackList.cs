﻿using System.Collections;
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
    [System.NonSerialized] public Attack[] attackType = new Attack[6];

	void Start() {
									// Name,            Damage, Speed,  Range, Movement, Number, Uses, Relaod Time, Prefab,             Bleed, Poison, SLow
		attackType [0] = new Attack ("Starting Pistol",  5.0f,  150.0f,  30.0f,  0,        1,      6,    1.0f,       projectiles [0],    false, false, false);
		attackType [1] = new Attack ("Web",             12.0f,   25.0f,  15.0f,  1,        1,      8,    1.5f,       projectiles [1],    false, true, true);
		attackType [2] = new Attack ("Crab Claw",       10.0f,   20.0f,  50.0f,  0,        1,      2,    3.0f,       projectiles [2],    false, false, false);
        attackType [3] = new Attack ("Wing",            10.0f,   50.0f,  20.0f,  0,        3,      3,    1.5f,       projectiles [4],    true, false, false);
        attackType [4] = new Attack ("Ultimate",        90.0f,   10.0f,  50.0f,  0,        1,      1,    0.8f,       projectiles [3],    true, true, true);
        attackType [5] = new Attack ("Cat Claw",        10.0f,   14.0f,   3.0f,  0,        2,      4,    2.0f,       projectiles [5],    true, false, false);
    }
}
