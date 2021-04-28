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

    public AudioSource forestOutofCombat;
    public AudioSource forestInCombat;

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
            needToSwitchAudio = gameManager.outOfCombat;
            switch(currentArea)
            {
                case 1:
                    if(gameManager.outOfCombat)
                    {
                        StartCoroutine(StartFade(caveInCombat, 1f, 0));
                        StartCoroutine(StartFade(caveOutofCombat, 2f, 1));
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
                        StartCoroutine(StartFade(mountainInCombat, 1f, 0));
                        StartCoroutine(StartFade(mountainOutofCombat, 2f, 1));
                    }
                    else
                    {
                        StartCoroutine(StartFade(mountainOutofCombat, 1f, 0));
                        StartCoroutine(StartFade(mountainInCombat, 1f, 1));
                    }

                    break;
                case 3:
                    if (gameManager.outOfCombat)
                    {
                        StartCoroutine(StartFade(forestInCombat, 1f, 0));
                        StartCoroutine(StartFade(forestOutofCombat, 2f, 1));
                    }
                    else
                    {
                        StartCoroutine(StartFade(forestOutofCombat, 1f, 0));
                        StartCoroutine(StartFade(forestInCombat, 1f, 1));
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
                StartCoroutine(StartFadeStop(caveOutofCombat, 2f, 0));
                StartCoroutine(StartFadeStop(caveInCombat, 2f, 0));


                mountainOutofCombat.Play();
                mountainInCombat.Play();
                StartCoroutine(StartFade(mountainOutofCombat, 2f, 1));
                
                break;
            case 3:
                StartCoroutine(StartFadeStop(mountainOutofCombat, 2f, 0));
                StartCoroutine(StartFadeStop(mountainInCombat, 2f, 0));


                forestOutofCombat.Play();
                forestInCombat.Play();
                StartCoroutine(StartFade(forestOutofCombat, 2f, 1));

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
    public static IEnumerator StartFadeStop(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        audioSource.Stop();
        yield break;
    }

}
