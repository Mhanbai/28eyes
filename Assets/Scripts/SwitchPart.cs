using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPart : MonoBehaviour {
	bool isZoomed = false;
	protected Camera cam;
	protected float startingSize;

	void Start() {
		cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		startingSize = cam.orthographicSize;
	}

	public void SwitchPartUp(int part) {
		switch (part) {
		case 0:
			if (PlayerInfo.Instance.headPart == (PlayerInfo.Instance.partManager.GetBody ().heads.Length - 1)) {
				PlayerInfo.Instance.headPart = 0;
			} else {
				PlayerInfo.Instance.headPart++;
			}
			break;
		case 1:
			if (PlayerInfo.Instance.bodyPart == (PlayerInfo.Instance.partManager.bodies.Length - 1)) {
				PlayerInfo.Instance.bodyPart = 0;
			} else {
				PlayerInfo.Instance.bodyPart++;
			}
			break;
		case 2:
			if (PlayerInfo.Instance.armPart == (PlayerInfo.Instance.partManager.GetBody ().l_arms.Length - 1)) {
				PlayerInfo.Instance.armPart = 0;
			} else {
				PlayerInfo.Instance.armPart++;
			}
			break;
		case 3:
			if (PlayerInfo.Instance.legPart == (PlayerInfo.Instance.partManager.GetBody ().r_legs.Length - 1)) {
				PlayerInfo.Instance.legPart = 0;
			} else {
				PlayerInfo.Instance.legPart++;
			}
			break;
		}
	}

	public void SwitchPartDown(int part) {
		switch (part) {
		case 0:
			if (PlayerInfo.Instance.headPart == 0) {
				PlayerInfo.Instance.headPart = (PlayerInfo.Instance.partManager.GetBody ().heads.Length - 1);
			} else {
				PlayerInfo.Instance.headPart--;
			}
			break;
		case 1:
			if (PlayerInfo.Instance.bodyPart == 0) {
				PlayerInfo.Instance.bodyPart = (PlayerInfo.Instance.partManager.bodies.Length - 1);
			} else {
				PlayerInfo.Instance.bodyPart--;
			}
			break;
		case 2:
			if (PlayerInfo.Instance.armPart == 0) {
				PlayerInfo.Instance.armPart = (PlayerInfo.Instance.partManager.GetBody ().l_arms.Length - 1);
			} else {
				PlayerInfo.Instance.armPart--;
			}
			break;
		case 3:
			if (PlayerInfo.Instance.legPart == 0) {
				PlayerInfo.Instance.legPart = (PlayerInfo.Instance.partManager.GetBody ().r_legs.Length - 1);
			} else {
				PlayerInfo.Instance.legPart--;
			}
			break;
		}
	}

	public void Zoom() {
		if (isZoomed == false) {
			cam.orthographicSize = 6.0f;
			isZoomed = true;
		} else {
			cam.orthographicSize = startingSize;
			isZoomed = false;
		}
	}
}
