using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
	[SerializeField] bool isLocked = true;
	[SerializeField] Canvas hubUI;
	[Range(0, 2)][SerializeField] int portalNumber;
	Item[] requiredToOpen;
	CharController player;

	void Start() {
		player = GameObject.Find ("Player").GetComponent<CharController> ();
		requiredToOpen = ItemList.Instance.AssignPortalItems (portalNumber);
		hubUI.gameObject.SetActive (false);
	}

	void Update() {
		if (PlayerInfo.Instance.IsRunningOrShooting ()) {
			hubUI.gameObject.SetActive (false);
		}
	}

	// Update is called once per frame
	void OnTriggerStay () {
		if (Input.GetKeyDown (KeyCode.E)) {
			if (isLocked == true) {
				FlipShowItems ();
				hubUI.GetComponent<hubUI> ().UpdatePortal (requiredToOpen, portalNumber);
			} else {
				UnityEngine.SceneManagement.SceneManager.LoadScene (portalNumber + 3);
			}
		}
	}

	public void FlipShowItems() {
		if (hubUI.gameObject.activeSelf == true) {
			hubUI.gameObject.SetActive (false);
		} else {
			hubUI.gameObject.SetActive (true);
		}
	}

	public void Unlock() {
		isLocked = false;
	}
}
