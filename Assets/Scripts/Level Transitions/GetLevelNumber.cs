using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetLevelNumber : MonoBehaviour
{
    GameManager gameManager;
    Canvas canvas;
    public Text levelNumber;
    private string levelToLoad;
    void Start()
    {
        //pull any canvas from scene and find the one that is the player movement/attack/main game ui
        foreach(Canvas can in Canvas.FindObjectsOfType<Canvas>())
        {
            if (can.CompareTag("playerUICanvas"))
                canvas = can;
        }

        //get the game manager
        gameManager = GameObject.FindObjectOfType<GameManager>();

        if (SceneManager.GetActiveScene().name.Equals("Level Screen"))
            canvas.gameObject.SetActive(false);


        if(gameManager.area == 1)
        {
            if(gameManager.level == 1 || gameManager.level == 2 || gameManager.level == 3)
            {
                gameManager.level++;
                levelToLoad = "Caves " + gameManager.level;
            }
            else
            {
                gameManager.level = 1;
                gameManager.area = 2;
                levelToLoad = "Mountains 1"; 
            }
        }
        else if (gameManager.area == 2)
        {
            if (gameManager.level == 1 || gameManager.level == 2 || gameManager.level == 3)
            {
                gameManager.level++;
                levelToLoad = "Mountains " + gameManager.level;
            }
            else
            {
                gameManager.level = 1;
                gameManager.area = 3;
                levelToLoad = "Forest 1";
            }
        }


        levelNumber.text = gameManager.area + " - " + gameManager.level;

        IEnumerator enumerator = pauseBetweenLevel(5.0f);
        StartCoroutine(enumerator);
    }

    private IEnumerator pauseBetweenLevel(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(levelToLoad);
        canvas.gameObject.SetActive(true);
    }
}
