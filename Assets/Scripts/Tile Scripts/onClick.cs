using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class onClick : MonoBehaviour 
{
    GameObject character;
    Vector3 playerPosition;
    Vector3 thisTilePosition;
    private void Awake()
    {
        character = GameObject.Find("character");
        playerPosition = character.transform.position;

        thisTilePosition = gameObject.transform.position;
    }


    private void OnMouseDown()
    {
        if (gameObject.tag == "FloorTile")
        {
            Debug.Log("Tile Clicked " + gameObject.GetComponent<WorldTile>().getOccupied() + " " + gameObject.GetComponent<WorldTile>().getAddedBool());
            if (character.GetComponent<characterStats>().wantsToMove)
            {
                //if tile clicked is not occupied and the player still has tiles left to move
                if (!gameObject.GetComponent<WorldTile>().getOccupied() && (!gameObject.GetComponent<WorldTile>().addedToMoveArray) && character.GetComponent<characterStats>().tilesInArray < character.GetComponent<characterStats>().moveDistance)
                {
                    //there are no previous tiles in the array
                    if (character.GetComponent<characterStats>().tilesToMoveTo[0] == null)
                    {
                        //if character and tile to move to is within 1
                        if ((Mathf.Abs(character.transform.position.x - thisTilePosition.x) == 1 && character.transform.position.y == thisTilePosition.y) || (Mathf.Abs(character.transform.position.y - thisTilePosition.y) == 1 && character.transform.position.x == thisTilePosition.x))
                        {
                            character.GetComponent<characterStats>().tilesToMoveTo[0] = gameObject.GetComponent<WorldTile>();
                            character.GetComponent<characterStats>().tilesInArray++;
                            gameObject.GetComponent<WorldTile>().setAddedBool(true);
                        }
                    }

                    //do the same checks as above, but with the previous tile in the array
                    else
                    {
                        int numTiles = character.GetComponent<characterStats>().tilesInArray;
                        Vector3 lastTilePosition = character.GetComponent<characterStats>().tilesToMoveTo[numTiles - 1].transform.position;
                        if ((Mathf.Abs(thisTilePosition.x - lastTilePosition.x) == 1 && thisTilePosition.y == lastTilePosition.y) || (Mathf.Abs(thisTilePosition.y - lastTilePosition.y) == 1 && thisTilePosition.x == lastTilePosition.x))
                        {
                            character.GetComponent<characterStats>().tilesToMoveTo[numTiles] = gameObject.GetComponent<WorldTile>();
                            character.GetComponent<characterStats>().tilesInArray++;
                            gameObject.GetComponent<WorldTile>().setAddedBool(true);
                        }
                    }
                }
            }
        }
    }
}
