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
		hubUI.gameObject.SetActive (false);
	}

	void Update() {
		if (PlayerInfo.Instance.IsRunningOrShooting ()) {
			hubUI.gameObject.SetActive (false);
		}
	}

	// Update is called once per frame
	void OnTriggerEnter () {
		SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.portalUse, 0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene (3);
	}
}
