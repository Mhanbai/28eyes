using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		float x = transform.position.x - 0.1f;

		transform.position = new Vector3 (x, transform.position.y, transform.position.z);
	}
}
