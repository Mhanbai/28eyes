﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {
	public Animator animController;
	public SpriteRenderer sprite;
	public GameObject player;

	protected int attackStyle;
	protected float attackCooldown;
	protected float attackCooldownCount = 0.0f;

	protected float velX;
	protected float velZ;

	protected bool isRunningX = false;
	protected bool isRunningZ = false;

	protected Vector3 forwardVector;

	CharacterController characterController;

	Canvas UI;
	Camera cam;

	int layer_mask;

	bool inLevel;

	// Use this for initialization
	void Start () {
		try {
			UI = GameObject.Find ("UI(Clone)").GetComponent<Canvas> ();
			inLevel = true;
		}
		catch (System.Exception) {
			inLevel = false;
		}

		cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		layer_mask = LayerMask.GetMask ("Ground");

		forwardVector = new Vector3 (1.0f, 0.0f, 0.0f);

		characterController = GetComponent<CharacterController> ();

		PlayerInfo.Instance.inventory [0] = ItemList.Instance.portalOneItems[0];
		PlayerInfo.Instance.inventory [1] = ItemList.Instance.portalTwoItems[0];
		PlayerInfo.Instance.inventory [2] = ItemList.Instance.portalThreeItems[0];
	}
	
	// Update is called once per frame
	void Update () {
		//Moving Code
		if (Input.GetKey (KeyCode.A)) {
			//Set Sprite and Animation
			animController.SetBool ("arrowPressed", true);
			sprite.flipX = true;
			//Move Avatar
			velX = -PlayerInfo.Instance.Speed() * Time.deltaTime;
			forwardVector = new Vector3 (-1.0f, 0.0f, forwardVector.z);
			isRunningX = true;
		} else if (Input.GetKey (KeyCode.D)) {
			//Set Sprite and Animation
			animController.SetBool ("arrowPressed", true);
			sprite.flipX = false;
			//Move Avatar
			velX = PlayerInfo.Instance.Speed() * Time.deltaTime;
			isRunningX = true;
			forwardVector = new Vector3 (1.0f, 0.0f, forwardVector.z);
		} else {
			velX = 0.0f;
			isRunningX = false;
		}

		if (Input.GetKey (KeyCode.W)) {
			//Set Sprite and Animation
			animController.SetBool ("arrowPressed", true);
			//Move Avatar
			velZ = PlayerInfo.Instance.Speed() * Time.deltaTime;
			forwardVector = new Vector3 (forwardVector.x, 0.0f, 1.0f);
			isRunningZ = true;
		} else if (Input.GetKey (KeyCode.S)) {
			//Set Sprite and Animation
			animController.SetBool ("arrowPressed", true);
			//Move Avatar
			velZ = -PlayerInfo.Instance.Speed() * Time.deltaTime;
			forwardVector = new Vector3 (forwardVector.x, 0.0f, -1.0f);
			isRunningZ = true;
		} else {
			velZ = 0.0f;
			isRunningZ = false;
		}

		forwardVector.Normalize ();
	
		if ((isRunningX == false) && (isRunningZ == false)){
			//Stop animation
			animController.SetBool ("arrowPressed", false);
		}

		characterController.Move(new Vector3(velX, 0.0f, velZ));

		if (inLevel == true) {
			if ((Input.GetMouseButtonDown (0)) && (PlayerInfo.Instance.IsAttackReady () == true)) {
				if (!IsPressOverUIObject (Input.mousePosition)) {
					RaycastHit result;
					Ray ray = cam.ScreenPointToRay (Input.mousePosition);
					if (Physics.Raycast (ray, out result, Mathf.Infinity, layer_mask)) {
						Attack (result.point);
						PlayerInfo.Instance.SetAttackReady (false);
					}
				}
			}

			if (PlayerInfo.Instance.IsAttackReady () == false) {
				attackCooldownCount += Time.deltaTime;
				if (attackCooldownCount > PlayerInfo.Instance.AttackCooldown ()) {
					attackCooldownCount = 0.0f;
					PlayerInfo.Instance.SetAttackReady (true);
				}
			}

			if (IsRunningOrShooting ()) {
				PlayerInfo.Instance.SetPlayerActive (true);
			} else {
				PlayerInfo.Instance.SetPlayerActive (false);
			}
		}
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
				try {
				result.transform.GetComponent<MonsterClass> ().TakeHit ();
				}
				catch {
				}
			} else if (Physics.Raycast (attackPt2, direction, out result)) {
				try {
				result.transform.GetComponent<MonsterClass> ().TakeHit ();
				}
				catch {
				}
			} else if (Physics.Raycast (attackPt3, direction, out result)) {
				try {
				result.transform.GetComponent<MonsterClass> ().TakeHit ();
				}
				catch {
				}
			}
			break;
		default:
			break;
		}
	}

	public Vector3 GetForwardVector() {
		return forwardVector;
	}

	public float GetSpeed() {
		return Vector3.Magnitude(new Vector3(velX, 0.0f, velZ));
	}
}
