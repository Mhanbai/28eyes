using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homePortal : MonoBehaviour {
	void OnTriggerStay () {
		UnityEngine.SceneManagement.SceneManager.LoadScene (2);
	}
}
