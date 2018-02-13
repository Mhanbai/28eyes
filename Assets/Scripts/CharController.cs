using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {
	//References
	public Animator animController;
	public SpriteRenderer sprite;
	public GameObject player;
	Canvas UI;
	Camera cam;

	//Values for movement
	protected float velX;
	protected float velZ;
	protected bool isRunningX = false;
	protected bool isRunningZ = false;
	protected Vector3 forwardVector;
	CharacterController characterController;

	//Determines whether or not UI needs updating
	bool inLevel;

	// Use this for initialization
	void Start () {
		//Find the players Character Controller
		characterController = GetComponent<CharacterController> ();

		//Find the camera
		cam = GameObject.Find("Main Camera").GetComponent<Camera>();

		//Try to find UI
		try {
			UI = GameObject.Find ("UI(Clone)").GetComponent<Canvas> ();
			inLevel = true;
		}
		//If it cant be found, we are not in a level
		catch (System.Exception) {
			inLevel = false;
		}

		//Debug add items
		PlayerInfo.Instance.inventory [0] = ItemList.Instance.portalOneItems[0];
		PlayerInfo.Instance.inventory [1] = ItemList.Instance.portalTwoItems[0];
		PlayerInfo.Instance.inventory [2] = ItemList.Instance.portalThreeItems[0];
	}

	void Update () {
		//Moving Code
		if (Input.GetKey (KeyCode.A)) {
			//Set Sprite and Animation
			animController.SetBool ("arrowPressed", true);
			sprite.flipX = true;
			//Move Avatar
			velX = -PlayerInfo.Instance.Speed () * Time.deltaTime;
			forwardVector = new Vector3 (-1.0f, 0.0f, forwardVector.z);
			isRunningX = true;
		} else if (Input.GetKey (KeyCode.D)) {
			//Set Sprite and Animation
			animController.SetBool ("arrowPressed", true);
			sprite.flipX = false;
			//Move Avatar
			velX = PlayerInfo.Instance.Speed () * Time.deltaTime;
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
			velZ = PlayerInfo.Instance.Speed () * Time.deltaTime;
			forwardVector = new Vector3 (forwardVector.x, 0.0f, 1.0f);
			isRunningZ = true;
		} else if (Input.GetKey (KeyCode.S)) {
			//Set Sprite and Animation
			animController.SetBool ("arrowPressed", true);
			//Move Avatar
			velZ = -PlayerInfo.Instance.Speed () * Time.deltaTime;
			forwardVector = new Vector3 (forwardVector.x, 0.0f, -1.0f);
			isRunningZ = true;
		} else {
			velZ = 0.0f;
			isRunningZ = false;
		}

		forwardVector.Normalize ();
	
		if ((isRunningX == false) && (isRunningZ == false)) {
			//Stop animation
			animController.SetBool ("arrowPressed", false);
		}

		characterController.Move (new Vector3 (velX, 0.0f, velZ));

		//Attacking Code
		if ((Input.GetMouseButtonDown(0)) && (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())) {
			//Storage variables to use with fucntions
			RaycastHit info;
			LayerMask ground = LayerMask.GetMask("Ground");
			//Find where on the screen was clicked
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			//Shoot a ray from this point in the direction the camera is facing
			if (Physics.Raycast (ray, out info, Mathf.Infinity, ground)) {
				//Get direction between player and resulting point 
				Vector3 direction = Vector3.Normalize (info.point - player.transform.position);
				//Attack in direction
				Attack (direction);
			}
		}
	}

	void Attack(Vector3 direction) {
		Debug.DrawRay (player.transform.position, direction * 200.0f, Color.red, 100.0f);
	}

	public bool IsRunningOrShooting() {
		if ((velZ != 0.0f) || (velX != 0.0f)) {
			return true;
		} else {
			return false;
		}
	}

	public Vector3 GetForwardVector() {
		return forwardVector;
	}

	public float GetSpeed() {
		return Vector3.Magnitude(new Vector3(velX, 0.0f, velZ));
	}
}
