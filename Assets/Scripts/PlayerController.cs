﻿    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour {


    Animator animator;
    Vector3 velocity;
    Rigidbody myRigidBody;
	void Start () {
        myRigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
	}
	
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
        
    }

    public void FixedUpdate()
    {
        myRigidBody.MovePosition(myRigidBody.position + velocity * Time.fixedDeltaTime);
    }


    public void LookAt(Vector3 lookPoint)
    {
        Vector3 hcp = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(hcp);
    }
}
