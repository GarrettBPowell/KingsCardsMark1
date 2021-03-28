using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour
{
    GameObject player;
    GameManager gameManager;
    characterStats charStats;

    Vector3 specificPosition;

    bool methodExecuted;
    public Button cancelButton;
    public Button moveButton;

    private void Start()
    {
        player = GameObject.Find("character");
        gameManager = GameObject.FindObjectOfType<GameManager>();
        charStats = player.GetComponent<characterStats>();
    }

    //variable used to move character towards tile i in array
    private int i = 0;
    private void Update()
    {
        if (gameManager.isInCombatMoving)
        {
            charStats = player.GetComponent<characterStats>();
            player.transform.position = Vector3.MoveTowards(player.transform.position, specificPosition, Time.deltaTime * 2f);
            if (player.transform.position.x == specificPosition.x && player.transform.position.y == specificPosition.y)
            {
                if(i < charStats.tilesInArray)
                {
                    WorldTile w = charStats.tilesToMoveTo[i];
                    specificPosition.Set(w.tilePosition.x, w.tilePosition.y, player.transform.position.z);
                    i++;
                }

                //reset the used values to be used again
                else
                    resetStuff();
            }
        }
    }

    //basically the button handler - first click activates it to accept tile inputs and store in array - second click executes the movements
    public void changePlayerMove() 
    {
        if (charStats.wantsToMove)
        {
            doTheMove();
            methodExecuted = true;
        }

        if (player != null && !methodExecuted)
        {
            cancelButton.gameObject.SetActive(true);
            charStats.wantsToMove = true;
        }
        methodExecuted = false;
    }

    //preps where the char will move by setting the movement - udpate handles the movement smoothly
    public void doTheMove()
    {
        if (charStats.wantsToMove && charStats.tilesToMoveTo[0] != null)
        {
            moveButton.interactable = false;
            cancelButton.gameObject.SetActive(false);
            WorldTile w = charStats.tilesToMoveTo[0];
            specificPosition.Set(w.tilePosition.x, w.tilePosition.y, player.transform.position.z);
            gameManager.isInCombatMoving = true;
        }
    }

    public void noMove()
    {
       charStats.wantsToMove = false;
        resetStuff();
    }
    //reset the used values to be used again
    public void resetStuff()
    {
        for (int i = 0; i <charStats.tilesInArray; i++)
        {
            charStats.tilesToMoveTo[i].GetComponent<WorldTile>().setAddedBool(false);
            charStats.tilesToMoveTo[i] = null;
        }

        //Debug.Log("setting to false");
        charStats.wantsToMove = false;
        charStats.tilesInArray = 0;
        i = 0;
        cancelButton.gameObject.SetActive(false);

        gameManager.isInCombatMoving = false;
        moveButton.interactable = true;

        if(gameManager.outOfCombat)
            moveButton.gameObject.SetActive(false);
    }
}
