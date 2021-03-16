using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;

    // Start is called before the first frame update
    public GameObject gameObjectToMove;
    public Dictionary<Vector3, WorldTile> tiles = new Dictionary<Vector3, WorldTile>();
    public bool outOfCombat;
    public bool isMovePlayer;
    Vector3Int movePlayer;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        outOfCombat = gameManager.GetComponent<GameManager>().outOfCombat;
        WorldTile moveTile;

        if (tiles.Count == 0)
            tiles = GameObject.FindGameObjectWithTag("levelCollider").GetComponent<levelToDict>().tiles;
        if (outOfCombat)
        {
            if (isMovePlayer)
            {
                if (gameObjectToMove.transform.position == movePlayer)
                    isMovePlayer = false;

                gameObjectToMove.transform.position = Vector3.MoveTowards(gameObjectToMove.transform.position, movePlayer, Time.deltaTime * 2f);
            }
            else if (!isMovePlayer)
            {
                if (Input.GetKeyDown("w"))
                {
                    Debug.Log("w");
                    var localPlace = new Vector3Int((int)gameObjectToMove.transform.position.x, (int)gameObjectToMove.transform.position.y + 1, (int)gameObjectToMove.transform.position.z);
                    Debug.Log(localPlace);
                    try
                    {
                        Debug.Log(tiles.TryGetValue(localPlace, out moveTile));
                        tiles.TryGetValue(localPlace, out moveTile);
                        if (moveTile != null && !moveTile.getOccupied())
                        {
                            movePlayer = localPlace;
                            Debug.Log("Move: " + movePlayer + "  " + moveTile.transform.position);
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
                    var localPlace = new Vector3Int((int)gameObjectToMove.transform.position.x - 1, (int)gameObjectToMove.transform.position.y, (int)gameObjectToMove.transform.position.z);
                    try
                    {
                        tiles.TryGetValue(localPlace, out moveTile);
                        if (moveTile != null && !moveTile.getOccupied())
                        {
                            movePlayer = localPlace;
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
                    var localPlace = new Vector3Int((int)gameObjectToMove.transform.position.x, (int)gameObjectToMove.transform.position.y - 1, (int)gameObjectToMove.transform.position.z);
                    try
                    {
                        tiles.TryGetValue(localPlace, out moveTile);
                        if (moveTile != null && !moveTile.getOccupied())
                        {
                            movePlayer = localPlace;
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
                    var localPlace = new Vector3Int((int)gameObjectToMove.transform.position.x + 1, (int)gameObjectToMove.transform.position.y, (int)gameObjectToMove.transform.position.z);
                    try
                    {
                        tiles.TryGetValue(localPlace, out moveTile);
                        if (moveTile != null && !moveTile.getOccupied())
                        {
                            movePlayer = localPlace;
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
