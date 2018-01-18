﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LevelEditor : MonoBehaviour {
	[Header("Ground Values")]
	//Reference for base ground tile
	[SerializeField] public GameObject groundTile;
	//Adjustable width and depth for level
	[SerializeField] [Range(2, 500)] public int levelWidth = 2;
	[SerializeField] [Range(2, 500)] public int levelDepth = 2;

	[Header("Clutter Values")]
	[SerializeField] public GameObject prop;
	[SerializeField] [Range(1, 100)] public int density = 1;
	[SerializeField] public GameObject bottomLeftBoundary;
	[SerializeField] public GameObject topRightBoundary;
	[SerializeField] public bool ShowBoundaries = true;
	[NonSerialized] public bool CorrectOrder = true;
	[NonSerialized] public Vector3 bottomLeft;
	[NonSerialized] public Vector3 bottomRight;
	[NonSerialized] public Vector3 topLeft;
	[NonSerialized] public Vector3 topRight;


	void Update() {
		
		//********Update for Clutter editor********

		//Reset Y values for both boundaries to 0
		bottomLeftBoundary.transform.position = new Vector3 (bottomLeftBoundary.transform.position.x, 0.0f, bottomLeftBoundary.transform.position.z);
		topRightBoundary.transform.position = new Vector3 (topRightBoundary.transform.position.x, 0.0f, topRightBoundary.transform.position.z);

		//Find the four corners of the bounding box
		bottomLeft = bottomLeftBoundary.transform.position;
		bottomRight = new Vector3 (topRightBoundary.transform.position.x, 0, bottomLeftBoundary.transform.position.z);
		topLeft = new Vector3 (bottomLeftBoundary.transform.position.x, 0, topRightBoundary.transform.position.z);
		topRight = topRightBoundary.transform.position;

		if ((bottomLeft.z > topRight.z) || (bottomLeft.x > topRight.x)) {
			CorrectOrder = false;
		} else {
			CorrectOrder = true;
		}
	}

	void OnDrawGizmos() {
		
		//********Gizmos for Clutter editor********

		if (CorrectOrder == true) {
			Gizmos.color = Color.green;
		} else {
			Gizmos.color = Color.red;
		}

		if (ShowBoundaries == true) {
			Gizmos.DrawLine (bottomLeft, bottomRight);
			Gizmos.DrawLine (bottomRight, topRight);
			Gizmos.DrawLine (topRight, topLeft);
			Gizmos.DrawLine (topLeft, bottomLeft);
			Gizmos.DrawSphere (bottomLeft, 0.3f);
			Gizmos.DrawSphere (bottomRight, 0.3f);
			Gizmos.DrawSphere (topRight, 0.3f);
			Gizmos.DrawSphere (topLeft, 0.3f);
		}
	}
}
