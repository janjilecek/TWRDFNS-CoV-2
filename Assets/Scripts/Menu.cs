using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GameObject start;

	// Use this for initialization
	public void Play()
    {
        SceneManager.LoadScene("main_scene");
    }
}
