using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {

    public Rigidbody mRigidBody;
    public float forceMin;
    public float forceMax;
    float lifetime = 30;
    float fadetime = 2;
	// Use this for initialization
	void Start () {
        float force = Random.Range(forceMin, forceMax);
        mRigidBody.AddForce(transform.right * force);
        mRigidBody.AddTorque(Random.insideUnitSphere * force);
        StartCoroutine(Fade());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(lifetime);

        float percent = 0;
        float fadeSpeed = 1 / fadetime;
        Destroy(gameObject);
    }
}
