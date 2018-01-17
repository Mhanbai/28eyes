using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditor : MonoBehaviour {
	//Reference for base ground tile
	public GameObject groundTile;

	//Adjustable width and depth for level
	[Range(2, 500)] public int levelWidth;
	[Range(2, 500)] public int levelDepth;
}
