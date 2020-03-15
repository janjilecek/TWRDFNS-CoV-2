using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    public Gun equippedGun;
    public Transform weaponHold;
    public Gun startingGun;

    public Canvas uiCanvas;
    void Start()
    {
        if (startingGun != null)
        {
            EquipGun(startingGun);
            
        }

        //uiCanvas = FindObjectOfType<Canvas>();

    }

    void Update()
    {
        uiCanvas.GetComponent<GameUI>().gunText.text = equippedGun.magazines.ToString() + "/" + equippedGun.bulletsInMagCurrent.ToString();
    }

	public void EquipGun(Gun gunToEquip)
    {
        if (equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
        }
        equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation) as Gun;
        equippedGun.transform.parent = weaponHold;
    }

    public void Shoot()
    {
        if (equippedGun != null)
        {
            equippedGun.Shoot();
            
            uiCanvas.GetComponent<GameUI>().gunText.text = equippedGun.magazines.ToString() + "/" + equippedGun.bulletsInMagCurrent.ToString(); 


        }
    }
}
