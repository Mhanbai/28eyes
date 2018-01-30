using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class PlayFromZero : MonoBehaviour {
	[UnityEditor.MenuItem("28Eyes Tools/Play from Preload Scene")]
	public static void PlayFromPrelaunchScene()
	{
		if ( UnityEditor.EditorApplication.isPlaying == true )
		{
			UnityEditor.EditorApplication.isPlaying = false;
			return;
		}

		UnityEditor.SceneManagement.EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
		UnityEditor.SceneManagement.EditorSceneManager.OpenScene ("Assets/Scenes/_preload.unity");
		UnityEditor.EditorApplication.isPlaying = true;
	}
}
