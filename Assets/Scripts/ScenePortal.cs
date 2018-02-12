using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePortal : MonoBehaviour {
	void OnTriggerStay () {
		if (Input.GetKeyDown (KeyCode.E)) {
			UnityEngine.SceneManagement.SceneManager.LoadScene (1);
		}
	}
}
