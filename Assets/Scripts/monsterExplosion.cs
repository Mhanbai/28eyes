using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterExplosion : MonoBehaviour {
    float duration = 1.2f;
    float timer = 0.0f;
    public int damage;

	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > duration)
        {
            GameObject.Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInfo.Instance.Hit(damage);
        }
    }
}
