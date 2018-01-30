using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
	//TODO: FIX STRUCTURE BASED ON DESIGN
	bool isObjective = false; 
	string itemName = "default";
	int statToRaise = 0;
	int statToRaisePercent = 0;
	int statToLower = 0;
	int statToLowerPercent = 0;
	public Sprite picture;

	public Item(bool objective_in, string name_in, Sprite picture_in, int statToRaise_in = 0, int raisePercent_in = 0, int statToLower_in = 0, int lowerPercent_in = 0) {
		isObjective = objective_in; 
		itemName = name_in;
		picture = picture_in;
		if (isObjective == false) {
			statToRaise = statToRaise_in;
			statToRaisePercent = raisePercent_in;
			statToLower = statToLower_in;
			statToLowerPercent = statToLower;
		}
	}

	public bool IsObjective() {
		return isObjective;
	}

	public string Name() {
		return itemName;
	}

	public Sprite Picture() {
		return picture;
	}

	public void Use() {
		//TODO: WRITE "USE" FUNCTION
	}
}
