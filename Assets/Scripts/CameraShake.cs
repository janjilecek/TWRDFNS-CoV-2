﻿using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

    Vector3 originalCameraPosition;

    float shakeAmt = 0;

    public Camera mainCamera;


    public void myShake()
    {
        float quakeAmt = Random.value * shakeAmt * 2 - shakeAmt;
        Vector3 pp = mainCamera.transform.position;
        pp.y += quakeAmt; // can also add to x and/or z
        mainCamera.transform.position = pp;
    }


    public void Shake()
    {

        shakeAmt = 5 * .0025f;
        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.3f);

    }

    void CameraShaker()
    {
        if (shakeAmt > 0)
        {
            float quakeAmt = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.y += quakeAmt; // can also add to x and/or z
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        mainCamera.transform.position = originalCameraPosition;
    }

}