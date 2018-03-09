using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TreeChanger : MonoBehaviour {

	[MenuItem("28Eyes Tools/Swap Assets")]
	// Update is called once per frame
	public static void SwapAssets () {
		LevelEditor editor = GameObject.Find("Level Editor").GetComponent<LevelEditor>();

		Transform[] swappers = editor.assetsToSwap.GetComponentsInChildren<Transform> ();

		foreach (Transform toSwap in swappers) {
			GameObject whatToSwap = toSwap.gameObject;
			Debug.Log (whatToSwap);
			GameObject tree = GameObject.Instantiate (editor.swapTo);
			tree.transform.SetParent(editor.assetsToSwap.transform);
			tree.transform.position = toSwap.position;
			GameObject.DestroyImmediate (whatToSwap);
		}
	}
}
