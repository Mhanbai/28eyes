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

    [SerializeField] protected float health = 100.0f;
    [SerializeField] protected float bleedDamage = 0.5f;
    [SerializeField] protected float bleedTime = 5.0f;
    [SerializeField] protected float poisonDamage = 10.0f;
    [SerializeField] protected float poisonFrequency = 5.0f;
    [SerializeField] protected int poisonHits = 5;
    [SerializeField] protected float deathTimer = 2.0f;
    [SerializeField] protected int dropChance = 99;
    [SerializeField] protected int lootList = 1;

    protected float deathCounter = 0.0f;
    protected float bleedCounter = 0.0f;
    protected float poisonCounter = 0.0f;
    protected int poisonTracker = 0;

    protected bool bleeding = false;
    protected bool poisoned = false;

    protected bool flipped = false;
    protected Vector3 bounds;

    // Use this for initialization
    void Start () {
        bounds = gameObject.GetComponent<Collider>().bounds.size;
        player = GameObject.Find("Player").GetComponent<CharController>();
        transform.position = new Vector3(transform.position.x, 10.2f, transform.position.z);
        fireTimer = fireFrequency;
    }
	
	// Update is called once per frame
	void Update () {
        if (health > 0.0f)
        {
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
                }
                else
                {
                    fireTimer += Time.deltaTime;
                }
            }

            if (bleeding == true)
            {
                bleedCounter += Time.deltaTime;
                health -= bleedDamage;

                if (bleedCounter > bleedTime)
                {
                    bleeding = false;
                }
            }

            if (poisoned == true)
            {
                poisonCounter += Time.deltaTime;

                if (poisonCounter > poisonFrequency)
                {
                    poisonCounter = 0.0f;
                    health = health - poisonDamage;
                    poisonTracker++;
                }

                if (poisonTracker > poisonHits)
                {
                    poisonTracker = 0;
                    poisoned = false;
                }
            }
        } else
        {
            if (!SoundManager.Instance.Monsters.isPlaying)
            {
                SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.guardianDie);
            }
            deathCounter += Time.deltaTime;

            if (deathCounter > deathTimer)
            {
                if (Random.Range(0, 100) < dropChance)
                {
                    int itemToDrop = Random.Range(0, ItemList.Instance.LevelItems(lootList).Length);
                    GameObject droppedItem = GameObject.Instantiate(ItemList.Instance.pickup, transform.position, Quaternion.identity);
                    droppedItem.GetComponent<Pickup>().AssignItem(ItemList.Instance.LevelItems(lootList)[itemToDrop]); //Change depending on level
                }

                GameObject.Destroy(gameObject);
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

        SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.guardianAttack);

        if (flipped)
        {
            missileContainer.transform.Rotate(new Vector3(25.0f, 0.0f, 180.0f));
        }
    }

    public void TakeExplosion(Explosion explosion)
    {
        health = health - explosion.damage;

        if ((explosion.causesPosion == true) && (poisoned == false))
        {
            SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.poison);
            poisoned = true;
        }
        else if ((explosion.causesPosion == true) && (poisoned == true))
        {
            SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.poison);
            poisonCounter = 0.0f;
        }

        if ((explosion.causesBleed == true) && (bleeding == false))
        {
            SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.bleed);
            bleeding = true;
        }
        else if ((explosion.causesBleed == true) && (bleeding == true))
        {
            SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.bleed);
            bleedCounter = 0.0f;
        }
    }
    public void TakeExplosion(monsterExplosion explosion)
    {
        health = health - explosion.damage;
    }


    public void TakeHit(ProjectileBehaviour hit)
    {
        SoundManager.Instance.Monsters.PlayOneShot(SoundManager.Instance.guardianTakeHit);

        health = health - hit.damage;

        if ((hit.causesPosion == true) && (poisoned == false))
        {
            SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.poison);
            poisoned = true;
        }
        else if ((hit.causesPosion == true) && (poisoned == true))
        {
            SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.poison);
            poisonCounter = 0.0f;
        }

        if ((hit.causesBleed == true) && (bleeding == false))
        {
            SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.bleed);
            bleeding = true;
        }
        else if ((hit.causesBleed == true) && (bleeding == true))
        {
            SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.bleed);
            bleedCounter = 0.0f;
        }
    }

    void OnTriggerEnter(Collider obstacle)
    {
        if (obstacle.CompareTag("projectile"))
        {
            try
            {
                ProjectileBehaviour projectile = obstacle.transform.GetComponent<ProjectileBehaviour>();
                TakeHit(projectile);
                projectile.Die();
            }
            catch
            {
            }
        }

        if (obstacle.CompareTag("explosion"))
        {
            try
            {
                Explosion explosion = obstacle.transform.GetComponentInParent<Explosion>();
                TakeExplosion(explosion);
            }
            catch
            {
                monsterExplosion explosion = obstacle.transform.GetComponentInParent<monsterExplosion>();
                TakeExplosion(explosion);
            }
        }
    }
}
