using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
	//TODO: FIX STRUCTURE BASED ON DESIGN
	bool isObjective = false; 
	string itemName = "default";
	string description = "";
	Sprite picture;
	int itemSet;


	public Item(bool objective_in, string name_in, Sprite picture_in, int itemSet_in) {
		isObjective = objective_in; 
		itemName = name_in;
		picture = picture_in;
		itemSet = itemSet_in;
		if (isObjective == false) {
			//Assign stats
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
