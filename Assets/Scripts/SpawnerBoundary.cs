using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpawnerBoundary : MonoBehaviour {
	[SerializeField] public GameObject bottomLeftBoundary;
	[SerializeField] public GameObject topRightBoundary;
	[SerializeField] public bool ShowBoundaries = true;

	Vector3 topLeft;
	Vector3 topRight;
	Vector3 bottomLeft;
	Vector3 bottomRight;

	void Update() {
		bottomLeft = bottomLeftBoundary.transform.position;
		bottomRight = new Vector3 (topRightBoundary.transform.position.x, 0, bottomLeftBoundary.transform.position.z);
		topLeft = new Vector3 (bottomLeftBoundary.transform.position.x, 0, topRightBoundary.transform.position.z);
		topRight = topRightBoundary.transform.position;
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;

		if (ShowBoundaries == true) {
			//Gizmos.DrawLine (bottomLeft, bottomRight);
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
