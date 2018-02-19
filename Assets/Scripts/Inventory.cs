using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
	[SerializeField] protected Image[] icons;
	[SerializeField] protected GameObject description;
	[SerializeField] protected GameObject dropButton;
	int activeButton = -1;
	CharController player;

	protected const int INVENTORYSIZE = 6;

	void Start() {
		player = GameObject.Find ("Player").GetComponent<CharController>();
		description.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < INVENTORYSIZE; i++) {
			if (PlayerInfo.Instance.inventory [i] != null) {
				icons [i].sprite = PlayerInfo.Instance.inventory [i].Picture ();
			} else {
				icons [i].sprite = ItemList.Instance.itemPictures [0];
			}
		}

		if (PlayerInfo.Instance.IsRunningOrShooting()) {
			RemoveActiveItem ();
		}
	}

	public void ButtonPress(int button) {
		//Only do the following if the player holds an item in this slot
		if (PlayerInfo.Instance.inventory [button] != null) {
			//If the button has not already been pressed
			if (activeButton != button) {
				activeButton = button;
				description.SetActive (true);
				//If the item is NOT an objective item
				if (PlayerInfo.Instance.inventory [button].IsObjective () == false) {
					//TO DO: WRITE WHAT ITEM DOES

					description.GetComponentInChildren<Text> ().text = PlayerInfo.Instance.inventory [activeButton].Name();
					dropButton.SetActive (true);
					//If the item IS an objective item
				} else {
					description.GetComponentInChildren<Text> ().text = "Needed to complete an objective";
					dropButton.SetActive (true);
				}
				//If the button has already been pressed and it is NOT an objective item
			} else {
				if (PlayerInfo.Instance.inventory [button].IsObjective () == false) {
					//TODO: TRIGGER USE OF ITEM HERE
					PlayerInfo.Instance.inventory [button] = null;
				}
				RemoveActiveItem ();
			} 
		}
	}

	public void Drop() {
		GameObject droppedItem = GameObject.Instantiate (ItemList.Instance.pickup, player.transform.position, Quaternion.identity);
		droppedItem.GetComponent<Pickup>().AssignItem (PlayerInfo.Instance.inventory [activeButton]);
		PlayerInfo.Instance.inventory [activeButton] = null;
		description.SetActive (false);
		activeButton = -1;
	}

	public void RemoveActiveItem() {
		description.SetActive (false);
		dropButton.SetActive (false);
		activeButton = -1;
	}
}
