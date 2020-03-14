using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public LayerMask collisionMask;
    float speed = 30;
    float lifetime = 3;
    float damage = 20;
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    // Update is called once per frame
    void Update() {
        float moveDistance = speed * Time.deltaTime;
        CheckCollision(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }

    void CheckCollision(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            print("hit");
            OnHitObject(hit.collider, hit.point);
        }
    }

    void OnHitObject(Collider c, Vector3 hitpoint)
    {
        IDamagable damagableObject = c.GetComponent<IDamagable>();
        if (damagableObject != null)
        {
            damagableObject.TakeHit(damage, hitpoint, transform.forward);
        }
        GameObject.Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("collision");
    }



    void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
