using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int playerHealth = 60;
    public int level = 1;
    public bool outOfCombat = false;

    //UI STUFF
    public Button moveButton;
    public Button attackButton;


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
        if (outOfCombat)
        {
            attackButton.gameObject.SetActive(false);
        }

        else
        {
            attackButton.gameObject.SetActive(true);
        }
    }
}
