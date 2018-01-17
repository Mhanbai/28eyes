using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createGround : MonoBehaviour {
	public GameObject groundTile;
	public Transform player;
	public int levelWidth;
	public int levelDepth;

	protected Vector3 startPos;
	protected int startXOffset;
	protected int startZOffset;

	protected const int SCALE = 10;


	// Use this for initialization
	void Start () {
		startXOffset = (levelWidth / 2) * SCALE;
		startZOffset = (levelDepth / 2) * SCALE;
		startPos = new Vector3 (player.position.x - startXOffset, 0, player.position.z - startZOffset);

		for (int i = 0; i < levelWidth; i++) {
			for (int j = 0; j < levelWidth; j++) {
				Vector3 pos = new Vector3 (startPos.x + (i * SCALE), groundTile.transform.position.y, startPos.z + (j * SCALE));
				GameObject newTile = GameObject.Instantiate (groundTile, pos, Quaternion.identity);
			}
		}


	}
}
