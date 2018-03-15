using System.Collections;
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
			Debug.Log ("Warning, there is more than one PlayerInfo class in the scene!");
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

			PlayerInfo.Instance.Load (data.health, data.inventory, data.headPart, data.bodyPart, data.armPart, data.legPart);
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

		bf.Serialize (file, data);
		file.Close ();
	}

	public bool IsPortalUnlocked(int portalNo) {
		switch (portalNo) {
		case 0:
			return portal1Unlocked;
		case 1:
			return portal2Unlocked;
		case 2:
			return portal3Unlocked;
		default:
			return false;
		}

		return false;
	}

	public bool SetPortalUnlocked(int portalNo, bool state) {
		switch (portalNo) {
		case 0:
			portal1Unlocked = state;
			break;
		case 1:
			portal2Unlocked = state;
			break;
		case 2:
			portal3Unlocked = state;
			break;
		default:
			return false;
		}

		return false;
	}
}

[System.Serializable]
class DataFile {
	public float health;
	public Item[] inventory;
	public Item headPart;
	public Item bodyPart;
	public Item armPart;
	public Item legPart;
	public bool portalOneUnlock;
	public bool portalTwoUnlock;
	public bool portalThreeUnlock;
}

