using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
	[SerializeField] protected Image[] icons;
	[SerializeField] protected GameObject description;
	[SerializeField] protected GameObject dropButton;
	[SerializeField] protected Text descriptionText;
	[SerializeField] protected Text headerText;
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
				if (PlayerInfo.Instance.inventory [i].description.Length == 0) {
					PlayerInfo.Instance.inventory [i] = null;
				} else {
					icons [i].sprite = ItemList.Instance.itemPictures [PlayerInfo.Instance.inventory [i].picRef];
				}
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
				if (PlayerInfo.Instance.inventory [button].isObjective == false) {
					switch (PlayerInfo.Instance.inventory [button].itemSet) {
					case 0:
						headerText.text = "Head Mutation";
						break;
					case 1:
						headerText.text = "Body Mutation";
						break;
					case 2:
						headerText.text = "Arm Mutation";
						break;
					case 3:
						headerText.text = "Leg Mutation";
						break;
					case -1:
						headerText.text = "Portal Key";
						break;
					}
					descriptionText.text = PlayerInfo.Instance.inventory [activeButton].description;
					dropButton.SetActive (true);
					//If the item IS an objective item
				} else {
					description.GetComponentInChildren<Text> ().text = "One of the items you've been searching for";
					dropButton.SetActive (true);
				}
				//If the button has already been pressed and it is NOT an objective item
			} else {
				if (PlayerInfo.Instance.inventory [button].isObjective == false) {
					SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.useItem, 0.5f);
                    PlayerInfo.Instance.UseItem (PlayerInfo.Instance.inventory [button]);
					PlayerInfo.Instance.inventory [button] = null;
				}
				RemoveActiveItem ();
			} 
		}
	}

	public void Drop() {
		GameObject droppedItem = GameObject.Instantiate (ItemList.Instance.pickup, player.transform.position + new Vector3(0.0f, 0.0f, 5.0f), Quaternion.identity);
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
