using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    public int sceneNumber;

	// Update is called once per frame
	void OnTriggerEnter (Collider collider) {
        if (collider.CompareTag("Player")) {
            SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.portalUse, 0.5f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNumber);
        }
	}
}
