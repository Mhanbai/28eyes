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

	[SerializeField] protected float maxHealth = 100.0f; //Percentage
	[SerializeField] protected float currentHealth = 100.0f; //Percentage
	[SerializeField] protected float speed = 0.25f; //Pixels per second
	[SerializeField] protected float minimumSpeed = 0.1f; 
	[SerializeField] protected float attackRate = 1.0f; //Projectiles per second
	[SerializeField] protected int attackStyle = 1; //Defined in CharController class

	//Functions for combat

	public void Hit(float damage) {
		currentHealth = currentHealth - damage;
	}

	public void Heal(float healing) {
		currentHealth = currentHealth + healing;

		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}
	}

	//Functions for pickups

	public void ChangeAttack(float atk_rate_in, int attackStyle_in) {
		attackRate = atk_rate_in;
		attackStyle = attackStyle_in;
	}

	public float Speed() {
		return speed;
	}

	public void SetSpeed(float speed_in) {
		speed = speed_in;

		if (speed < minimumSpeed) {
			speed = minimumSpeed;
		}
	}

	public float MaxHealth() {
		return maxHealth;
	}

	public void SetMaxHealth(float max_health_in) {
		maxHealth = max_health_in;

		if (maxHealth < 0.1f) {
			maxHealth = 0.1f;
		}

		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}
	}

	public float CurrentHealth() {
		return currentHealth;
	}
}
