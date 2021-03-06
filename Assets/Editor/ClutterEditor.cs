using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ClutterEditor : Editor {
	//Script to run when button is pressed
	[MenuItem("28Eyes Tools/Create Props")]
	public static void CreateProps () {
		//Make sure editor tools exist and find a reference
		GameObject editorRef = GameObject.Find("Level Editor");

		//If they dont, exit out and warn the user
		if (editorRef == null) {
			EditorUtility.DisplayDialog ("Warning", "No editor tools exist in scene! Try using 'Add Tools to Scene'.", "Okay");
			return;
		}

		//Find the level editor object
		LevelEditor editor = editorRef.GetComponent<LevelEditor>();

		//Reference for base tile
		GameObject prop = editor.prop;
		int density = editor.density;

		//Find the available ground
		GameObject ground = GameObject.Find("Ground");

		//If user has not set something up correctly, exit out
		if (ground == null) {
			EditorUtility.DisplayDialog ("Warning", "No ground in scene - try using 'Generate Ground' tool", "Okay");
			return;
		} else if (prop == null) {
			EditorUtility.DisplayDialog ("Warning", "User has not indicated a prop to use", "Okay");
			return;
		} else if (density == 1) { 
			if (EditorUtility.DisplayDialog ("Warning", "Density is only set to 1%", "Continue", "Cancel")) {
			} else {
				return;
			}
		} 
			
		//Create empty gameobject to store props
		GameObject propStorer = new GameObject ("Props");

		//Figur out what Y position to spawn object
		float yPos = prop.GetComponentInChildren<SpriteRenderer> ().bounds.extents.y;

		//Figur out how many objects need to spawn using density and boundary sizes
		//Find size of sprite
		float xSize = prop.GetComponentInChildren<SpriteRenderer> ().bounds.extents.x;
		float zSize = prop.GetComponentInChildren<SpriteRenderer> ().bounds.extents.y;
		//Find size of bounding box
		float boundBoxXSize = Vector3.Distance (editor.bottomLeft, editor.bottomRight);
		float boundBoxZSize = Vector3.Distance (editor.bottomLeft, editor.topLeft);
		//Divide size of bounding box by size of sprite
		float maxXProps = boundBoxXSize / xSize;
		float maxZProps = boundBoxZSize / zSize;
		//Multiply these numbers to find the max amount of objects that can fit
		float maxObj = maxXProps * maxZProps;
		float objToSpawn = (maxObj / 100.0f) * density;

		List<Vector3> usedPositions = new List<Vector3>();
		usedPositions.Add(new Vector3(0.0f, 0.0f, 0.0f));

		//For ech object
		for (int i = 0; i < objToSpawn; i++) {
			bool place = true;
			Vector3 spawnPos = new Vector3 (Random.Range(editor.bottomLeft.x, editor.bottomRight.x), yPos, Random.Range(editor.bottomLeft.z, editor.topLeft.z));
			foreach (Vector3 pos in usedPositions) {
				if (Vector3.Distance (spawnPos, pos) < 5.0f) {
					place = false;
				}
			}

			if (place == true) {
				GameObject.Instantiate (prop, spawnPos, Quaternion.identity, propStorer.transform);
				usedPositions.Add (spawnPos);
			} else {
				i--;
			}
		}
	}
}
