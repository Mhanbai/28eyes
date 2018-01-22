using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walk : MonoBehaviour {
	float time;
	float speed = 5.0f;
	float width = 0.6f;
	float height = 0.3f;
	Vector3 position;

	void Start() {
		time = time + GetInstanceID ();
		//float time = Random.Range(0.0f, 20.0f);
		position = transform.position;
	}

	// Update is called once per frame
	void Update () {
		time += Time.deltaTime * speed;

		position = new Vector3 (position.x - 0.1f, position.y, position.z);

		float x = position.x - Mathf.Cos (time) * width;
		float y = position.y - Mathf.Sin (time) * height;
		float z = transform.position.z;

		transform.position = new Vector3 (x, y, z);
	}
}