using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack {
	string name;
	float damage;
	float projectileSpeed;
	Sprite projectileStyle;
	int noOfProjectiles;
	int uses;
	float reloadTime;
	bool bleed;
	bool poison;
	bool slow;

	public Attack(string name_in, float damage_in, float projectileSpeed_in, int noOfProjectiles_in, int uses_in, float reloadTime_in, Sprite style_in, bool bleed_in, bool poison_in, bool slow_in) {
		name = name_in;
		damage = damage_in;
		projectileSpeed	= projectileSpeed_in;
		projectileStyle = style_in;
		noOfProjectiles = noOfProjectiles_in;
		uses = uses_in;
		reloadTime = reloadTime_in;
		bleed = bleed_in;
		poison = poison_in;
		slow = slow_in;
	}

	public float Damage	{ get; }
	public float ProjectileSpeed { get; }
	public Sprite ProjectileStyle { get; }
	public int NoOfProjectiles { get; }
	public int Uses	{ get; }
	public float ReloadTime	{ get; }
	public bool Bleed { get; }
	public bool Poison { get; }
	public bool Slow { get; }
}
