using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int playerHealth = 60;

    //level variables
    public int level = 1;
    public int area = 1;

    //movement vars
    public bool outOfCombat = true; //tells any movement and any other scripts that need to know if the player is in combat (enemies are in room the player is in) or not in combat
    public bool isInCombatMoving; //tells the out of combat player controller if the in combat controller is still moving the player

    public GameObject character;

    //currently if its a moboile game allow joystick to show
    public bool isMobile = false;


    //UI STUFF
    public Joystick joystick;
    public Button moveButton;
    public Button attackButton;
    
    public bool enemiesInRoomDiedHideMoveButton; //this is used to recall the hide button if all of the enemies the player is in the room with have died so the button now needs to be hidden due to being changed to out of conbat

    private void Awake()
    {
        GameObject[] gameManager = GameObject.FindGameObjectsWithTag("gameManager");

        if (gameManager.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(transform.root.gameObject);
    }
    void Start()
    {
        //if the controls are not mobile controls, dont enable the joystick
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
