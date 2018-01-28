using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {
	public Animator animController;
	public SpriteRenderer sprite;
	public GameObject player;

	protected float speed;
	protected float maxHealth;
	protected float attackSpeed;
	protected int attackStyle;

	protected float velX;
	protected float velZ;

	protected bool isRunningX = false;
	protected bool isRunningZ = false;

	// Use this for initialization
	void Start () {
		UpdateSpeed ();
		UpdateMaxHealth ();
		UpdateAtack ();
		PlayerInfo.Instance.inventory [0] = null;
		PlayerInfo.Instance.inventory [1] = ItemList.Instance.ghoulClaw;
		PlayerInfo.Instance.inventory [2] = ItemList.Instance.unicornLeg;
		PlayerInfo.Instance.inventory [3] = ItemList.Instance.redPortalStone;
		PlayerInfo.Instance.inventory [4] = ItemList.Instance.bluePortalStone;
		PlayerInfo.Instance.inventory [5] = ItemList.Instance.greenPortalStone;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			//Set Sprite and Animation
			animController.SetBool ("arrowPressed", true);
			sprite.flipX = true;
			//Move Avatar
			velX = -speed;
			isRunningX = true;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
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

		if (Input.GetKey (KeyCode.UpArrow)) {
			//Set Sprite and Animation
			animController.SetBool ("arrowPressed", true);
			//Move Avatar
			velZ = speed;
			isRunningZ = true;
		} else if (Input.GetKey (KeyCode.DownArrow)) {
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
	}

	void UpdateSpeed() {
		speed = PlayerInfo.Instance.Speed ();
	}

	void UpdateMaxHealth() {
		maxHealth = PlayerInfo.Instance.MaxHealth ();
	}

	void UpdateAtack() {
		PlayerInfo.Instance.ChangeAttack (ref attackSpeed, ref attackStyle); 
	}
}
