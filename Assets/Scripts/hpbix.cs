using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpbix : MonoBehaviour {

    public int lifetime = 30;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Destroy(gameObject, lifetime); // self destructs after 30 sec
    }
}
