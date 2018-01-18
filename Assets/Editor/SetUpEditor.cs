using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SetUpEditor : Editor {
	[MenuItem("28Eyes Tools/Add Editor Tools to Scene")]
	public static void AddTools() {
		//Create the neccessary gameobjects
		GameObject levelEditor = new GameObject ("Level Editor");
		GameObject topRightBoundary = new GameObject ("ClutterTopRightBoundary");
		topRightBoundary.transform.SetParent (levelEditor.transform);
		GameObject bottomLeftBoundary = new GameObject ("ClutterBottomLeftBoundary");
		bottomLeftBoundary.transform.SetParent (levelEditor.transform);

		//Add the level editor script
		LevelEditor LE = levelEditor.AddComponent (typeof(LevelEditor)) as LevelEditor;

		//Link up the bounding boxes
		LE.bottomLeftBoundary = bottomLeftBoundary;
		LE.topRightBoundary = topRightBoundary;
	}
}
