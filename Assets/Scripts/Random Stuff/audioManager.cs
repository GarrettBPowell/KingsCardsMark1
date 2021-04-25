using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class audioManager : MonoBehaviour
{
    public AudioSource caveOutofCombat;
    public AudioSource caveInCombat;

    public AudioSource mountainOutofCombat;
    public AudioSource mountainInCombat;

    public int currentArea;
    GameManager gameManager;

    public bool needToSwitchAudio = true;
    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        currentArea = gameManager.area;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Level Screen"))
        {
            if(gameManager.area != currentArea)
            {
                currentArea = gameManager.area;
                switchAudio();
            }
        }


        if(gameManager.outOfCombat != needToSwitchAudio)
        {
            Debug.Log("bool worked");
            needToSwitchAudio = gameManager.outOfCombat;
            switch(currentArea)
            {
                case 1:
                    if(gameManager.outOfCombat)
                    {
                        StartCoroutine(StartFade(caveInCombat, 1f, 0));
                        StartCoroutine(StartFade(caveOutofCombat, 1f, 1));
                    }
                    else
                    {
                        StartCoroutine(StartFade(caveOutofCombat, 1f, 0));
                        StartCoroutine(StartFade(caveInCombat, 1f, 1));
                    }

                    break;

                case 2:
                    if (gameManager.outOfCombat)
                    {
                        StartFade(mountainInCombat, 1f, 0);
                        StartFade(mountainOutofCombat, 1f, 1);
                    }
                    else
                    {
                        StartFade(mountainOutofCombat, 1f, 0);
                        StartFade(mountainInCombat, 1f, 1);
                    }

                    break;
            }
        }
    }


    public void switchAudio()
    {
        switch(currentArea)
        {
            case 2:
                caveOutofCombat.Stop();
                caveInCombat.Stop();

                mountainOutofCombat.Play();
                mountainOutofCombat.mute = false;
                mountainInCombat.mute = true;
                mountainInCombat.Play();
                break;
        }
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

}
