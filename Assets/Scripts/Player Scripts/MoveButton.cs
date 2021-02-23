using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButton : MonoBehaviour
{
    GameObject player;
    Vector3 specificPosition;
    bool moveCharacter;
    bool methodExecuted;

    private void Start()
    {
        player = GameObject.Find("character");
    }

    //variable used to move character towards tile i in array
    private int i = 0;
    private void Update()
    {
        if(moveCharacter)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, specificPosition, Time.deltaTime * 2f);
            if (player.transform.position.x == specificPosition.x && player.transform.position.y == specificPosition.y)
            {
                if(i < player.GetComponent<characterStats>().tilesInArray)
                {
                    WorldTile w = player.GetComponent<characterStats>().tilesToMoveTo[i];
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
        if (player.GetComponent<characterStats>().wantsToMove)
        {
            doTheMove();
            methodExecuted = true;
        }

        if (player != null && !methodExecuted)
        {
            player.GetComponent<characterStats>().wantsToMove = true;
            methodExecuted = false;
        }
        methodExecuted = false;
    }

    //preps where the char will move by setting the movement - udpate handles the movement smoothly
    public void doTheMove()
    {
        if (player.GetComponent<characterStats>().wantsToMove && player.GetComponent<characterStats>().tilesToMoveTo[0] != null)
        {
            WorldTile w = player.GetComponent<characterStats>().tilesToMoveTo[0];
            specificPosition.Set(w.tilePosition.x, w.tilePosition.y, player.transform.position.z);
            moveCharacter = true;
        }
    }

    //reset the used values to be used again
    public void resetStuff()
    {
        for (int i = 0; i < player.GetComponent<characterStats>().tilesInArray; i++)
        {
            player.GetComponent<characterStats>().tilesToMoveTo[i].GetComponent<WorldTile>().setAddedBool(false);
            player.GetComponent<characterStats>().tilesToMoveTo[i] = null;
        }

        Debug.Log("setting to false");
        player.GetComponent<characterStats>().wantsToMove = false;
        player.GetComponent<characterStats>().tilesInArray = 0;
        i = 0;
        moveCharacter = false;
    }
}
