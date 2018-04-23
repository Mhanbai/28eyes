using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {
	//The following code ensures that there is a single global PlayerInfo class available for other objects to use 
	public static PlayerInfo Instance { get; private set; }

	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else {
			Debug.Log ("Warning, there is more than one PlayerInfo class in the scene!");
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// Class: PlayerInfo
	///////////////////////////////////////////////////////////////////////////////////////////////////////////////

	//TODO: Change stats based on design
	[SerializeField] protected int maxHealth = 100; //Percentage
	[SerializeField] protected int currentHealth = 100; //Percentage
	[SerializeField] protected float maxSpeed = 3.0f; //Pixels per second
	[SerializeField] protected float minimumSpeed = 0.1f; 
	protected bool playerIsActive = false;
	protected bool attackIsReady = true;

	public int attackCount = 0;
	public Attack equippedAttack;
	public float reloadTimer = 0.0f;

	public Item[] inventory = new Item[6];

	public Item headItem;
	public Item bodyItem;
	public Item armItem;
	public Item legItem;

	public GameObject firePoint;

	public int ammoDiff = 0;
	public float reloadDiff = 0.0f;
	public float rangeDiff = 0.0f;

	public int bodyPart = 0;
	public int headPart = 0;
	public int armPart = 0;
	public int legPart = 0;

	public PartManager partManager = null;

	void OnDestroy() {
		DataManager.Instance.Save ();
	}

	void Start() {
		DataManager.Instance.Load ();
	}

	//Functions for combat
	public void Hit(int damage) {
		currentHealth = currentHealth - damage;
		currentHealth = Mathf.Clamp (currentHealth, 0, maxHealth);
	}

	public void Heal(int healing) {
		currentHealth = currentHealth + healing;
		currentHealth = Mathf.Clamp (currentHealth, 0, maxHealth);
	}

	//Functions for pickups
	public Attack AttackStyle() {
		return equippedAttack;
	}


	public float Speed() {
		return maxSpeed;
	}

	public void SetMaxSpeed(float speed_in) {
		maxSpeed = speed_in;

		if (maxSpeed < minimumSpeed) {
			maxSpeed = minimumSpeed;
		}
	}

	public int MaxHealth() {
		return maxHealth;
	}

	public void SetMaxHealth(int max_health_in) {
		maxHealth = max_health_in;

		if (maxHealth < 1) {
			maxHealth = 1;
		}

		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}
	}

	public int CurrentHealth() {
		if (currentHealth >= 0) {
			return currentHealth;
		} else {
			return 0;
		}
	}

    public void SetCurrentHealth(int health_in)
    {
        currentHealth = health_in;
    }

	public bool IsRunningOrShooting() {
		return playerIsActive;
	}

	public void SetPlayerActive(bool isActive) {
		playerIsActive = isActive;
	}

	public bool IsAttackReady() {
		return attackIsReady;
	}

	public void SetAttackReady(bool attackState) {
		attackIsReady = attackState;
	}

	public bool AddToInventory(Item toAdd) {
		int index = -1;
		foreach (Item item in inventory) {
			index++;
			if (item == null) {
				inventory [index] = toAdd;
				return true;
			}
		}

		return false;
	}

	public void UseItem(Item toUse) {
		partManager = null;
		GameObject player = GameObject.Find("Character");
		if (player != null) {
			partManager = player.GetComponentInChildren<PartManager> ();
		}
		switch (toUse.itemSet) {
		case 0:
			if ((headItem != null) && (headItem != toUse)) {
				SetMaxHealth (maxHealth + toUse.healthChange);
				SetMaxSpeed (maxSpeed + (maxSpeed * toUse.speedChange));
				ammoDiff = toUse.ammoChange;
				reloadDiff = toUse.reloadChange;
				rangeDiff = toUse.rangeChange;

				if (toUse.attackType != -1) {
					equippedAttack = PlayerInfo.Instance.equippedAttack = AttackList.Instance.attackType [toUse.attackType];
				}
					
				SetMaxHealth (maxHealth - headItem.healthChange);
				SetMaxSpeed (maxSpeed + (maxSpeed * -headItem.speedChange));

				headPart = toUse.bodyPart;

				if (partManager != null) {
					partManager.ActivateHead (headPart);
				}

				headItem = toUse;
			}
			break;
		case 1:
			if ((bodyItem != null) && (bodyItem != toUse)) {
				SetMaxHealth (maxHealth + toUse.healthChange);
				SetMaxSpeed (maxSpeed + (maxSpeed * toUse.speedChange));
				ammoDiff = toUse.ammoChange;
				reloadDiff = toUse.reloadChange;
				rangeDiff = toUse.rangeChange;

				if (toUse.attackType != -1) {
					equippedAttack = PlayerInfo.Instance.equippedAttack = AttackList.Instance.attackType [toUse.attackType];
				}

				SetMaxHealth (maxHealth - bodyItem.healthChange);
				SetMaxSpeed (maxSpeed + (maxSpeed * -bodyItem.speedChange));

				bodyPart = toUse.bodyPart;

				if (partManager != null) {
					partManager.ActivateBody (bodyPart);
					partManager.ActivateHead (headPart);
					partManager.ActivateArms (armPart);
					partManager.ActivateLegs (legPart);
				}
			}

			bodyItem = toUse;
			break;
		case 2:
			if ((armItem != null) && (armItem != toUse)) {
				SetMaxHealth (maxHealth + toUse.healthChange);
				SetMaxSpeed (maxSpeed + (maxSpeed * toUse.speedChange));
				ammoDiff = toUse.ammoChange;
				reloadDiff = toUse.reloadChange;
				rangeDiff = toUse.rangeChange;

				if (toUse.attackType != -1) {
					equippedAttack = PlayerInfo.Instance.equippedAttack = AttackList.Instance.attackType [toUse.attackType];
				}

				SetMaxHealth (maxHealth - armItem.healthChange);
				SetMaxSpeed (maxSpeed + (maxSpeed * -armItem.speedChange));

				armPart = toUse.bodyPart;

				if (partManager != null) {
					partManager.ActivateArms (armPart);
				}
			}

			armItem = toUse;
			break;
		case 3:
			if ((legItem != null) && (legItem != toUse)) {
				SetMaxHealth (maxHealth + toUse.healthChange);
				SetMaxSpeed (maxSpeed + (maxSpeed * toUse.speedChange));
				ammoDiff = toUse.ammoChange;
				reloadDiff = toUse.reloadChange;
				rangeDiff = toUse.rangeChange;

				if (toUse.attackType != -1) {
					equippedAttack = PlayerInfo.Instance.equippedAttack = AttackList.Instance.attackType [toUse.attackType];
				}

				SetMaxHealth (maxHealth - legItem.healthChange);
				SetMaxSpeed (maxSpeed + (maxSpeed * -legItem.speedChange));

				legPart = toUse.bodyPart;

				if (partManager != null) {
					partManager.ActivateLegs (legPart);
				}
			}

			legItem = toUse;
			break;
		}
	}

	public void Load(int health_in, Item headPart_in, Item bodyPart_in, Item armPart_in, Item legPart_in, int headSprite_in, int bodySprite_in, int armSprite_in, int legSprite_in) {
        if (headPart_in != null)
        {
            UseItem(headPart_in);
        }
        if (bodyPart_in != null)
        {
            UseItem(bodyPart_in);
        }
        if (armPart_in != null)
        {
            UseItem(armPart_in);
        }
        if (legPart_in != null)
        {
            UseItem(legPart_in);
        }
		headPart = headSprite_in;
		bodyPart = bodySprite_in;
		armPart = armSprite_in;
		legPart = legSprite_in;
		currentHealth = health_in;
	}

	public void Die() {
		SceneManager.Instance.GameOver ();
		currentHealth = maxHealth;
		inventory = new Item[6];
		if (headItem != null) {
			SetMaxHealth (maxHealth - headItem.healthChange);
			SetMaxSpeed (maxSpeed + (maxSpeed * -headItem.speedChange));
			headItem = null;
			headPart = 0;
		}
		if (bodyItem != null) {
			SetMaxHealth (maxHealth - bodyItem.healthChange);
			SetMaxSpeed (maxSpeed + (maxSpeed * -bodyItem.speedChange));
			bodyItem = null;
			bodyPart = 0;
		}
		if (armItem != null) {
			SetMaxHealth (maxHealth - armItem.healthChange);
			SetMaxSpeed (maxSpeed + (maxSpeed * -armItem.speedChange));
			armItem = null;
			armPart = 0;
		}
		if (legItem != null) {
			SetMaxHealth (maxHealth - legItem.healthChange);
			SetMaxSpeed (maxSpeed + (maxSpeed * -legItem.speedChange));
			legItem = null;
			legPart = 0;
		}
		ammoDiff = 0;
		reloadDiff = 0.0f;
		rangeDiff = 0.0f;
		equippedAttack = AttackList.Instance.attackType [0];
		headPart = 0;
		armPart = 0;
		bodyPart = 0;
		legPart = 0;
		DataManager.Instance.Save ();
	}

	public void UpdatePartManager(){
		GameObject player = GameObject.Find("Character");
		if (player != null) {
			partManager = player.GetComponentInChildren<PartManager> ();
		}
	}

	public void FindPartManager() {
		GameObject player = GameObject.Find("Character");
		if (player != null) {
			partManager = player.GetComponentInChildren<PartManager> ();
		}
	}

	public float MaxSpeed(){
		return maxSpeed;
	}
}
