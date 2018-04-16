using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;

public class Monster : MonoBehaviour {
	CharController player;
	//Max speed the monster will move at
	[SerializeField] protected float maxSpeed;

	//Variables to determine wantering behaviour
	[SerializeField] protected float wanderRadius = 20.0f;
	[SerializeField] protected float wanderDistance = 5.0f;
	[SerializeField] protected float wanderVariance = 10.0f;
	[SerializeField] protected float pursueDistance = 25.0f;
	protected float wanderTimer;
	protected Vector3 wanderTarget;

	//Attack settings
	[SerializeField] protected bool shouldMoveWhileAttacking = false;
	[SerializeField] protected float attackRange;
    [SerializeField] protected int attackStrength = 10;
	[SerializeField] float attackLength = 0.5f;
	protected float attackTimer = 0.0f;
	[SerializeField] float delayBetweenAttacks = 1.0f;
	protected float attackDelayTimer = 0.0f;
    protected bool isAttacking = false;

	//How close the monster will come to an obstacle before avoiding it
	[SerializeField] protected float avoidDistance;
	protected List<Collider> obstacleList = new List<Collider>();

	protected Vector3 velocityVector;
	protected Vector3 rightVector;
	protected Vector3 forwardVector;
	protected Vector3 steeringForce;
	protected Vector3 avoidanceForce;

    [SerializeField] protected float health = 100.0f;
    [SerializeField] protected float bleedDamage = 0.5f;
    [SerializeField] protected float bleedTime = 5.0f;
    [SerializeField] protected float poisonDamage = 10.0f;
    [SerializeField] protected float poisonFrequency = 5.0f;
    [SerializeField] protected int poisonHits = 5;
    [SerializeField] protected float slowTime = 5.0f;
    [SerializeField] protected float slowSeverity = 2.0f;
    [SerializeField] protected float deathTimer = 2.0f;
    [SerializeField] protected int dropChance = 99;
    [SerializeField] protected int lootList = 1;

    protected float deathCounter = 0.0f;
    protected float bleedCounter = 0.0f;
    protected float poisonCounter = 0.0f;
    float slowCounter = 0.0f;
    protected int poisonTracker = 0;
    protected float trappedCounter = 0.0f;
    protected float escapeCounter = 0.0f;

    protected bool bleeding = false;
    protected bool poisoned = false;
    protected bool slowed = false;
    protected bool trapped = false;

    float yPos;
    SpriteRenderer mySprite;

    // Use this for initialization
    void Start () {
		player = GameObject.Find ("Player").GetComponent<CharController> ();
		yPos = gameObject.GetComponent<Collider>().bounds.size.y / 2;
		attackRange = GameObject.Find ("Player").GetComponent<Collider> ().bounds.size.x * 3;
        attackDelayTimer = delayBetweenAttacks;
		wanderTimer = wanderVariance;
        mySprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        //Reset velocity vector
        velocityVector = new Vector3(0.0f, 0.0f, 0.0f);

        if (health > 0.0f)
        {
            if (!isAttacking)
            {
                //Find array of obstacles and create line between two furthest collision points
                if (trapped == false)
                {
                    if (obstacleList.Count > 1)
                    {
                        trappedCounter += Time.deltaTime;
                        if (trappedCounter > 2.0f)
                        {
                            trapped = true;
                        }

                        Vector3 avoidanceForce;
                        Vector3 pointOne = FindFurthest(transform.position);
                        Vector3 pointTwo = FindFurthest(pointOne);

                        if (Vector3.Angle(forwardVector, pointOne - pointTwo) > Vector3.Angle(forwardVector, pointTwo - pointOne))
                        {
                            avoidanceForce = pointTwo - pointOne;
                        }
                        else
                        {
                            avoidanceForce = pointOne - pointTwo;
                        }

                        Vector3.Normalize(avoidanceForce);
                        wanderTimer = wanderVariance;
                        velocityVector = new Vector3(0.0f, 0.0f, 0.0f);
                        velocityVector += avoidanceForce * Time.deltaTime;
                    }
                    else if (obstacleList.Count == 1)
                    {
                        Vector3 between = obstacleList[0].transform.position - transform.position;
                        between = new Vector3(between.z, 0.0f, -between.x);
                        Vector3.Normalize(between);

                        if (Vector3.Angle(forwardVector, between) > Vector3.Angle(forwardVector, -between))
                        {
                            avoidanceForce = -between;
                        }
                        else
                        {
                            avoidanceForce = between;
                        }

                        wanderTimer = wanderVariance;

                        velocityVector += avoidanceForce * Time.deltaTime;
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, player.transform.position) < pursueDistance)
                        {
                            if (!(Vector3.Distance(transform.position, player.transform.position) < attackRange))
                            {
                                attackDelayTimer = delayBetweenAttacks;
                                velocityVector += Pursue(player);
                            }
                            else
                            {
                                isAttacking = true;
                            }

                        }
                        else
                        {
                            velocityVector += Seek(Wander());
                        }
                    }
                } else
                { 
                    if (obstacleList.Count > 0)
                    {
                        escapeCounter += Time.deltaTime;
                        Vector3 pointOne = FindFurthest(transform.position);
                        Vector3 pointTwo = FindFurthest(pointOne);

                        Vector3 escape = pointOne - pointTwo;
                        escape = new Vector3(escape.z, 0.0f, -escape.x);

                        Vector3.Normalize(escape);

                        if (escapeCounter < 2.0f)
                        {
                            velocityVector += escape * Time.deltaTime * 0.5f;
                        } else
                        {
                            velocityVector -= escape * Time.deltaTime * 0.5f;
                        }
                    }
                    else
                    {
                        trapped = false;
                        trappedCounter = 0.0f;
                        escapeCounter = 0.0f;
                    }
                }
            }

