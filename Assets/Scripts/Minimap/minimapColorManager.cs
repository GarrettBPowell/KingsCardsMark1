using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minimapColorManager : MonoBehaviour
{
    GameManager gameManager;
    int currentLevel = 0;
    public RawImage map;
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.level != currentLevel)
        {
            currentLevel = gameManager.level;
            if (gameManager.area == 2)
            {
                switch (currentLevel)
                {
                    case 1:
                        map.color = new Color(1, 1, 1, 1);
                        break;
                    case 2:
                        map.color = new Color(0.7f, 0.7f, 0.7f, 1);
                        break;
                    case 3:
                        map.color = new Color(1, 1, 1, 1);
                        break;
                    case 4:
                        map.color = new Color(0.7f, 0.7f, 0.7f, 1);
                        break;

                }
            }

            
        }
    }
}
