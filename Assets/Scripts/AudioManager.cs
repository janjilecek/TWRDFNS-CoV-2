using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    float masterVolumePercent = 1;
    float sfxVOlumePercent = 1;
    float musicVolumePrcnt = 1;

    Transform audioListener;
    Transform playerMine;

    AudioSource[] musicSources;
    int activeMusicSourceIndex;


    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
        musicSources = new AudioSource[2];
        for (int i = 0; i < 2; i++)
        {
            GameObject newMusicSource = new GameObject("Music source " + (i + 1));
            musicSources[i] =  newMusicSource.AddComponent<AudioSource>();
            newMusicSource.transform.parent = transform;
        }

        audioListener = FindObjectOfType<AudioListener>().transform;
        playerMine = FindObjectOfType<Player>().transform;

    }

    void Update()
    {
        if (playerMine != null)
        {
            audioListener.position = playerMine.position;
        }
    }

    public void PlayMusic(AudioClip clip, float fadeDuration = 1)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        musicSources[activeMusicSourceIndex].clip = clip;
        musicSources[activeMusicSourceIndex].Play();
        StartCoroutine(ANimateMusicCorssfade(fadeDuration));
    }

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos, sfxVOlumePercent * masterVolumePercent);
        // pass
    }

    IEnumerator ANimateMusicCorssfade(float duration)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePrcnt * masterVolumePercent, percent);
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(musicVolumePrcnt * masterVolumePercent, 0 , percent);

            yield return null;
        }
    }
}
