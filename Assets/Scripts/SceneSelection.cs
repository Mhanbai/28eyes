using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSelection : MonoBehaviour {
	string[] scenes;
	public GameObject button;
	public GameObject panel;
	public GameObject buttonPos;
	public Canvas canvas;

	void Awake () {
		int sceneCount = SceneManager.sceneCountInBuildSettings;
		scenes = new string[sceneCount];

		for (int i = 0; i < sceneCount; i++) {
			scenes [i] = System.IO.Path.GetFileNameWithoutExtension (SceneUtility.GetScenePathByBuildIndex (i));
		}
	}

	void Start() {
		int i = 0;
		foreach (string scene in scenes) {
			GameObject sceneButton = Instantiate (button, panel.transform);
			sceneButton.GetComponentInChildren<Text> ().text = scene;
			sceneButton.GetComponent<menuButton> ().sceneToOpen = i;
			sceneButton.GetComponent<Button>().onClick.AddListener(() => OnButtonPress(sceneButton.GetComponent<menuButton> ().sceneToOpen));
			sceneButton.transform.position = new Vector3 (buttonPos.transform.position.x, buttonPos.transform.position.y - (i * 75.0f), buttonPos.transform.position.z);

			i++;
		}
	}

	void OnButtonPress(int sceneToLoad) {
		SceneManager.LoadScene (sceneToLoad);
	}
}
