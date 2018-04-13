using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticMonster : MonoBehaviour {
    CharController player;
    [SerializeField] protected float range = 25.0f;
    [SerializeField] protected float fireFrequency = 3.0f;
    [SerializeField] protected float missileSpeed = 3.0f;
    [SerializeField] protected int damage = 25;
    protected float fireTimer = 0.0f;
    [SerializeField] public GameObject firePoint;
    [SerializeField] public GameObject missile;

    protected bool flipped = false;
    protected Vector3 bounds;

    // Use this for initialization
    void Start () {
        bounds = gameObject.GetComponent<Collider>().bounds.size;
        player = GameObject.Find("Player").GetComponent<CharController>();
        transform.position = new Vector3(transform.position.x, bounds.y, transform.position.z);
        fireTimer = fireFrequency;
    }
	
	// Update is called once per frame
	void Update () {
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            flipped = false;
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            flipped = true;
        }
		if (Vector3.Distance(player.transform.position, transform.position) < range)
        {
            if (fireTimer >= fireFrequency)
            {
                fireTimer = 0.0f;
                Fire();
            } else
            {
                fireTimer += Time.deltaTime;
            }
        } 
	}

    void Fire()
    {
        GameObject missileContainer = GameObject.Instantiate(missile);
        EnemyProjectile missileController = missileContainer.GetComponent<EnemyProjectile>();

        missileContainer.transform.position = firePoint.transform.position;
        missileController.startPosition = firePoint.transform.position;
        missileController.target = player.transform.position;
        missileController.speed = missileSpeed;
        missileController.damage = damage;

        if (flipped)
        {
            missileContainer.transform.Rotate(new Vector3(25.0f, 0.0f, 180.0f));
        }
    }
}
