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

    //Item lists for each level
    [System.NonSerialized] public Item[] levelOneItems = new Item[9];
    [System.NonSerialized] public Item[] levelTwoItems = new Item[3];

	void Start() {
		// 	                        Description,                                                                                            Objective?, Picture, Category, Health Change, Speed Change, Ammo, Reload, Range, Attack Type, Body Part Sprite) {
		levelOneItems[0] = new Item("Equips the body of a crab, raising health by 30 but reducing speed by 20",                             false,      1,          1,      30,              -0.2f,      0,      0.0f,   0.0f,   -1,         0);
		levelOneItems[1] = new Item("Equips the body of an eagle, increasing speed by 20% but lowering health by 20",                       false,      1,          1,      -20,             +0.2f,      0,      0.0f,   0.0f,   -1,         1);
		levelOneItems[2] = new Item("Equips the body of a spider, increasing speed by 10% and health by 10",                                false,      1,          1,      -10,             +0.1f,     0,      0.0f,   0.0f,   -1,         0);
        levelOneItems[3] = new Item("Equips the arms of a crab, altering your attack to a crab claw RPG!",                                  false,      1,          2,      0,                  0f,     0,      0.0f,   0.0f,    2,         0);
        levelOneItems[4] = new Item("Equips the wings of an eagle, altering your attack to shoot razor sharp feathers!",                    false,      1,          2,      0,                  0f,     0,      0.0f,   0.0f,    4,         1);
        levelOneItems[5] = new Item("Equips the arms of a spider, altering your attack to throw a slowing web shot in an arcing motion!",   false,      1,          2,      0,                  0f,     0,      0.0f,   0.0f,    1,         0);
        levelOneItems[6] = new Item("Equips the legs of a crab, raising your speed by 10% and your health by 20",                           false,      1,          3,      +20,             +0.1f,     0,      0.0f,   0.0f,   -1,         0);
        levelOneItems[7] = new Item("Equips the legs of an eagle, raising your speed by 20% and your health by 10",                         false,      1,          3,      +10,             +0.2f,     0,      0.0f,   0.0f,   -1,         1);
        levelOneItems[8] = new Item("Equips the legs of a spider, Raising your speed by 30% but lowering your health by 10",                false,      1,          3,      -10,             +0.3f,     0,      0.0f,   0.0f,   -1,         0);

        levelTwoItems[0] = new Item("Raises health by 30 but lowers speed by 10%", false, 1, 0, 30, 0, 0, 0.0f, 0.0f, -1, 0);
		levelTwoItems[1] = new Item("Increases speed by 20% but lowers ammo by 10%", false, 1, 3, 0, 0, 0, 0, 0.0f, -1, 0);
		levelTwoItems[2] = new Item("Lowers health by 5 and speed by 5%, equips a slowing web attack", false, 1, 2, -5, 0, 0, 0.0f, 0.0f, 1, 0);
	}

	public Item[] LevelItems(int listNo) {
		switch (listNo) {
		case 1:
			return levelOneItems;
		case 2:
			return levelTwoItems;
		default:
			return levelOneItems;
		}
	}
}
