using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
	private string[] playerStats = 
	{
		" speed by ",		//0
		" attack speed by ",	//1
		" health by ",		//2
	};

	bool isObjective = false; 
	string itemName = "default";
	int statToRaise = 0;
	int statToRaisePercent = 0;
	int statToLower = 0;
	int statToLowerPercent = 0;
	string description;
	public Sprite picture;

	public Item(bool objective_in, string name_in, Sprite picture_in, int statToRaise_in = 0, int raisePercent_in = 0, int statToLower_in = 0, int lowerPercent_in = 0) {
		isObjective = objective_in; 
		itemName = name_in;
		picture = picture_in;
		if (isObjective == false) {
			description = "Consuming will increase" + playerStats[statToRaise] + statToRaisePercent.ToString () + "%, and lower" + playerStats[statToLower] + statToLowerPercent.ToString () + "%";
			statToRaise = statToRaise_in;
			statToRaisePercent = raisePercent_in;
			statToLower = statToLower_in;
			statToLowerPercent = statToLower;
		} else {
			description = "An essential component required to open a portal out of here!";
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

	public string Description() {
		return description;
	}
}
