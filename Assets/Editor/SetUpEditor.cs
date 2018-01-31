using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SetUpEditor : Editor {
	[MenuItem("28Eyes Tools/Add Editor Tools to Scene")]
	public static void AddTools() {
		//Create the neccessary gameobjects
		GameObject levelEditor = new GameObject ("Level Editor");
		GameObject topRightBoundary = new GameObject ("BoxTopRight");
		topRightBoundary.transform.SetParent (levelEditor.transform);
		GameObject bottomLeftBoundary = new GameObject ("BoxBottomLeft");
		bottomLeftBoundary.transform.SetParent (levelEditor.transform);

		GameObject spawnPoint = Instantiate(Resources.Load ("PlayerSpawnPoint", typeof(GameObject))) as GameObject;
		spawnPoint.name = "PlayerSpawnPoint";
		spawnPoint.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
		spawnPoint.transform.SetParent (levelEditor.transform);

		GameObject viewReference = new GameObject ("ViewReference");
		viewReference.transform.position = new Vector3 (0.0f, 18.5f, -41.0f);
		viewReference.transform.rotation = Quaternion.Euler (25.0f, 0.0f, 0.0f);
		viewReference.transform.SetParent (levelEditor.transform);

		//Add the level editor script
		LevelEditor LE = levelEditor.AddComponent (typeof(LevelEditor)) as LevelEditor;

		//Link up the bounding boxes
		LE.bottomLeftBoundary = bottomLeftBoundary;
		LE.topRightBoundary = topRightBoundary;
		LE.spawnPointLocation = spawnPoint;
		LE.viewReference = viewReference;
	}
}
