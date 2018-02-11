using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour {
	[SerializeField] protected GameObject monsterBase;
	[SerializeField] protected GameObject bottomLeftBoundary;
	[SerializeField] protected GameObject topRightBoundary;

	float time = 0.0f;

	void Start() {
		CreateMonster ();
	}

	void Update() {
		time += Time.deltaTime;
		if (time > 3.0f) {
			CreateMonster ();
			time = 0.0f;
		}
	}

	void CreateMonster() {
		GameObject toSpawn = GameObject.Instantiate(monsterBase, new Vector3(this.transform.position.x, 1.75f, this.transform.position.z), Quaternion.identity);
		toSpawn.GetComponent<MonsterClass> ().EstablishBoundaries(topRightBoundary.transform.position, bottomLeftBoundary.transform.position);
	}
}
