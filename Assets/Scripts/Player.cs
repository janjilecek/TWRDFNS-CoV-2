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

    public AudioClip ammoPickup;
    public AudioClip hpPickup;
    public AudioClip dieSound;

    public Animator animator;

    // Use this for initialization
    public override void Start () {
        base.Start();
        controller = GetComponent<PlayerController>();
        viewCamera = Camera.main;
        gunController = GetComponent<GunController>();

        animator = GetComponentInChildren<Animator>();


        //animator.SetInteger("WeaponType_int", 6);
        //animator.SetBool("Shoot_b", false);


    }
	
	// Update is called once per frame
	void Update () {
        

        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        if (moveVelocity != Vector3.zero)
        {
            print("moving");
            //animator.SetFloat("Speed_f", 1f);
            animator.SetBool("walking", true);

        } else {
            print("not moving");
            animator.SetBool("walking", false);
            //animator.SetFloat("Speed_f", 0f);
        } 
        

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

        if (collision.gameObject.tag == "ammo") // pick up ammo boost
        {
            print("ammo");
            Destroy(collision.gameObject);
            gunController.equippedGun.magazines += 3;
            gunController.equippedGun.hasAmmo = true;
            AudioManager.instance.PlaySound(ammoPickup, transform.position);
        }

        if (collision.gameObject.tag == "hp") // pick up hp boost
        {
            if (health < 100)
            {
                Destroy(collision.gameObject);
                AudioManager.instance.PlaySound(hpPickup, transform.position);
                health = 100;
            }
            
        }
    }


    public override void Die()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(dieSound, 0.4f);
       
        

        GameObject g = GameObject.FindGameObjectWithTag("MainPlayer");
        g.GetComponentInChildren<Animator>().SetInteger("DeathType_int", 2);
        g.GetComponentInChildren<Animator>().SetBool("Death_b", true);
        gunController.equippedGun = null;
        //g.GetComponentInChildren<Animator>().SetFloat("Speed_f", 0.3f);
        //g.GetComponentInChildren<Animator>().SetBool("Static_b", true);

        base.Die();
    }
}
