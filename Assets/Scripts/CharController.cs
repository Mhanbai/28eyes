using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {
	//References
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

	public Animator headAnimator = null;
	public Animator bodyAnimator = null;
	public Animator rArmAnimator = null;
	public Animator lArmAnimator = null;
	public Animator rLegAnimator = null;
	public Animator lLegAnimator = null;

	bool playReload = true;
	bool isDead = false;
	float deathCounter = 0.0f;

	public GameObject avatar;
	float regenTimer = 0.0f;

	//Determines whether or not UI needs updating
	bool inLevel;

	// Use this for initialization
	void Start () {
		if (PlayerInfo.Instance.equippedAttack == null) {
			PlayerInfo.Instance.equippedAttack = AttackList.Instance.attackType [0];
		}

		//Find the players Character Controller
		characterController = GetComponent<CharacterController> ();
		avatar = GameObject.Find ("Character");

		//Find the camera
		cam = Camera.main;

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
		if (PlayerInfo.Instance.partManager == null) {
			PlayerInfo.Instance.FindPartManager ();
		}
		if ((!PlayerInfo.Instance.partManager.IsHeadActive(PlayerInfo.Instance.headPart)) || 
			(!PlayerInfo.Instance.partManager.IsBodyActive(PlayerInfo.Instance.bodyPart)) || 
			(!PlayerInfo.Instance.partManager.IsArmsActive(PlayerInfo.Instance.armPart)) || 
			(!PlayerInfo.Instance.partManager.IsLegsActive(PlayerInfo.Instance.legPart))) {
			PlayerInfo.Instance.partManager.ActivateBody (PlayerInfo.Instance.bodyPart);
			PlayerInfo.Instance.partManager.ActivateHead (PlayerInfo.Instance.headPart);
			PlayerInfo.Instance.partManager.ActivateArms (PlayerInfo.Instance.armPart);
			PlayerInfo.Instance.partManager.ActivateLegs (PlayerInfo.Instance.legPart);
		}

		transform.position = new Vector3 (transform.position.x, 0.0f, transform.position.z);

		if (isDead == false) {
			//Moving Code
			if (Input.GetKey (KeyCode.A)) {
				//Set Sprite and Animation
				if (!headAnimator.GetCurrentAnimatorStateInfo (0).IsName ("run")) {
					headAnimator.Play ("run", 0);
					bodyAnimator.Play ("run", 0);
					rArmAnimator.Play ("run", 0);
					lArmAnimator.Play ("run", 0);
					rLegAnimator.Play ("run", 0);
					lLegAnimator.Play ("run", 0);
				}
				avatar.transform.localScale = new Vector3 (Mathf.Abs(avatar.transform.localScale.x), avatar.transform.localScale.y, avatar.transform.localScale.z);
				//Move Avatar
				velX = -PlayerInfo.Instance.Speed () * Time.deltaTime;
				forwardVector = new Vector3 (-1.0f, 0.0f, forwardVector.z);
				isRunningX = true;
			} else if (Input.GetKey (KeyCode.D)) {
				//Set Sprite and Animation
				if (!headAnimator.GetCurrentAnimatorStateInfo (0).IsName ("run")) {
					headAnimator.Play ("run", 0);
					bodyAnimator.Play ("run", 0);
					rArmAnimator.Play ("run", 0);
					lArmAnimator.Play ("run", 0);
					rLegAnimator.Play ("run", 0);
					lLegAnimator.Play ("run", 0);
				}
				avatar.transform.localScale = new Vector3 (-Mathf.Abs(avatar.transform.localScale.x), avatar.transform.localScale.y, avatar.transform.localScale.z);
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
				if (!headAnimator.GetCurrentAnimatorStateInfo (0).IsName ("run")) {
					headAnimator.Play ("run", 0);
					bodyAnimator.Play ("run", 0);
					rArmAnimator.Play ("run", 0);
					lArmAnimator.Play ("run", 0);
					rLegAnimator.Play ("run", 0);
					lLegAnimator.Play ("run", 0);
				}

				//Move Avatar
				velZ = PlayerInfo.Instance.Speed () * Time.deltaTime;
				forwardVector = new Vector3 (forwardVector.x, 0.0f, 1.0f);
				isRunningZ = true;
			} else if (Input.GetKey (KeyCode.S)) {
				//Set Sprite and Animation
				if (!headAnimator.GetCurrentAnimatorStateInfo (0).IsName ("run")) {
					headAnimator.Play ("run", 0);
					bodyAnimator.Play ("run", 0);
					rArmAnimator.Play ("run", 0);
					lArmAnimator.Play ("run", 0);
					rLegAnimator.Play ("run", 0);
					lLegAnimator.Play ("run", 0);
				}

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
				if (!headAnimator.GetCurrentAnimatorStateInfo (0).IsName ("idle")) {
					headAnimator.Play ("idle", 0);
					bodyAnimator.Play ("idle", 0);
					rArmAnimator.Play ("idle", 0);
					lArmAnimator.Play ("idle", 0);
					rLegAnimator.Play ("idle", 0);
					lLegAnimator.Play ("idle", 0);
				}
				PlayerInfo.Instance.SetPlayerActive (false);
			} else {
				PlayerInfo.Instance.SetPlayerActive (true);
			}

			characterController.Move (new Vector3 (velX, 0.0f, velZ));

			//Attacking Code
			if ((Input.GetMouseButtonDown (0)) && (PlayerInfo.Instance.attackCount < PlayerInfo.Instance.AttackStyle ().Uses) && (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ())) {
				//Storage variables to use with fucntions
				RaycastHit info;
				LayerMask ground = LayerMask.GetMask ("Ground");
				//Find where on the screen was clicked
				Ray ray = cam.ScreenPointToRay (Input.mousePosition);
				//Shoot a ray from this point in the direction the camera is facing
				if (Physics.Raycast (ray, out info, Mathf.Infinity, ground)) {
					//Get direction between player and resulting point 
					Vector3 clickDistance = new Vector3 (info.point.x, player.transform.position.y, info.point.z) - player.transform.position;
					//Attack in direction
					Attack (Vector3.Normalize (clickDistance), clickDistance);
					PlayerInfo.Instance.attackCount++;
				}
			}

			if (PlayerInfo.Instance.attackCount >= (PlayerInfo.Instance.AttackStyle ().Uses + PlayerInfo.Instance.ammoDiff)) {
				if (playReload) {
					rArmAnimator.Play ("reload", 0);
					lArmAnimator.Play ("reload", 0);
					playReload = false;
				}
				PlayerInfo.Instance.SetAttackReady (false);
				PlayerInfo.Instance.reloadTimer += Time.deltaTime;
				if (PlayerInfo.Instance.reloadTimer > (PlayerInfo.Instance.AttackStyle ().ReloadTime + (PlayerInfo.Instance.AttackStyle ().ReloadTime * PlayerInfo.Instance.reloadDiff))) {
					PlayerInfo.Instance.reloadTimer = 0.0f;
					PlayerInfo.Instance.attackCount = 0;
					PlayerInfo.Instance.SetAttackReady (true);
					playReload = true;
				}
			}
		}

		if (PlayerInfo.Instance.CurrentHealth () <= 0.0f) {
			isDead = true;
			deathCounter += Time.deltaTime;
			if (!headAnimator.GetCurrentAnimatorStateInfo (0).IsName ("death")) {
				headAnimator.Play ("death", 0);
				bodyAnimator.Play ("death", 0);
				rArmAnimator.Play ("death", 0);
				lArmAnimator.Play ("death", 0);
				rLegAnimator.Play ("death", 0);
				lLegAnimator.Play ("death", 0);
			}
			if (deathCounter > 3.0f) {
				deathCounter = 0.0f;
				isDead = false;
				PlayerInfo.Instance.Die ();
			}
		} else if (PlayerInfo.Instance.CurrentHealth () < PlayerInfo.Instance.MaxHealth ()) {
			regenTimer += Time.deltaTime;
			if (regenTimer > 3.0f) {
				regenTimer = 0.0f;
				PlayerInfo.Instance.Heal (5);
			}
		}
	}

	void Attack(Vector3 direction, Vector3 clickDistance) {
		if (!rArmAnimator.GetCurrentAnimatorStateInfo (0).IsName ("attack")) {
			rArmAnimator.Play ("attack", 0);
			lArmAnimator.Play ("attack", 0);
		}
		PlayerInfo.Instance.SetPlayerActive (true);
		for (int i = 0; i < PlayerInfo.Instance.AttackStyle().NoOfProjectiles; i++) {
			GameObject projectileObject = GameObject.Instantiate (PlayerInfo.Instance.AttackStyle().Projectile);
			ProjectileBehaviour projectileBehaviour = projectileObject.GetComponent<ProjectileBehaviour> ();

			projectileObject.transform.position = PlayerInfo.Instance.firePoint.transform.position;
			projectileBehaviour.startingLocation = player.transform.position;
			projectileBehaviour.speed = PlayerInfo.Instance.AttackStyle().ProjectileSpeed - (i * 2);
			projectileBehaviour.direction = Quaternion.AngleAxis(i * 10.0f, new Vector3(0.0f, 1.0f, 0.0f)) * direction;
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

	public Vector3 GetVelocityVector() {
		return new Vector3 (velX, 0.0f, velZ);
	}
}
