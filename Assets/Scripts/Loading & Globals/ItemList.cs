using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour {
	//The following code ensures that there is a single global ItemList class available for other objects to use 
	public static ItemList Instance { get; private set; }

	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else {
			Debug.Log ("Warning, there is more than one ItemList class in the scene!");
		}
	}

	public Sprite[] itemPictures;
	public enum Stats {Speed, AttackSpeed, Health};

	[System.NonSerialized] public Item unicornLeg;
	[System.NonSerialized] public Item dryadHeart;
	[System.NonSerialized] public Item ghoulClaw;
	[System.NonSerialized] public Item redPortalStone;
	[System.NonSerialized] public Item bluePortalStone;
	[System.NonSerialized] public Item greenPortalStone;

	void Start() {
		unicornLeg = new Item(false, "Unicorn Leg", itemPictures[1], (int)Stats.Speed, 20, (int)Stats.Health, 10);
		dryadHeart = new Item(false, "Dryad Heart", itemPictures[1], (int)Stats.Health, 20, (int)Stats.AttackSpeed, 10);
		ghoulClaw = new Item(false, "Kraken Claw", itemPictures[1], (int)Stats.AttackSpeed, 20, (int)Stats.Speed, 10);
		redPortalStone = new Item (true, "Red Portal Stone", itemPictures[4]);
		bluePortalStone = new Item (true, "Blue Portal Stone", itemPictures[5]);
		greenPortalStone = new Item (true, "Green Portal Stone", itemPictures[6]);
	}
}
