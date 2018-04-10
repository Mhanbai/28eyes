using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePortal : MonoBehaviour {
	void OnTriggerEnter () {
		UnityEngine.SceneManagement.SceneManager.LoadScene (1);
	}
}
