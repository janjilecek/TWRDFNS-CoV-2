using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity {

    NavMeshAgent pathfinder;
    Transform target;
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
}
