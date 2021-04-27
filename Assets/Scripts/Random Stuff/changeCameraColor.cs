using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeCameraColor : MonoBehaviour
{
    public List<Color> cameraColor;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Level Screen"))
        {
            if(gameManager.area == 2)
            {
                if(gameManager.level % 2 == 1)
                {
                    gameObject.GetComponent<Camera>().backgroundColor = cameraColor[1];
                }
                else
                    gameObject.GetComponent<Camera>().backgroundColor = cameraColor[2];
            }
            else if(gameManager.area == 3)
            {
                gameObject.GetComponent<Camera>().backgroundColor = cameraColor[3];
            }
        }
    }
}
