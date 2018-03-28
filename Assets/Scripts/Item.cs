using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {
	public bool isObjective = false; 
	public string description = "";
	public int picRef;
	public int itemSet;
	public float healthChange;
	public float speedChange;
	public int ammoChange;
	public float reloadChange;
	public float rangeChange;
	public int attackType;
	public int bodyPart;

	public Item(string description_in, bool objective_in, int picRef_in, int itemSet_in = -1, 
		float healthChange_in = 0.0f, float speedChange_in = 0.0f, int ammoChange_in = 0, float reloadChange_in = 0.0f, float rangeChange_in = 0.0f, int attackType_in = -1, int bodyPart_in = -1) {

		isObjective = objective_in; 
		description = description_in;
		picRef = picRef_in;
		itemSet = itemSet_in;
		if (isObjective == false) {
			healthChange = healthChange_in;
			speedChange = speedChange_in;
			ammoChange = ammoChange_in;
			reloadChange = reloadChange_in;
			rangeChange = rangeChange_in;
			attackType = attackType_in;
			bodyPart = bodyPart_in;
		}
	}
}