            if ((Vector3.Distance(transform.position, player.transform.position) < attackRange))
            {
                isAttacking = true;
            }

            if (isAttacking)
            {
                if (shouldMoveWhileAttacking)
                {
                    velocityVector += Pursue(player);
                }
                attackDelayTimer += Time.deltaTime;
                if (attackDelayTimer > delayBetweenAttacks)
                {
                    if (!SoundManager.Instance.Monsters.isPlaying) { 
                        SoundManager.Instance.Monsters.PlayOneShot(SoundManager.Instance.hunterAttack);
                    }
                    attackTimer += Time.deltaTime;
                    if (attackTimer > attackLength)
                    {
                        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
                        {
                            PlayerInfo.Instance.Hit(attackStrength);
                        }
                        attackTimer = 0.0f;
                        attackDelayTimer = 0.0f;
                        isAttacking = false;
                    }
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

            if (slowed == true)
            {
                slowCounter = slowCounter + Time.deltaTime;
            }

            if (slowCounter > slowTime)
            {
                slowCounter = 0.0f;
                slowed = false;
                maxSpeed = maxSpeed * slowSeverity;
            }

            //Add velocity vector to position
            forwardVector = velocityVector.normalized;
            transform.position += velocityVector;

            //Flip depencing on direction
            if (velocityVector.x < -0.1f)
            {
                mySprite.flipX = false;
            }
            else if (velocityVector.x > 0.1f)
            {
                mySprite.flipX = true;
            }
        }
        else
        {
            velocityVector -= (velocityVector * 0.1f);
            mySprite.flipY = true;
            deathCounter += Time.deltaTime;

            if (!SoundManager.Instance.Monsters.isPlaying)
            {
                SoundManager.Instance.Monsters.PlayOneShot(SoundManager.Instance.hunterDie);
            }

            if (deathCounter > deathTimer)
            {
                if (Random.Range(0, 100) < dropChance)
                {
                    int drop = Random.Range(0, ItemList.Instance.LevelItems(lootList).Length);
                    int itemToDrop = drop;
                    GameObject droppedItem = GameObject.Instantiate(ItemList.Instance.pickup, transform.position, Quaternion.identity);
                    droppedItem.GetComponent<Pickup>().AssignItem(ItemList.Instance.LevelItems(lootList)[itemToDrop]); //Change depending on level
                }

                GameObject.Destroy(gameObject);
            }
        }

        //Ensure monster is always touching the ground
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
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

        if (explosion.causesSlow == true)
        {
            SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.slow);
            Slow();
        }
    }
    public void TakeExplosion(monsterExplosion explosion)
    {
        health = health - explosion.damage;
    }


    public void TakeHit(ProjectileBehaviour hit)
    {
        SoundManager.Instance.Monsters.PlayOneShot(SoundManager.Instance.hunterTakeHit);
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

        if (hit.causesSlow == true)
        {
            SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.slow);
            Slow();
        }
    }

    public void TakeHit(EnemyProjectile hit)
    {
        SoundManager.Instance.Monsters.PlayOneShot(SoundManager.Instance.hunterTakeHit);
        health = health - hit.damage;
    }

    public void Slow()
    {
        if (slowed != true)
        {
            maxSpeed = maxSpeed / slowSeverity;
            slowed = true;
        }
        else
        {
            slowCounter = 0.0f;
        }
    }

    public Vector3 Seek(Vector3 targetPos) {
		if (Vector3.Distance (targetPos, transform.position) < 0.2f) {
			return new Vector3 (0.0f, 0.0f, 0.0f);
		} else {
			return (Vector3.Normalize (targetPos - transform.position) * maxSpeed) - velocityVector;
		}
	}

	public Vector3 Avoid(Vector3 targetPos) {
		return (Vector3.Normalize (transform.position - targetPos) * maxSpeed) - velocityVector;
	}

	public Vector3 Pursue(CharController target) {
		Vector3 distanceToTarget = target.transform.position - transform.position;
		float relativeHeading = Vector3.Dot (forwardVector, target.GetForwardVector());

		//If difference in direction is less that 18 degrees, simply head towards the target
		if ((Vector3.Dot (forwardVector, distanceToTarget) > 0) && (relativeHeading > 0.9)) {
			return Seek (target.transform.position);
		} else {
			//Calculate seconds ahead to predict
			float predictionTime = distanceToTarget.magnitude / (maxSpeed + PlayerInfo.Instance.MaxSpeed());
			//Head towards targets current heading
			return Seek (target.transform.position + target.GetVelocityVector() * predictionTime);
		}
	}

	public Vector3 Wander() {
		if (wanderTimer < wanderVariance) {
			wanderTimer += Time.deltaTime;
		} else {
			Vector2 movement = Random.insideUnitCircle * wanderRadius;
			wanderTarget = transform.position + (new Vector3 (movement.x, 0.0f, movement.y) * wanderDistance);
			wanderTimer = 0.0f;
		}

		return wanderTarget;
	}

	void OnTriggerEnter(Collider obstacle) {
        if (
            (!obstacle.CompareTag("Player"))
            && (!obstacle.CompareTag("Enemy")) 
            && (!obstacle.CompareTag("explosion")) 
            && (!obstacle.CompareTag("projectile")) 
            && (!obstacle.CompareTag("ground"))
            )
        {
            obstacleList.Add (obstacle);
		}

        if (obstacle.CompareTag("projectile"))
        {
            try
            {
                ProjectileBehaviour projectile = obstacle.transform.GetComponent<ProjectileBehaviour>();
                TakeHit(projectile);
                projectile.Die();
            } catch
            {
                EnemyProjectile projectile = obstacle.transform.GetComponent<EnemyProjectile>();
                projectile.collided = true;
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

    void OnTriggerExit(Collider obstacle) {
        if ((!obstacle.CompareTag("Player")) && (!obstacle.CompareTag("Enemy")) && (!obstacle.CompareTag("explosion")) && (!obstacle.CompareTag("projectile")) && (!obstacle.CompareTag("ground")))
        {
            obstacleList.Remove(obstacle);
        }
	}

	Vector3 FindFurthest(Vector3 furthestFrom) {
		Vector3 furthestObstacle = furthestFrom;
		float distance = 0.0f;
		foreach (Collider obs in obstacleList) {
			float compareDist = Vector3.Distance (furthestFrom, obs.transform.position);
			if (compareDist > distance) {
				distance = compareDist;
				furthestObstacle = obs.transform.position;
			}
		}

		return furthestObstacle;
	}

	Vector3 FindClosest(Vector3 pointOne, Vector3 pointTwo) {
		Vector3 closestPoint = pointOne;
		float distance = Vector3.Distance (transform.position, pointOne);
		float compareDist = Vector3.Distance (transform.position, pointTwo);
		if (compareDist < distance) {
			closestPoint = pointTwo;
		}

		return closestPoint;
	}

    bool isLeft(Vector3 pointOne, Vector3 pointTwo, Vector3 queryPoint)
    {
        return ((pointTwo.x - pointOne.x) * (queryPoint.z - pointOne.z) - (pointTwo.z - pointOne.z) * (queryPoint.x - pointOne.x)) < 0;
    }
}