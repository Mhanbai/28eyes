using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homePortal : MonoBehaviour {
	void OnTriggerStay () {
		if (Input.GetKeyDown (KeyCode.E)) {
			UnityEngine.SceneManagement.SceneManager.LoadScene (2);
		}
	}
}
