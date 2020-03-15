using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public GameObject gameOverUI;
    public Text newWaveTitle;
    public GameObject newWaveUI;
    public Text gunText;
    public Text hpText;
    public GameObject won;
    bool gameover = false;


    public AudioClip dedsound;
    public AudioClip wonsound;
    public AudioClip wavesound;

    Spawner spawner;
	// Use this for initialization
	void Start () {
        FindObjectOfType<Player>().OnDeath += onGameOver;
        
	}

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        spawner.OnNewWave += OnNewWave;
    }
    

    void OnNewWave(int waveNumber)
    {
        string[] numbers = { "I.", "II.", "III.", "IV.", "V.", "VI.", "VII.", "VIII.", "IX.", "X." };
        newWaveTitle.text = numbers[waveNumber - 1] + " WAVE INCOMING";
        StartCoroutine(AnimateNewWave());
        //AudioManager.instance.PlayMusic(wavesound, 2);
        //AudioManager.instance.PlaySound(wavesound, transform.position);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(wavesound, 0.2f);
    }

    IEnumerator AnimateNewWave()
    {
        newWaveUI.SetActive(true);
        yield return new WaitForSeconds(4);
        newWaveUI.SetActive(false);
    }

    void onGameOver()
    {
        gameover = true;
        gameOverUI.SetActive(true);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(wonsound, 0.4f);

    }

    public void startNewGame()
    {
        //Application.LoadLevel("main_scene");
        SceneManager.LoadScene("main_scene");
    }


    private void Update()
    {
        if (gameover)
        {
            if (Input.anyKey)
            {
                print("anty key");
                //startNewGame();
            }
        }
        
    }
}
