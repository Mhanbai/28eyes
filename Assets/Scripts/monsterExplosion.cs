using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterExplosion : MonoBehaviour {
    float duration = 1.2f;
    float colliderDuration = 0.6f;
    float timer = 0.0f;
    public int damage;
    protected bool isColliderEnabled = true;

	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > colliderDuration)
        {
            isColliderEnabled = false;
        }
        if (timer > duration)
        {
            GameObject.Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (isColliderEnabled)
        {
            if (other.CompareTag("Player"))
            {
                PlayerInfo.Instance.Hit(damage);
            }
        }
    }
}
