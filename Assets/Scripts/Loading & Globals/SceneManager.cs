using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {
	public GameObject UI;
	public GameObject Player;

	void OnEnable()
	{
		UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
	}
		
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if ((scene.name != "_preload") && (scene.name != "_mainmenu")) {
			GameObject.Instantiate (UI);
			GameObject spawnPoint = GameObject.Find ("PlayerSpawnPoint");
			GameObject.Instantiate (Player, spawnPoint.transform.position, Quaternion.identity);
			spawnPoint.SetActive (false);
		}
	}
}
