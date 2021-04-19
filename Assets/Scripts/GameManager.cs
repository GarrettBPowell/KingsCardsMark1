using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class GameManager : MonoBehaviour
{

    //get access to all cards and their data
    public Dictionary<string, GameObject> UICardDict = new Dictionary<string, GameObject>();
    public List<GameObject> allCardsToAddToDict;

//PLAYER STUFF
    //cards player has
    public List<GameObject> playerDeck; //cards player can pull from
    public List<GameObject> playerHand; //cards in hand
    public List<GameObject> discardPile; //its a discard pile
    
    public int numCardsToDraw = 3;

    //hand cards
    public bool displayHand = true;
    public int drawExtra = 0;
    public bool drewExtraCards;

    //play stats, effects, anything else
    public int playerMaxHealth = 60;
    public int playerHealth = 60;
    public int playerDefense = 0;
    public int playerMaxDefense = 60;
    public Dictionary<string, int> playerStatusEffects = new Dictionary<string, int>();

    //movement vars
    public bool outOfCombat = true; //tells any movement and any other scripts that need to know if the player is in combat (enemies are in room the player is in) or not in combat
    public bool isInCombatMoving; //tells the out of combat player controller if the in combat controller is still moving the player
    public bool playerWantsToMove = false;

    public GameObject character;
    public string playerFacing;
    public bool playerTurn = true;
    public bool enemyMoving = false;

    //attack vars
    public bool playerAttacked = false;
    public bool wantsToAttack = false;
    public bool cancelAttackHit = false;

    //level variables
    public int level = 1;
    public int area = 1;
    public Light2D globalLight;

    //currently if its a moboile game allow joystick to show
    public bool isMobile = false;


    //UI STUFF
    public Joystick joystick;
    public Button moveButton;
    public Button attackButton;
    public Text healthText;
    public Slider healthSlider;
    public Image handDeckUI;
    public Slider defenseSlider;
    
    public bool enemiesInRoomDiedHideMoveButton; //this is used to recall the hide button if all of the enemies the player is in the room with have died so the button now needs to be hidden due to being changed to out of conbat

    private void Awake()
    {
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
        if (globalLight.enabled == false && SceneManager.GetActiveScene().name.Equals("Mountains 1"))
            globalLight.enabled = true;

        if (SceneManager.GetActiveScene().name.Equals("Level Screen"))
        {
            moveButton.GetComponent<MoveButton>().sceneChangeReset();
        }

        if (playerMaxDefense != playerMaxHealth)
        {
            playerMaxDefense = playerMaxHealth;
            healthSlider.maxValue = playerMaxHealth;
        }

        healthText.text = "Health: " + playerHealth + " / " + playerMaxHealth;
        healthSlider.value = playerHealth;
        defenseSlider.value = playerDefense;

        if (enemyMoving || isInCombatMoving)
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
            handDeckUI.enabled = false;
            wantsToAttack = false;
            if(!moveButton.gameObject.activeSelf)
            {
                if(isMobile)
                    joystick.gameObject.SetActive(true);
            }
        }
        else
        {
            joystick.gameObject.SetActive(false);

            handDeckUI.enabled = true;
            if(!playerWantsToMove)
                attackButton.gameObject.SetActive(true);
            else
                attackButton.gameObject.SetActive(false);
            if (!wantsToAttack)
            {
                if (!joystick.gameObject.activeSelf)
                    moveButton.gameObject.SetActive(true);
            }
            else
            {
                moveButton.gameObject.SetActive(false);
            }
        }

        if(drawExtra > 0)
        {
            attackButton.GetComponent<Attack>().drawExtra(drawExtra);
            drawExtra = 0;
        }
    }
}
