using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pauseScreen : MonoBehaviour
{
    public GameObject pauseScreenPanel;

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    
    public void enablePauseScreen()
    {
        gameManager.screenPaused = true;
        pauseScreenPanel.SetActive(true);
    }

    public void resumeGame()
    {
        gameManager.screenPaused = false;
        pauseScreenPanel.SetActive(false);
    }

    public void quitToTile()
    {
        
        SceneManager.LoadScene("Title Screen");
        Destroy(GameObject.FindGameObjectWithTag("dontDestroyOnLoad"));
    }
}
