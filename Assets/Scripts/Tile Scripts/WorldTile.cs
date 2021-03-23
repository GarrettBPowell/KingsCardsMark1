using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldTile : MonoBehaviour
{
    public string tileType;
    public string tileName;
    public bool occupied;
    public bool addedToMoveArray;
    public Vector3 tilePosition;
    private SpriteRenderer thisSprite;
    private Color originalColor;
    private void Awake()
    {
        var localPlace = new Vector3Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y, (int)gameObject.transform.position.z);
        tilePosition = localPlace;
        setTileName(localPlace.x + "," + localPlace.y);
        thisSprite  = GetComponent<SpriteRenderer>();
        originalColor = thisSprite.color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //will need one for enemy
        if(collision.gameObject.CompareTag("Player"))
            setOccupied(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            setOccupied(false);
    }

    private void Update()
    {
        if(addedToMoveArray)
        {
            thisSprite.color = new Color(0, 1, 0, 1);
        }
        else if(!addedToMoveArray && thisSprite.color == new Color(0, 1, 0, 1))
        {
            thisSprite.color = Color.white;
        }
    }
    public void setAddedBool(bool added)
    {
        addedToMoveArray = added;
    }
    public bool getAddedBool()
    {
        return addedToMoveArray;
    }
    public void setTileType(string type)
    {
        tileType = type;
    }
    public string getTileType()
    {
        return tileType;
    }

    public void setTileName(string name)
    {
        tileName = name;
    }
    public string getTileName()
    {
        return tileName;
    }

    public void setOccupied(bool occ)
    {
        occupied = occ;
    }
    public bool getOccupied()
    {
        return occupied;
    }
}
