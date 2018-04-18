using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour {
	[SerializeField] protected GameObject[] monsters;
    [SerializeField] protected float spawnTime = 10.0f;
    [SerializeField] protected int lootList = 1;
	float time = 0.0f;

	void Start() {
		CreateMonster ();
	}

	void Update() {
		time += Time.deltaTime;
		if (time > spawnTime) {
			CreateMonster ();
			time = 0.0f;
		}
	}

	void CreateMonster() {
		int selection = Random.Range (0, monsters.Length);
		GameObject toSpawn = GameObject.Instantiate(monsters[selection], new Vector3(this.transform.position.x, 1.75f, this.transform.position.z), Quaternion.identity);
        toSpawn.GetComponent<Monster>().lootList = lootList;
	}
}
