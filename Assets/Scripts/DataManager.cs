﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManager : MonoBehaviour {
	//The following code ensures that there is a single global DataManager class available for other objects to use 
	public static DataManager Instance { get; private set; }

	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else {
			Debug.Log ("Warning, there is more than one DataManager class in the scene!");
		}
	}
		
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////

	bool portal1Unlocked = false;
	bool portal2Unlocked = false;
	bool portal3Unlocked = false;
	Item headEquip;
	Item bodyEquip;
	Item armEquip;
	Item legEquip;

	public void Load() {
		if (File.Exists (Application.persistentDataPath + "/PleaseEat.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/PleaseEat.dat", FileMode.Open);

			DataFile data = (DataFile)bf.Deserialize (file);
			file.Close ();

			PlayerInfo.Instance.Load (data.health, data.inventory, data.headPart, data.bodyPart, data.armPart, 
										data.legPart, data.headSprite, data.bodySprite, data.armSprite, data.legSprite);
			portal1Unlocked = data.portalOneUnlock;
			portal2Unlocked = data.portalTwoUnlock;
			portal3Unlocked = data.portalThreeUnlock;
		}
	}

	public void Save() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/PleaseEat.dat", FileMode.OpenOrCreate);

		DataFile data = new DataFile ();
		data.health = PlayerInfo.Instance.CurrentHealth ();
		data.inventory = PlayerInfo.Instance.inventory;
		data.headPart = PlayerInfo.Instance.headItem;
		data.bodyPart = PlayerInfo.Instance.bodyItem;
		data.armPart = PlayerInfo.Instance.armItem;
		data.legPart = PlayerInfo.Instance.legItem;
		data.portalOneUnlock = portal1Unlocked;
		data.portalTwoUnlock = portal2Unlocked;
		data.portalThreeUnlock = portal3Unlocked;
		data.headSprite = PlayerInfo.Instance.headPart;
		data.bodySprite = PlayerInfo.Instance.bodyPart;
		data.armSprite  = PlayerInfo.Instance.armPart;
		data.legSprite  = PlayerInfo.Instance.legPart;

		bf.Serialize (file, data);
		file.Close ();
	}

    public void Reset()
    {
        Item[] emptyInv = { null, null, null, null, null, null };

        PlayerInfo.Instance.Load(100, emptyInv, null, null, null, null, 0, 0, 0, 0);
        portal1Unlocked = false;
        portal2Unlocked = false;
        portal3Unlocked = false;

        Save();
    }
}

[System.Serializable]
class DataFile {
	public int health;
	public Item[] inventory;
	public Item headPart;
	public Item bodyPart;
	public Item armPart;
	public Item legPart;
	public int headSprite;
	public int bodySprite;
	public int armSprite;
	public int legSprite;
	public bool portalOneUnlock;
	public bool portalTwoUnlock;
	public bool portalThreeUnlock;
}

