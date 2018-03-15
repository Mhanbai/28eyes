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
		if (PlayerInfo.Instance.equippedAttack == null) {
			PlayerInfo.Instance.equippedAttack = AttackList.Instance.attackType [0];
		}

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
			PlayerInfo.Instance.SetPlayerActive (false);
		} else {
			PlayerInfo.Instance.SetPlayerActive (true);
		}

		characterController.Move (new Vector3 (velX, 0.0f, velZ));

		//Attacking Code
		if ((Input.GetMouseButtonDown(0)) && (PlayerInfo.Instance.attackCount < PlayerInfo.Instance.AttackStyle().Uses) && (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())) {
			//Storage variables to use with fucntions
			RaycastHit info;
			LayerMask ground = LayerMask.GetMask("Ground");
			//Find where on the screen was clicked
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			//Shoot a ray from this point in the direction the camera is facing
			if (Physics.Raycast (ray, out info, Mathf.Infinity, ground)) {
				//Get direction between player and resulting point 
				Vector3 clickDistance = new Vector3(info.point.x, player.transform.position.y, info.point.z) - player.transform.position;
				//Attack in direction
				Attack (Vector3.Normalize (clickDistance), clickDistance);
				PlayerInfo.Instance.attackCount++;
			}
		}

		if (PlayerInfo.Instance.attackCount >= (PlayerInfo.Instance.AttackStyle().Uses +  PlayerInfo.Instance.ammoDiff)) {
			PlayerInfo.Instance.SetAttackReady(false);
			PlayerInfo.Instance.reloadTimer += Time.deltaTime;
			if (PlayerInfo.Instance.reloadTimer > (PlayerInfo.Instance.AttackStyle().ReloadTime + (PlayerInfo.Instance.AttackStyle().ReloadTime * PlayerInfo.Instance.reloadDiff))) {
				PlayerInfo.Instance.reloadTimer = 0.0f;
				PlayerInfo.Instance.attackCount = 0;
				PlayerInfo.Instance.SetAttackReady(true);
			}
		}

		if (PlayerInfo.Instance.CurrentHealth () <= 0.0f) {
			PlayerInfo.Instance.Die ();
		}
	}

	void Attack(Vector3 direction, Vector3 clickDistance) {
		PlayerInfo.Instance.SetPlayerActive (true);
		for (int i = 0; i < PlayerInfo.Instance.AttackStyle().NoOfProjectiles; i++) {
			GameObject projectileObject = GameObject.Instantiate (PlayerInfo.Instance.AttackStyle().Projectile);
			ProjectileBehaviour projectileBehaviour = projectileObject.GetComponent<ProjectileBehaviour> ();

			projectileObject.transform.position = player.transform.position;
			projectileBehaviour.startingLocation = player.transform.position;
			projectileBehaviour.speed = PlayerInfo.Instance.AttackStyle().ProjectileSpeed - (i * 2);
			projectileBehaviour.direction = direction;
			projectileBehaviour.trajectoryType = PlayerInfo.Instance.AttackStyle().TrajectoryType;

			if (projectileBehaviour.trajectoryType == 1) {
				projectileBehaviour.range = Mathf.Clamp (Vector3.Magnitude (clickDistance), 0.0f, (PlayerInfo.Instance.AttackStyle().Range + (PlayerInfo.Instance.AttackStyle().Range * PlayerInfo.Instance.rangeDiff)));
			} else {
				projectileBehaviour.range = (PlayerInfo.Instance.AttackStyle().Range + (PlayerInfo.Instance.AttackStyle().Range * PlayerInfo.Instance.rangeDiff));
			}

			projectileBehaviour.damage = PlayerInfo.Instance.AttackStyle().Damage;
			projectileBehaviour.causesPosion = PlayerInfo.Instance.AttackStyle().Poison;
			projectileBehaviour.causesBleed = PlayerInfo.Instance.AttackStyle().Bleed;
			projectileBehaviour.causesSlow = PlayerInfo.Instance.AttackStyle().Slow;
		}
	}

	public Vector3 GetForwardVector() {
		return forwardVector;
	}

	public float GetSpeed() {
		return Vector3.Magnitude(new Vector3(velX, 0.0f, velZ));
	}
}
