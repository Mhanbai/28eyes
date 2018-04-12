using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {
	//The following code ensures that there is a single global PlayerInfo class available for other objects to use 
	public static SceneManager Instance { get; private set; }

	void Awake () {
		if (Instance == null) {
			Instance = this;
		}
	}

	public GameObject UI;
	public GameObject Player;
	public GameObject DebugUI;

	public GameObject uiRef;

	void OnEnable()
	{
		UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
	}
		
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if ((scene.name != "_preload") && (scene.name != "_mainmenu") && (scene.name != "_death")) {
			uiRef = GameObject.Instantiate (UI);
			GameObject.Instantiate (DebugUI);
			GameObject spawnPoint = GameObject.Find ("PlayerSpawnPoint");
			GameObject avatar = GameObject.Instantiate (Player, spawnPoint.transform.position, Quaternion.identity);
			avatar.name = "Player";
			spawnPoint.SetActive (false);
		}

		if (scene.name == "_hubWorld") {
			uiRef.SetActive (false);
		}
	}

	public void GameOver() {
		UnityEngine.SceneManagement.SceneManager.LoadScene (6);
	}

	public void Quit() {
		Application.Quit ();
	}

	public void MainMenu() {
		UnityEngine.SceneManagement.SceneManager.LoadScene (1);
	}
}
