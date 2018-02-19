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

	public GameObject pickup;

	public Sprite[] itemPictures;
	public enum Stats {Speed, AttackSpeed, Health};

	public Item[] portalOneItems = new Item[1];
	public Item[] portalTwoItems = new Item[1];
	public Item[] portalThreeItems = new Item[1];
	public Item[] levelOneItems = new Item[3];
	public Item[] levelTwoItems = new Item[3];
	public Item[] levelThreeItems = new Item[3];

	void Start() {
		levelOneItems[0] = new Item(false, "Unicorn Leg", itemPictures[1], (int)Stats.Speed, 20, (int)Stats.Health, 10);
		levelOneItems[1] = new Item(false, "Dryad Heart", itemPictures[1], (int)Stats.Health, 20, (int)Stats.AttackSpeed, 10);
		levelOneItems[2] = new Item(false, "Kraken Claw", itemPictures[1], (int)Stats.AttackSpeed, 20, (int)Stats.Speed, 10);

		levelTwoItems[0] = new Item(false, "Unicorn Leg", itemPictures[1], (int)Stats.Speed, 20, (int)Stats.Health, 10);
		levelTwoItems[1] = new Item(false, "Dryad Heart", itemPictures[1], (int)Stats.Health, 20, (int)Stats.AttackSpeed, 10);
		levelTwoItems[2] = new Item(false, "Kraken Claw", itemPictures[1], (int)Stats.AttackSpeed, 20, (int)Stats.Speed, 10);

		levelThreeItems[0] = new Item(false, "Unicorn Leg", itemPictures[1], (int)Stats.Speed, 20, (int)Stats.Health, 10);
		levelThreeItems[1] = new Item(false, "Dryad Heart", itemPictures[1], (int)Stats.Health, 20, (int)Stats.AttackSpeed, 10);
		levelThreeItems[2] = new Item(false, "Kraken Claw", itemPictures[1], (int)Stats.AttackSpeed, 20, (int)Stats.Speed, 10);

		portalOneItems[0] = new Item (true, "Red Portal Stone", itemPictures[4]);
		portalTwoItems[0] = new Item (true, "Blue Portal Stone", itemPictures[5]);
		portalThreeItems[0] = new Item (true, "Green Portal Stone", itemPictures[6]);
	}

	public Item[] LevelItems(int levelNo) {
		switch (levelNo) {
		case 1:
			return levelOneItems;
		case 2:
			return levelTwoItems;
		case 3:
			return levelThreeItems;
		default:
			return levelOneItems;
		}
	}

	public Item[] AssignPortalItems(int portalNo) {
		switch (portalNo) {
		case 0:
			return portalOneItems;
		case 1:
			return portalTwoItems;
		case 2:
			return portalThreeItems;
		default:
			return portalOneItems;
		}
	}
}
