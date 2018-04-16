using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
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
	void OnTriggerEnter () {
		if (DataManager.Instance.IsPortalUnlocked(portalNumber) == true) {
			FlipShowItems ();
			hubUI.GetComponent<hubUI> ().UpdatePortal (requiredToOpen, portalNumber);
		} else {
            SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.portalUse);
			UnityEngine.SceneManagement.SceneManager.LoadScene (portalNumber + 3);
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
		DataManager.Instance.SetPortalUnlocked (portalNumber, true);
	}
}
