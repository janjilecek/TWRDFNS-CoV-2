using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity {

    float attackDistanceThresh = 1.0f;
    float timebttattacks = 1;
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
        targetEntity = target.GetComponent<LivingEntity>();
        // alternative PlaneTickets

        StartCoroutine(updatePath());
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
        base.TakeHit(damage, hitPoint, hitDirection);
    }
}
