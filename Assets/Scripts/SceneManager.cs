using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {
	public GameObject UI;
	public GameObject Player;

	protected GameObject uiRef;

	void OnEnable()
	{
		UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
	}
		
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if ((scene.name != "_preload") && (scene.name != "_mainmenu")) {
			uiRef = GameObject.Instantiate (UI);
			GameObject spawnPoint = GameObject.Find ("PlayerSpawnPoint");
			GameObject avatar = GameObject.Instantiate (Player, spawnPoint.transform.position, Quaternion.identity);
			avatar.name = "Player";
			spawnPoint.SetActive (false);
		}

		if (scene.name == "_hubWorld") {
			uiRef.SetActive (false);
		}
	}
}
