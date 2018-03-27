using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationViewer : MonoBehaviour {
	public GameObject character;
	BodyManager bodyManager;
	PartManager partManager;

	// Use this for initialization
	void Start () {
		partManager = character.GetComponentInChildren<PartManager> ();
		bodyManager = partManager.GetComponentInChildren<BodyManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		partManager.ActivateBody (0);
		bodyManager.ActivateHead (0);
		bodyManager.ActivateLegs (0);
		bodyManager.ActivateArms (0);
	}
}
