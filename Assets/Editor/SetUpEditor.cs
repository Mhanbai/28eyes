﻿using System.Collections;
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

		GameObject spawnPoint = Instantiate(Resources.Load ("Tools/PlayerSpawnPoint", typeof(GameObject))) as GameObject;
		spawnPoint.name = "PlayerSpawnPoint";
		spawnPoint.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
		spawnPoint.transform.SetParent (levelEditor.transform);

		GameObject viewReference = new GameObject ("ViewReference");
		viewReference.transform.position = new Vector3 (0.0f, 18.5f, -41.0f);
		viewReference.transform.rotation = Quaternion.Euler (45.0f, 0.0f, 0.0f);
		viewReference.transform.SetParent (levelEditor.transform);

		GameObject lighting = Instantiate(Resources.Load ("Tools/Light", typeof(GameObject))) as GameObject;
		lighting.name = "Lighting";
		lighting.transform.position = new Vector3 (0.0f, 100.0f, -70.0f);

		//Add the level editor script
		LevelEditor LE = levelEditor.AddComponent (typeof(LevelEditor)) as LevelEditor;

		//Link up the bounding boxes
		LE.bottomLeftBoundary = bottomLeftBoundary;
		LE.topRightBoundary = topRightBoundary;
		LE.spawnPointLocation = spawnPoint;
		LE.viewReference = viewReference;

		Snap s1 = LE.bottomLeftBoundary.AddComponent (typeof(Snap)) as Snap;
		Snap s2 = LE.topRightBoundary.AddComponent (typeof(Snap)) as Snap;
	}
}
