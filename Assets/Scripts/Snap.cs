using UnityEngine;
using System.Collections;

// This script is executed in the editor
[ExecuteInEditMode]
public class Snap : MonoBehaviour {
	public bool snapToGrid = true;
	public float snapValue = 10.0f;

	// Adjust size and position
	void Update ()
	{
		if (snapToGrid) {
			transform.position = RoundTransform (transform.position, snapValue);
		}
		transform.position = new Vector3 (transform.position.x, 0.0f, transform.position.z);
	}

	// The snapping code
	private Vector3 RoundTransform (Vector3 v, float snapValue)
	{
		return new Vector3
			(
				snapValue * Mathf.Round(v.x / snapValue),
				snapValue * Mathf.Round(v.y / snapValue),
				snapValue * Mathf.Round(v.z / snapValue)
			);
	}
}
