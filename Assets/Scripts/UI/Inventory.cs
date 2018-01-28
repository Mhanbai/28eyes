using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
	[SerializeField] protected Image[] backgrounds;
	[SerializeField] protected Image[] icons;

	protected const int INVENTORYSIZE = 6;

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < INVENTORYSIZE; i++) {
			if (PlayerInfo.Instance.inventory [i] != null) {
				icons [i].sprite.Equals(PlayerInfo.Instance.inventory [i].Picture());
			}
		}
	}
}
