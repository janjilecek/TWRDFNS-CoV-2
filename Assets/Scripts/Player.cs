using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GunController))]
[RequireComponent (typeof (PlayerController))]
public class Player : LivingEntity {

    Camera viewCamera;
    public float moveSpeed = 5;
    public Transform crosshairs;
    PlayerController controller;
    GunController gunController;
    public Text hp;

	// Use this for initialization
	public override void Start () {
        base.Start();
        controller = GetComponent<PlayerController>();
        viewCamera = Camera.main;
        gunController = GetComponent<GunController>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        hp.text = health.ToString();

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            controller.LookAt(point);
            crosshairs.position = point;
        }

        // gun controls
        if (Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
		
	}


    void CheckCollision(float moveDistance)
    {
        print("check col");
    }

    void OnHitObject(Collider c, Vector3 hitpoint)
    {
        print("hit obj");   
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("collision");

        if (collision.gameObject.tag == "ammo")
        {
            print("ammo");
            Destroy(collision.gameObject);
            gunController.equippedGun.magazines += 3;
        }

        if (collision.gameObject.tag == "hp")
        {
            Destroy(collision.gameObject);
            health = 100;
        }
    }
}
