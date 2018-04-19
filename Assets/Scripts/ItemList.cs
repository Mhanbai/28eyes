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
    [System.NonSerialized] public Item[] levelOneItems = new Item[5];
    [System.NonSerialized] public Item[] levelTwoItems = new Item[4];
    [System.NonSerialized] public Item[] levelThreeItems = new Item[7];
    [System.NonSerialized] public Item[] levelFourItems = new Item[10];
    [System.NonSerialized] public Item[] levelFiveItems = new Item[1];

    void Start() {
		// 	                        Description,                                                                                                        Objective?, Picture, Category, Health Change, Speed Change, Ammo, Reload, Range, Attack Type, Body Part Sprite) {
		levelOneItems[0] = new Item("Equips the arms of a spider, altering your attack to throw a poisonous, slowing web shot in an arcing motion!",    false,      1,          2,      0,                  0f,      0,      0.0f,   0.0f,    1,         0);
        levelOneItems[1] = new Item("Equips the legs of a crab, reducing your speed by 10% and your health by 20",                                      false,      1,          3,      +20,            -0.01f,      0,      0.0f,   0.0f,   -1,         0);
        levelOneItems[2] = new Item("Equips the legs of an eagle, raising your speed by 10% and your health by 10",                                     false,      1,          3,      +10,             +0.1f,      0,      0.0f,   0.0f,   -1,         1);
        levelOneItems[3] = new Item("Equips the legs of a spider, Raising your speed by 30% but lowering your health by 10",                            false,      1,          3,      -10,             +0.3f,      0,      0.0f,   0.0f,   -1,         0);
        levelOneItems[4] = new Item("Equips a pair of deadly cat claws, allowing you to slash foes at close range to make them bleed!",                 false,      1,          2,        0,                 0,      0,      0.0f,   0.0f,    5,         0);

        levelTwoItems[0] = new Item("Equips the body of a crab, raising health by 30 but reducing speed by 20%", false, 1, 1, +30, -0.2f, 0, 0.0f, 0.0f, -1, 0);
        levelTwoItems[1] = new Item("Equips the body of an eagle, increasing speed by 10% but lowering health by 10", false, 1, 1, -10, +0.1f, 0, 0.0f, 0.0f, -1, 1);
        levelTwoItems[2] = new Item("Equips the body of a spider, increasing speed by 20% and reducing health by 20", false, 1, 1, -20, +0.2f, 0, 0.0f, 0.0f, -1, 0);
        levelTwoItems[3] = new Item("Equips the wings of an eagle, altering your throw a spread of deadly eggs!", false, 1, 2, 0, 0f, 0, 0.0f, 0.0f, 3, 1);

        levelThreeItems[0] = new Item("Equips the body of a crab, raising health by 30 but reducing speed by 20%", false, 1, 1, +30, -0.2f, 0, 0.0f, 0.0f, -1, 0);
        levelThreeItems[1] = new Item("Equips the body of an eagle, increasing speed by 10% but lowering health by 10", false, 1, 1, -10, +0.1f, 0, 0.0f, 0.0f, -1, 1);
        levelThreeItems[2] = new Item("Equips the body of a spider, increasing speed by 20% and reducing health by 20", false, 1, 1, -20, +0.2f, 0, 0.0f, 0.0f, -1, 0);
        levelThreeItems[3] = new Item("Equips the legs of a crab, reducing your speed by 10% and your health by 20", false, 1, 3, +20, -0.01f, 0, 0.0f, 0.0f, -1, 0);
        levelThreeItems[4] = new Item("Equips the legs of an eagle, raising your speed by 10% and your health by 10", false, 1, 3, +10, +0.1f, 0, 0.0f, 0.0f, -1, 1);
        levelThreeItems[5] = new Item("Equips the legs of a spider, Raising your speed by 30% but lowering your health by 10", false, 1, 3, -10, +0.3f, 0, 0.0f, 0.0f, -1, 0);
        levelThreeItems[6] = new Item("Equips the arms of a crab, altering your attack to a crab claw RPG!", false, 1, 2, 0, 0f, 0, 0.0f, 0.0f, 2, 0);

        levelFourItems[0] = new Item("Equips the body of a crab, raising health by 30 but reducing speed by 20%", false, 1, 1, +30, -0.2f, 0, 0.0f, 0.0f, -1, 0);
        levelFourItems[1] = new Item("Equips the body of an eagle, increasing speed by 10% but lowering health by 10", false, 1, 1, -10, +0.1f, 0, 0.0f, 0.0f, -1, 1);
        levelFourItems[2] = new Item("Equips the body of a spider, increasing speed by 20% and reducing health by 20", false, 1, 1, -20, +0.2f, 0, 0.0f, 0.0f, -1, 0);
        levelFourItems[3] = new Item("Equips the arms of a crab, altering your attack to a crab claw RPG!", false, 1, 2, 0, 0f, 0, 0.0f, 0.0f, 2, 0);
        levelFourItems[4] = new Item("Equips the wings of an eagle, altering your throw a spread of deadly eggs!", false, 1, 2, 0, 0f, 0, 0.0f, 0.0f, 3, 1);
        levelFourItems[5] = new Item("Equips the arms of a spider, altering your attack to throw a poisonous, slowing web shot in an arcing motion!", false, 1, 2, 0, 0f, 0, 0.0f, 0.0f, 1, 0);
        levelFourItems[6] = new Item("Equips the legs of a crab, reducing your speed by 10% and your health by 20", false, 1, 3, +20, -0.01f, 0, 0.0f, 0.0f, -1, 0);
        levelFourItems[7] = new Item("Equips the legs of an eagle, raising your speed by 10% and your health by 10", false, 1, 3, +10, +0.1f, 0, 0.0f, 0.0f, -1, 1);
        levelFourItems[8] = new Item("Equips the legs of a spider, Raising your speed by 30% but lowering your health by 10", false, 1, 3, -10, +0.3f, 0, 0.0f, 0.0f, -1, 0);
        levelFourItems[9] = new Item("Equips a pair of deadly cat claws, allowing you to slash foes at close range to make them bleed!", false, 1, 2, 0, 0, 0, 0.0f, 0.0f, 5, 0);

        levelFiveItems[0] = new Item("Equips the Ultimate Weapon, use at your own risk! Also provides a significant boost to health!", false, 1, 2, 50, 0, 0, 0.0f, 0.0f, 4, 0);

    }

	public Item[] LevelItems(int listNo) {
		switch (listNo) {
		case 1:
			return levelOneItems;
		case 2:
			return levelTwoItems;
        case 3:
            return levelThreeItems;
        case 4:
            return levelFourItems;
        case 5:
            return levelFiveItems;
        default:
			return levelOneItems;
		}
	}
}
