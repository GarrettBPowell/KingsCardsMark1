using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int playerHealth = 60;
    public int level = 1;
    public bool outOfCombat = false;

   
    private void Awake()
    {
        GameObject[] gameManager = GameObject.FindGameObjectsWithTag("gameManager");

        if (gameManager.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
