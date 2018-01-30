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
		description.SetActive (false);
		player = GameObject.Find ("Player").GetComponent<CharController> ();
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

		if ((activeButton != -1) && (player.IsRunningOrShooting())) {
			RemoveActiveItem ();
		}
	}

	public void ButtonPress(int button) {
		if (PlayerInfo.Instance.inventory [button] != null) {
			if (activeButton != button) {
				activeButton = button;
				description.SetActive (true);
				if (PlayerInfo.Instance.inventory [button].IsObjective () == false) {
					//TO DO: WRITE WHAT ITEM DOES

					description.GetComponentInChildren<Text> ().text = "Does a thing";
					dropButton.SetActive (true);
				} else {
					description.GetComponentInChildren<Text> ().text = "Needed to complete an objective";
					dropButton.SetActive (false);
				}
			} else if (PlayerInfo.Instance.inventory [button].IsObjective() == false) {
				//TODO: TRIGGER USE OF ITEM HERE

				PlayerInfo.Instance.inventory [button] = null;
				activeButton = -1;
				description.SetActive (false);
			}
		}
	}

	public void Drop() {
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
