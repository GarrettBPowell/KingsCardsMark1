using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameObjectToMove;
    public Dictionary<Vector3, WorldTile> tiles = new Dictionary<Vector3, WorldTile>();
    public bool outOfCombat;
    public bool isMovePlayer;
    Vector3 movePlayer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WorldTile moveTile;

        if (tiles.Count == 0)
            tiles = gameObject.GetComponentInParent<levelToDict>().tiles;
        if (isMovePlayer)
        {     
            if (gameObjectToMove.transform.position == movePlayer)
                isMovePlayer = false;
            else
                gameObjectToMove.transform.position = Vector3.MoveTowards(gameObjectToMove.transform.position, movePlayer, Time.deltaTime * 2f);
        }
        else
        {
            if (Input.GetKeyDown("w"))
            {
                var localPlace = new Vector3Int((int)gameObjectToMove.transform.position.x, (int)gameObjectToMove.transform.position.y + 1, -1);
                try 
                {
                    tiles.TryGetValue(localPlace, out moveTile);
                    if (moveTile != null && !moveTile.getOccupied())
                    {
                        movePlayer = new Vector3(localPlace.x, localPlace.y, gameObject.transform.position.z);
                        isMovePlayer = true;
                    }
                }
                catch (KeyNotFoundException)
                {
                  Debug.Log("Key  is not found y.");
                }
            }
            else if (Input.GetKeyDown("a"))
            {
                var localPlace = new Vector3Int((int)gameObjectToMove.transform.position.x -1, (int)gameObjectToMove.transform.position.y, -1);
                try
                {
                    tiles.TryGetValue(localPlace, out moveTile);
                    if (moveTile != null && !moveTile.getOccupied())
                    {
                        movePlayer = new Vector3(localPlace.x, localPlace.y, gameObject.transform.position.z);
                        isMovePlayer = true;
                    }
                }
                catch (KeyNotFoundException)
                {
                    Debug.Log("Key  is not found -x.");
                }
            }
            else if (Input.GetKeyDown("s"))
            {
                var localPlace = new Vector3Int((int)gameObjectToMove.transform.position.x, (int)gameObjectToMove.transform.position.y - 1, -1);
                try
                {
                    tiles.TryGetValue(localPlace, out moveTile);
                    if (moveTile != null && !moveTile.getOccupied())
                    {
                        movePlayer = new Vector3(localPlace.x, localPlace.y, gameObject.transform.position.z);
                        isMovePlayer = true;
                    }
                }
                catch (KeyNotFoundException)
                {
                    Debug.Log("Key  is not found -y.");
                }
            }
            else if (Input.GetKeyDown("d"))
            {
                var localPlace = new Vector3Int((int)gameObjectToMove.transform.position.x + 1, (int)gameObjectToMove.transform.position.y, -1);
                try
                {
                    tiles.TryGetValue(localPlace, out moveTile);
                    if (moveTile != null && !moveTile.getOccupied())
                    {
                        movePlayer = new Vector3(localPlace.x, localPlace.y, gameObject.transform.position.z);
                        isMovePlayer = true;
                    }
                }
                catch (KeyNotFoundException)
                {
                    Debug.Log("Key  is not found x.");
                }
            }
        }
       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FloorTile")
        {
            collision.gameObject.GetComponent<WorldTile>().setOccupied(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "FloorTile")
        {
            collision.gameObject.GetComponent<WorldTile>().setOccupied(false);
        }
    }

}
