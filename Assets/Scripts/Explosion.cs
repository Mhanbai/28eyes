using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
	public Collider collider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		collider.transform.localScale += new Vector3 (0.1f, 0.1f, 0.1f);
	}
}
