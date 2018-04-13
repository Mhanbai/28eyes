using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {
    public Vector3 target;
    public Vector3 startPosition;
    public GameObject explosive;
    public int damage;
    public float speed;
    protected float movement;
    protected bool collided = false;
	
	// Update is called once per frame
	void Update () {
        if ((collided) || (transform.position == target))
        {
            Explode(transform.position.x, transform.position.z);
        }
        else
        {
            movement += Time.deltaTime * speed;
            transform.position = Vector3.MoveTowards(startPosition, target, movement);
        }
	}

    void Explode(float xPos, float zPos)
    {
        GameObject exp = GameObject.Instantiate(explosive);
        exp.transform.position = new Vector3(xPos, exp.GetComponent<Collider>().bounds.size.y / 2, zPos);
        exp.GetComponent<monsterExplosion>().damage = damage;
        GameObject.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        collided = true;
    }
}
