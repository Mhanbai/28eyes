﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {
	public Animator animController;
	public SpriteRenderer sprite;
	public GameObject player;

	protected float speed;
	protected float maxHealth;
	protected int attackStyle;

	protected float velX;
	protected float velZ;

	protected bool isRunningX = false;
	protected bool isRunningZ = false;

	Canvas UI;
	Camera cam;

	int layer_mask;

	// Use this for initialization
	void Start () {
		UI = GameObject.Find ("UI").GetComponent<Canvas> ();
		cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		layer_mask = LayerMask.GetMask ("Ground");

		UpdateSpeed ();
		UpdateMaxHealth ();
		PlayerInfo.Instance.inventory [0] = ItemList.Instance.dryadHeart;
		PlayerInfo.Instance.inventory [1] = ItemList.Instance.ghoulClaw;
		PlayerInfo.Instance.inventory [2] = ItemList.Instance.unicornLeg;
		PlayerInfo.Instance.inventory [3] = ItemList.Instance.redPortalStone;
		PlayerInfo.Instance.inventory [4] = ItemList.Instance.bluePortalStone;
		PlayerInfo.Instance.inventory [5] = ItemList.Instance.greenPortalStone;
	}
	
	// Update is called once per frame
	void Update () {
		//Moving Code
		if (Input.GetKey (KeyCode.A)) {
			//Set Sprite and Animation
			animController.SetBool ("arrowPressed", true);
			sprite.flipX = true;
			//Move Avatar
			velX = -speed;
			isRunningX = true;
		} else if (Input.GetKey (KeyCode.D)) {
			//Set Sprite and Animation
			animController.SetBool ("arrowPressed", true);
			sprite.flipX = false;
			//Move Avatar
			velX = speed;
			isRunningX = true;
		} else {
			velX = 0.0f;
			isRunningX = false;
		}

		if (Input.GetKey (KeyCode.W)) {
			//Set Sprite and Animation
			animController.SetBool ("arrowPressed", true);
			//Move Avatar
			velZ = speed;
			isRunningZ = true;
		} else if (Input.GetKey (KeyCode.S)) {
			//Set Sprite and Animation
			animController.SetBool ("arrowPressed", true);
			//Move Avatar
			velZ = -speed;
			isRunningZ = true;
		} else {
			velZ = 0.0f;
			isRunningZ = false;
		}
	

		if ((isRunningX == false) && (isRunningZ == false)){
			//Stop animation
			animController.SetBool ("arrowPressed", false);
		}

		player.transform.position = new Vector3 (player.transform.position.x + velX, player.transform.position.y, player.transform.position.z + velZ);

		//Attacking code
		if (Input.GetMouseButtonDown (0)) {
			if (!IsPressOverUIObject (Input.mousePosition)) {
				RaycastHit result;
				Ray ray = cam.ScreenPointToRay (Input.mousePosition);
				//Debug.DrawRay (ray.origin, ray.direction * 100.0f, Color.green, 90.0f);
				if (Physics.Raycast (ray, out result, Mathf.Infinity, layer_mask)) {
					Attack (result.point);
				}
			}
		}
	}

	void UpdateSpeed() {
		speed = PlayerInfo.Instance.Speed ();
	}

	void UpdateMaxHealth() {
		maxHealth = PlayerInfo.Instance.MaxHealth ();
	}

	public bool IsRunningOrShooting() {
		if ((velZ != 0.0f) || (velX != 0.0f)) {
			return true;
		} else {
			return false;
		}
	}

	private bool IsPressOverUIObject (Vector2 screenPosition) {
		UnityEngine.EventSystems.PointerEventData eventDataCurrentPosition = new UnityEngine.EventSystems.PointerEventData (UnityEngine.EventSystems.EventSystem.current);
		eventDataCurrentPosition.position = screenPosition;

		UnityEngine.UI.GraphicRaycaster uiRaycaster = UI.GetComponent<UnityEngine.UI.GraphicRaycaster> ();
		List <UnityEngine.EventSystems.RaycastResult> results = new List<UnityEngine.EventSystems.RaycastResult> ();
		uiRaycaster.Raycast (eventDataCurrentPosition, results);
		return results.Count > 0;
	}

	private void Attack(Vector3 point) {
		Vector3 attackPt0 = new Vector3 (player.transform.position.x, 0.0f, player.transform.position.z);
		Vector3 attackPt1 = new Vector3 (player.transform.position.x, 1.0f, player.transform.position.z);
		Vector3 attackPt2 = new Vector3 (player.transform.position.x, 2.0f, player.transform.position.z);
		Vector3 attackPt3 = new Vector3 (player.transform.position.x, 3.0f, player.transform.position.z);

		RaycastHit result;
		Vector3 direction = point - attackPt0;

		switch(PlayerInfo.Instance.AttackStyle()) {
		case 0:
			Debug.DrawRay (attackPt2, direction, Color.red, 0.1f);

			if (Physics.Raycast (attackPt1, direction, out result)) {
				result.transform.GetComponentInParent<MonsterClass> ().TakeHit ();
			} else if (Physics.Raycast (attackPt2, direction, out result)) {
				result.transform.GetComponentInParent<MonsterClass> ().TakeHit ();
			} else if (Physics.Raycast (attackPt3, direction, out result)) {
				result.transform.GetComponentInParent<MonsterClass> ().TakeHit ();
			}
			break;
		default:
			break;
		}
	}
}