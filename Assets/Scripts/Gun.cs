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
    float nextShotTime;

    [Header("Reloading")]
    public int bulletsInMagCurrent;
    public int bulletsInMagMaximum;
    public int magazines;
    bool isReloading = false;
    bool hasAmmo = true;

    public AudioClip shootAudio;
    public AudioClip reloadAudio;

    public Canvas uiCanvas;

    public void Start()
    {
        bulletsInMagCurrent = 30;
        bulletsInMagMaximum = 30;
        magazines = 2;

        
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

                    hasAmmo = false; // doint forget to change it after AMMO pickup
                }

                
            }
            

        }

        
    }


    public void Reloading()
    {
        StartCoroutine(Reload());
    }

    public IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(2);

        isReloading = false;
        
    }


}
