using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReorientateCamera : Editor {
	[MenuItem("28Eyes Tools/Reorientate Camera")]
	public static void MoveCamera() {
		Transform bestView = GameObject.Find ("Level Editor").GetComponent<LevelEditor> ().viewReference.transform;
		SceneView.lastActiveSceneView.AlignViewToObject (bestView);
	}
}
