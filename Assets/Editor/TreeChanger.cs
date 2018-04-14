using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TreeChanger : MonoBehaviour {

	[MenuItem("28Eyes Tools/Swap Assets")]
	public static void SwapAssets () {
		LevelEditor editor = GameObject.Find("Level Editor").GetComponent<LevelEditor>();
		Transform[] swappers = editor.assetsToSwap.GetComponentsInChildren<Transform> ();

		//for (int i = 0; Transform toSwap in swappers) {
        for (int i = 0; i < swappers.Length; i++) {
			GameObject tree = GameObject.Instantiate (editor.swapTo);
			//tree.transform.SetParent(editor.assetsToSwap.transform);
			tree.transform.position = swappers[i].position;
		}
	}
}
