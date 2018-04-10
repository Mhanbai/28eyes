using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
	public SpriteRenderer sprite;
	public GameObject icon;
	[System.NonSerialized] public Item myItem;

	float yPos;
	float time = 0.0f;

	void Start() {
		//AssignItem (ItemList.Instance.firstLevelItems [0]);
		yPos = gameObject.GetComponent<Collider> ().bounds.size.y;
	}

	// Update is called once per frame
	void Update () {
		//Movement code
		time += Time.deltaTime;
		float iconYPos = Mathf.Cos (time);
		icon.transform.position = new Vector3 (icon.transform.position.x, yPos + iconYPos, icon.transform.position.z);

		transform.position = new Vector3 (transform.position.x, yPos, transform.position.z);
	}

	void OnTriggerStay() {
		if (PlayerInfo.Instance.AddToInventory (myItem) == true) {
			GameObject.Destroy (gameObject);
		}
	}

	public void AssignItem(Item item_in) {
		sprite.sprite = ItemList.Instance.itemPictures [item_in.picRef];
		myItem = item_in;
	}
}
