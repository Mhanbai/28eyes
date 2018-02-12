using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hubUI : MonoBehaviour {
	[SerializeField] Image[] itemImages;
	[SerializeField] Image[] inventoryImages;
	[SerializeField] Portal[] scenePortals;
	Item[] requiredItems;
	int portalNo;

	public void UpdatePortal(Item[] requiredItems_in, int portalNo_in) {
		portalNo = portalNo_in;
		requiredItems = requiredItems_in;

		int i = 0;
		foreach (Item item in requiredItems) {
			itemImages [i].sprite = item.Picture ();
			i++;
		}

		for (; i < itemImages.Length; i++) {
			itemImages [i].gameObject.SetActive (false);
		}

		for (int j = 0; j < 6; j++) {
			if (PlayerInfo.Instance.inventory [j] != null) {
				inventoryImages [j].sprite = PlayerInfo.Instance.inventory [j].Picture ();
			} else {
				inventoryImages [j].sprite = ItemList.Instance.itemPictures [0];
			}
		}
	}

	public void ClickUnlock() {
		int target = 0;
		foreach (Item item in requiredItems) {
			foreach (Item heldItem in PlayerInfo.Instance.inventory) {
				if (heldItem == item) {
					target++;
					break;
				}
			}
		}

		if (target == requiredItems.Length) {
			scenePortals [portalNo].Unlock ();
			scenePortals [portalNo].FlipShowItems ();
		}
	}
}
