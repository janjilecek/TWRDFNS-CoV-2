using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity {

    float attackDistanceThresh = 1.0f;
    float timebttattacks = 1;
    public AudioClip hitAudio;
    float nextAttackTime;
    public enum State
    {
        attacking,
        chasing,
        idle
    }
    State currentState;

    NavMeshAgent pathfinder;
    Transform target;
    LivingEntity targetEntity;
    public GameObject deathEffect;
	// Use this for initialization
	public override void Start () {
        base.Start();
        currentState = State.chasing;
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("MainPlayer").transform;
        Random.seed = System.Environment.TickCount;
        targetEntity = target.GetComponent<LivingEntity>();
        hasNothingAmmoHp = Random.Range(0, 3); // random for hp ammo drops
        print(hasNothingAmmoHp);
        // alternative PlaneTickets

        StartCoroutine(updatePath());
	}

    public void SetSpeed(float speed)
    {
        pathfinder.speed = speed;
    }
	
	// Update is called once per frame
	void Update () {


        if (Time.time > nextAttackTime )
        {
            float sqrtDistanceTotarget = (target.position - transform.position).sqrMagnitude;

            if (sqrtDistanceTotarget < Mathf.Pow(attackDistanceThresh, 2))
            {
                nextAttackTime = Time.time + timebttattacks;
                StartCoroutine(Attack());
            }
        }
        
	}

    IEnumerator Attack()
    {
        currentState = State.attacking;
             
        pathfinder.enabled = false;
        Vector3 originalPosition = transform.position;
        Vector3 attackPosition = target.position;
        bool hasAppliedDamage = false;
        float attackSpeed = 3.0f;
        float percent = 0;
        while (percent <= 1)
        {
            if (percent >= .5f && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
                targetEntity.TakeDamage(20f);
            }


            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-percent * percent + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
            yield return null;
        }

        currentState = State.chasing;
        pathfinder.enabled = true;
        
    }

    IEnumerator updatePath()
    {
        float refreshRate = 0.2f;
        while (target != null)
        {
            if (currentState == State.chasing)
            {
                Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
                if (!dead) { pathfinder.SetDestination(targetPosition); }
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }

    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        Instantiate(deathEffect, hitPoint, Quaternion.FromToRotation(Vector3.forward, -hitDirection));
        print("taking a hit");
        AudioManager.instance.PlaySound(hitAudio, transform.position);

        base.TakeHit(damage, hitPoint, hitDirection);

    }

    public override void Die()
    {
        Transform transformIT = GetComponent<Transform>();


        if (hasNothingAmmoHp == 1)
        {
            Instantiate(ammoobj, transformIT.position, Quaternion.identity);
        } else if (hasNothingAmmoHp == 2) {
            Instantiate(hpobj, transformIT.position, Quaternion.identity);
        }

        //GameObject g = GameObject.FindGameObjectWithTag("MainPlayer");
        this.GetComponentInChildren<Animator>().SetInteger("DeathType_int", 2);
        this.GetComponentInChildren<Animator>().SetBool("Death_b", true);
        


        base.Die();




        //GameObject.Destroy(gameObject);
    }
}
