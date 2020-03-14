using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour {
    public GameObject flashHolder;
    public float flashTime;
    public Light light;
    void Start()
    {
        light = gameObject.GetComponent<Light>();
        Deactivate();
    }

    public void Activate()
    {
        flashHolder.SetActive(true);
        light.gameObject.SetActive(true);

        Invoke("Deactivate", flashTime);
    }

    void Deactivate()
    {
        light.gameObject.SetActive(false);
        flashHolder.SetActive(false);
    }
}
