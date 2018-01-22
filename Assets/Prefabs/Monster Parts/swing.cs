using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swing : MonoBehaviour {
	float time = 0.0f;
	float range = 4.0f;
	float speed = 3.0f;

	// Update is called once per frame
	void Update () {
		time += Time.deltaTime * speed;

		transform.rotation = Quaternion.Euler (new Vector3(0.0f, 0.0f, Mathf.Sin (time) * range));
	}
}
