using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioClip mainTheme;
    
    public void Start( )
    {
        AudioManager.instance.PlayMusic(mainTheme, 2);
    }

    public void Update()
    {

    }
}
