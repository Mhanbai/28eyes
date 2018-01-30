﻿using System.Collections;
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
	[SerializeField] protected float maxHealth = 100.0f; //Percentage
	[SerializeField] protected float currentHealth = 100.0f; //Percentage
	[SerializeField] protected float maxSpeed = 0.25f; //Pixels per second
	[SerializeField] protected float minimumSpeed = 0.1f; 
	protected int attackStyle = 0; //Defined in CharController class

	public Item[] inventory = new Item[6];

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

	public int AttackStyle() {
		return attackStyle;
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