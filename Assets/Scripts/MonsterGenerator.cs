﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour {
	public GameObject[]headParts;
	public GameObject[]bodyParts;
	public GameObject[]tailParts;
	public GameObject[]frontLegParts;
	public GameObject[]backLegParts;

	[SerializeField] protected GameObject monsterBase;
	protected Bounds combinedBounds;

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
		int head = Random.Range (0, headParts.Length);
		int body = Random.Range (0, bodyParts.Length);
		int tail = Random.Range (0, tailParts.Length);
		int legs = Random.Range (0, frontLegParts.Length);

		MonsterClass monster = monsterBase.GetComponent<MonsterClass> ();
		monster.UpdateParts (headParts [head], bodyParts [body], tailParts [tail], frontLegParts [legs], backLegParts [legs]);

		combinedBounds = monster.head.GetComponent<Anima2D.SpriteMeshInstance> ().spriteMesh.sprite.bounds;
		combinedBounds.Encapsulate (monster.body.GetComponent<Anima2D.SpriteMeshInstance> ().spriteMesh.sprite.bounds);
		combinedBounds.Encapsulate (monster.tail.GetComponent<Anima2D.SpriteMeshInstance> ().spriteMesh.sprite.bounds);
		combinedBounds.Encapsulate (monster.fLeg1.GetComponent<Anima2D.SpriteMeshInstance> ().spriteMesh.sprite.bounds);
		combinedBounds.Encapsulate (monster.fLeg2.GetComponent<Anima2D.SpriteMeshInstance> ().spriteMesh.sprite.bounds);
		combinedBounds.Encapsulate (monster.bLeg1.GetComponent<Anima2D.SpriteMeshInstance> ().spriteMesh.sprite.bounds);
		combinedBounds.Encapsulate (monster.bLeg2.GetComponent<Anima2D.SpriteMeshInstance> ().spriteMesh.sprite.bounds);

		GameObject.Instantiate(monsterBase, new Vector3(this.transform.position.x, combinedBounds.extents.y * 2, this.transform.position.z), Quaternion.identity);
		Debug.Log ("Bounds: " + combinedBounds);
	}
}
