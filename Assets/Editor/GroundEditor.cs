using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GroundEditor : Editor {
	protected static int placements = 0;

	//Script to run when button is pressed
	[MenuItem("28Eyes Tools/Generate Ground")]
	public static void CreateGround () {

		//Reference for prop
		LevelEditor editor = GameObject.Find("Level Editor").GetComponent<LevelEditor>();

		if (editor.groundTile == null) {
			EditorUtility.DisplayDialog ("Warning", "User has not indicated a ground tile to use", "Okay");
		} else {
			//Constant values that indicate the X and Z scale of the tile
			float tileScaleX = editor.groundTile.transform.localScale.x;
			float tileScaleZ = editor.groundTile.transform.localScale.z;

			//Find the diminsions of the ground to be placed
			Vector3 startPos = new Vector3(editor.bottomLeft.x + (tileScaleX / 2), editor.bottomLeft.y, editor.bottomLeft.z + (tileScaleZ / 2));
			float placementWidth = Vector3.Distance (editor.bottomLeft, editor.bottomRight) / tileScaleX;
			float placementDepth = Vector3.Distance (editor.bottomLeft, editor.topLeft) / tileScaleZ;

			//Find GameObject that contains ground tiles
			GameObject container = GameObject.Find("Ground");

			//If no such GameObject exists, create one
			if (container == null) {
				container = new GameObject ("Ground");
			}

			//Create a new GameObject as a child of container that will take the new tiles
			GameObject currentPlacement = GameObject.Instantiate(editor.bottomLeftBoundary, container.transform);
			currentPlacement.name = "Placement" + placements++;

			//Instantiate new tiles as per the Width and Depth defined by the user
			for (int i = 0; i < placementWidth; i++) {
				for (int j = 0; j < placementDepth; j++) {
					Vector3 pos = new Vector3 (startPos.x + (i * tileScaleX), editor.groundTile.transform.position.y, startPos.z + (j * tileScaleZ));
					GameObject.Instantiate (editor.groundTile, pos, Quaternion.identity, currentPlacement.transform);
				}
			}
		}
	}
}
