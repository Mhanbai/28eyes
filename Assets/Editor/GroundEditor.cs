using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GroundEditor : Editor {
	//Script to run when button is pressed
	[MenuItem("28Eyes Tools/Generate Ground")]
	public static void CreateGround () {

		//Reference for prop
		GameObject groundTile = GameObject.Find("Level Editor").GetComponent<LevelEditor>().groundTile;

		if (groundTile == null) {
			EditorUtility.DisplayDialog ("Warning", "User has not indicated a ground tile to use", "Okay");
		} else {

			//Adjustable width and depth for level
			int levelWidth = GameObject.Find ("Level Editor").GetComponent<LevelEditor> ().levelWidth;
			int levelDepth = GameObject.Find ("Level Editor").GetComponent<LevelEditor> ().levelDepth;

			//Constant values that indicate the X and Z scale of the tile
			float tileScaleX = groundTile.transform.localScale.x;
			float tileScaleZ = groundTile.transform.localScale.z;

			//Find the location of the bottom left of the level
			float startXOffset = (levelWidth / 2) * tileScaleX;
			float startZOffset = (levelDepth / 2) * tileScaleZ;
			Vector3 startPos = new Vector3 (0 - startXOffset, 0, 0 - startZOffset);

			//Create a new empty GameObject to store the ground tiles
			GameObject ground = new GameObject ("Ground");

			//Instantiate new tiles as per the Width and Depth defined by the user
			for (int i = 0; i < levelWidth; i++) {
				for (int j = 0; j < levelDepth; j++) {
					Vector3 pos = new Vector3 (startPos.x + (i * tileScaleX), groundTile.transform.position.y, startPos.z + (j * tileScaleZ));
					GameObject.Instantiate (groundTile, pos, Quaternion.identity, ground.transform);
				}
			}
		}
	}
}
