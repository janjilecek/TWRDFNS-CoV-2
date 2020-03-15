using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public Transform muzzle;
    public Projectile projectile;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;
    public GameObject weaponhold;
    public Transform shell;
    public Transform shellEjection;
    public MuzzleFlash muzzleflash;
    public Camera myCam;
    float nextShotTime;

    [Header("Reloading")]
    public int bulletsInMagCurrent;
    public int bulletsInMagMaximum;
    public int magazines;
    bool isReloading = false;
    public bool hasAmmo = true;

    public AudioClip shootAudio;
    public AudioClip reloadAudio;
    public AudioClip dryFIre;

    public Canvas uiCanvas;

    public Animator animator;

    public void Start()
    {
        bulletsInMagCurrent = 30;
        bulletsInMagMaximum = 30;
        magazines = 2;
        myCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        print(myCam);
        animator = GameObject.FindGameObjectWithTag("MainPlayer").GetComponentInChildren<Animator>();
    }


    public void Shoot()
    {
        


        if (Time.time > nextShotTime && !isReloading && hasAmmo)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile; // spawn bullet
            newProjectile.SetSpeed(muzzleVelocity); // set speed of the bullet

            Instantiate(shell, shellEjection.position, shellEjection.rotation); // create shell to eject
            muzzleflash.Activate();  // muzzle flash flash
            GameObject.FindGameObjectWithTag("MainPlayer").transform.localPosition -= Vector3.forward *.1f; // kickback player
            AudioManager.instance.PlaySound(shootAudio, transform.position);
            //cameraShake.Shake(0.15f, 0.4f));

            animator.SetInteger("WeaponType_int", 6);

            //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().ShakeCamera(10f, 10f);
            //myCam.GetComponent<CameraShake>().ShakeCamera(0.1f, 0.01f);
            myCam.GetComponent<CameraShake>().myShake();

            // reloading
            if (bulletsInMagCurrent > 0)
            {
                bulletsInMagCurrent--;
            } else
            {
                StartCoroutine(Reload()); // wait for 2 seconds
                if (magazines > 0)
                {
                    magazines--;
                    Reloading();
                    bulletsInMagCurrent = bulletsInMagMaximum;
                } else {
                    AudioManager.instance.PlaySound(dryFIre, transform.position);
                    hasAmmo = false; // doint forget to change it after AMMO pickup
                }

                
            }
            

        }

        
    }


    public void Reloading()
    {
        AudioManager.instance.PlaySound(reloadAudio, transform.position);
        StartCoroutine(Reload());
    }

    public IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(2);

        isReloading = false;
        
    }


}
