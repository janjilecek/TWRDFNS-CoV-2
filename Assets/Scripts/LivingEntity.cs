using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour,IDamagable {

    public float startingHealth;
    public float health;
    protected bool dead;
    public int hasNothingAmmoHp;
    public event System.Action OnDeath;

    public GameObject ammoobj;
    public GameObject hpobj;

    public virtual void Start()
    {
        health = startingHealth;
        
    }

    public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        dead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        GameObject.Destroy(gameObject);
    }


}
