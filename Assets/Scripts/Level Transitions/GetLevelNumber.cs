using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetLevelNumber : MonoBehaviour
{
    GameManager gameManager;
    public Text levelNumber;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        levelNumber.text = gameManager.area + " - " + gameManager.level;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
