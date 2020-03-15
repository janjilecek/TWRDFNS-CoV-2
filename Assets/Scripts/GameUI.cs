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
    }

    IEnumerator AnimateNewWave()
    {
        newWaveUI.SetActive(true);
        yield return new WaitForSeconds(4);
        newWaveUI.SetActive(false);
    }

    void onGameOver()
    {
        gameOverUI.SetActive(true);
        if (Input.GetKeyDown(KeyCode.R))
        {
            print("anty key");
            startNewGame();
        }
    }

    public void startNewGame()
    {
        //Application.LoadLevel("main_scene");
        SceneManager.LoadScene("main_scene");
    }
}
