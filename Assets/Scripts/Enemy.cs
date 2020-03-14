using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity {

    NavMeshAgent pathfinder;
    Transform target;
    public GameObject deathEffect;
	// Use this for initialization
	public override void Start () {
        base.Start();
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("MainPlayer").transform;
        // alternative PlaneTickets

        StartCoroutine(updatePath());
	}
	
	// Update is called once per frame
	void Update () {
    
	}

    IEnumerator updatePath()
    {
        float refreshRate = 0.2f;
        while (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
            if (!dead) { pathfinder.SetDestination(targetPosition); }
            yield return new WaitForSeconds(refreshRate);
        }
    }

    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        Instantiate(deathEffect, hitPoint, Quaternion.FromToRotation(Vector3.forward, -hitDirection));
        base.TakeHit(damage, hitPoint, hitDirection);
    }
}
