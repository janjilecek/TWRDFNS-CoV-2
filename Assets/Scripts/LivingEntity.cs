using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour,IDamagable {

    public float startingHealth;
    public float health;
    protected bool dead;
    public virtual void Start()
    {
        health = startingHealth;
        print(health);
    }

    public void TakeHit(float damage, RaycastHit hit)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    public void Die()
    {
        dead = true;
        GameObject.Destroy(gameObject);
    }


}
