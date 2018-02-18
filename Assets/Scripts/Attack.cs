using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack {
	string name;
	float damage;
	float projectileSpeed;
	GameObject projectile;
	int noOfProjectiles;
	int uses;
	float reloadTime;
	bool bleed;
	bool poison;
	bool slow;
	float range;
	int trajectoryType;

	public Attack(string name_in, float damage_in, float projectileSpeed_in, float range_in, int trajectoryType_in, int noOfProjectiles_in, int uses_in, float reloadTime_in, GameObject projectile_in, bool bleed_in, bool poison_in, bool slow_in) {
		name = name_in;
		damage = damage_in;
		projectileSpeed	= projectileSpeed_in;
		projectile = projectile_in;
		noOfProjectiles = noOfProjectiles_in;
		uses = uses_in;
		reloadTime = reloadTime_in;
		bleed = bleed_in;
		poison = poison_in;
		slow = slow_in;
		range = range_in;
		trajectoryType = trajectoryType_in;
	}
		
	public int NoOfProjectiles { get { return noOfProjectiles; } }
	public int Uses	{ get { return uses; } }
	public float ReloadTime	{ get { return reloadTime; } }
	public float Damage	{ get { return damage; } }
	public bool Bleed { get { return bleed; } }
	public bool Poison { get { return poison; } }
	public bool Slow { get { return slow; } }
	public float ProjectileSpeed { get { return  projectileSpeed; } }
	public GameObject Projectile { get { return projectile; } }
	public float Range { get { return range; } }
	public int TrajectoryType { get { return trajectoryType; } }
}
