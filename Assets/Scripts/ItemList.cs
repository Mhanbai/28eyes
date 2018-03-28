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

	//Items required to unlock each portal
	public Item[] portalOneItems = new Item[1];
	public Item[] portalTwoItems = new Item[1];
	public Item[] portalThreeItems = new Item[1];

	//Item lists for each level
	public Item[] levelOneItems = new Item[3];
	public Item[] levelTwoItems = new Item[3];
	public Item[] levelThreeItems = new Item[3];

	void Start() {
		// 	Description, Objective?, Picture, Category, Health Change, Speed Change, Ammo, Reload, Range, Attack Type, Body Part Sprite) {
		levelOneItems[0] = new Item("Raises health by 30% but lowers speed by 10%", false, 1, 0, 0.3f, -0.1f, 0, 0.0f, 0.0f, 0, 1);
		levelOneItems[1] = new Item("Increases speed by 20% but lowers ammo by 10%", false, 1, 2, 0.0f, 0.2f, 0, -0.1f, 0.0f, 0, 0);
		levelOneItems[2] = new Item("Lowers health and speed by 5%, equips a slowing web attack", false, 1, 3, -0.05f, -0.05f, 0, 0.0f, 0.0f, 1, 0);

		levelTwoItems[0] = new Item("Raises health by 30% but lowers speed by 10%", false, 1, 1, 0.3f, -0.1f, 0, 0.0f, 0.0f, 0, 0);
		levelTwoItems[1] = new Item("Increases speed by 20% but lowers ammo by 10%", false, 1, 2, 0.0f, 0.2f, 0, -0.1f, 0.0f, 0, 0);
		levelTwoItems[2] = new Item("Lowers health and speed by 5%, equips a slowing web attack", false, 1, 3, -0.05f, -0.05f, 0, 0.0f, 0.0f, 1, 0);

		levelThreeItems[0] = new Item("Raises health by 30% but lowers speed by 10%", false, 1, 1, 0.3f, -0.1f, 0, 0.0f, 0.0f, 0, 0);
		levelThreeItems[1] = new Item("Increases speed by 20% but lowers ammo by 10%", false, 1, 2, 0.0f, 0.2f, 0, -0.1f, 0.0f, 0, 0);
		levelThreeItems[2] = new Item("Lowers health and speed by 5%, equips a slowing web attack", false, 1, 3, -0.05f, -0.05f, 0, 0.0f, 0.0f, 1, 0);

		portalOneItems[0] = new Item ("Red Portal Stone", true, 4);
		portalTwoItems[0] = new Item ("Blue Portal Stone", true, 5);
		portalThreeItems[0] = new Item ("Green Portal Stone", true, 6);
	}

	public Item[] LevelItems(int listNo) {
		switch (listNo) {
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
