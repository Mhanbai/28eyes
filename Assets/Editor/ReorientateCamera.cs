using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReorientateCamera : Editor {
	[MenuItem("28Eyes Tools/1. Reorientate Camera")]
	public static void MoveCamera() {
		Transform bestView = GameObject.Find ("Main Camera").GetComponent<Camera> ().transform;
		SceneView.lastActiveSceneView.AlignViewToObject (bestView);
	}
}
