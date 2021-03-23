using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int playerHealth = 60;
    public int level = 1;

    //movement vars
    public bool outOfCombat = true;
    public bool isInCombatMoving;

    public GameObject character;

    //currently if its a moboile game allow joystick to show
    public bool isMobile = false;


    //UI STUFF
    public Joystick joystick;
    public Button moveButton;
    public Button attackButton;
    
    public bool enemiesInRoomDiedHideMoveButton;
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
        if(!isMobile)
            joystick.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesInRoomDiedHideMoveButton)
        {
            moveButton.gameObject.SetActive(false);
            enemiesInRoomDiedHideMoveButton = false;
        }

        if (outOfCombat)
        {
            attackButton.gameObject.SetActive(false);
            if(!moveButton.gameObject.activeSelf)
            {
                if(isMobile)
                    joystick.gameObject.SetActive(true);
            }
        }
        else
        {
            joystick.gameObject.SetActive(false);

            attackButton.gameObject.SetActive(true);
            if(!joystick.gameObject.activeSelf)
                moveButton.gameObject.SetActive(true);
        }
    }
}
