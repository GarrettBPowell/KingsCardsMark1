using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameManager gameManager;

    //get access to all cards and their data
    public Dictionary<string, GameObject> UICardDict = new Dictionary<string, GameObject>();
    public List<GameObject> allCardsToAddToDict;

    //cards player has
    public List<GameObject> playerDeck;
    public List<GameObject> playerHand;

    public int playerMaxHealth = 60;
    public int playerHealth = 60;
    public List<string> playerStatusEffects = new List<string>();

    //level variables
    public int level = 1;
    public int area = 1;

    //movement vars
    public bool outOfCombat = true; //tells any movement and any other scripts that need to know if the player is in combat (enemies are in room the player is in) or not in combat
    public bool isInCombatMoving; //tells the out of combat player controller if the in combat controller is still moving the player

    public GameObject character;
    public bool playerTurn = true;
    public bool enemyMoving = false;

    //currently if its a moboile game allow joystick to show
    public bool isMobile = false;


    //UI STUFF
    public Joystick joystick;
    public Button moveButton;
    public Button attackButton;
    public Text healthText;
    public Slider healthSlider;
    
    public bool enemiesInRoomDiedHideMoveButton; //this is used to recall the hide button if all of the enemies the player is in the room with have died so the button now needs to be hidden due to being changed to out of conbat

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        //adds all cards in array to dict for easier access/faster look up
        foreach (GameObject g in allCardsToAddToDict)
        {
            UICardDict.Add(g.name, g);

        }    

        DontDestroyOnLoad(transform.root.gameObject);
    }
    void Start()
    {
        level = 1;
        area = 1;
        //if the controls are not mobile controls, dont enable the joystick
        if(!isMobile)
            joystick.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        healthText.text = "Health: " + playerHealth + " / " + playerMaxHealth;
        healthSlider.value = playerHealth;

        if (enemyMoving)
        {
            moveButton.interactable = false;
            attackButton.interactable = false;
        }
        else
        {
            moveButton.interactable = true;
            attackButton.interactable = true;
        }
        if(enemiesInRoomDiedHideMoveButton)
        {
            moveButton.gameObject.SetActive(false);
            enemiesInRoomDiedHideMoveButton = false;
        }

        if (outOfCombat)
        {
            playerTurn = true;
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
