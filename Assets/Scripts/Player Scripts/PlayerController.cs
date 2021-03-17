using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    public Joystick joystick;

    public GameObject gameObjectToMove;
    public Dictionary<Vector3, WorldTile> tiles = new Dictionary<Vector3, WorldTile>();
    public bool outOfCombat;
    public bool isMovePlayer;
    Vector3Int movePlayer;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        outOfCombat = gameManager.GetComponent<GameManager>().outOfCombat;
    }

    // Update is called once per frame
    void Update()
    {
        outOfCombat = gameManager.GetComponent<GameManager>().outOfCombat;
        WorldTile moveTile;

        if (tiles.Count == 0)
            tiles = GameObject.FindGameObjectWithTag("levelCollider").GetComponent<levelToDict>().tiles;

        if (isMovePlayer)
        {
            if (gameObjectToMove.transform.position == movePlayer)
                isMovePlayer = false;

            gameObjectToMove.transform.position = Vector3.MoveTowards(gameObjectToMove.transform.position, movePlayer, Time.deltaTime * 2f);
        }
        if (outOfCombat)
        {
            
            if (!isMovePlayer)
            {
                //get the input
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");

                //joysstick
                float horizontalJoyInput = joystick.Horizontal;
                float verticalJoyInput = joystick.Vertical;
                Debug.Log(horizontalJoyInput + "   " + verticalJoyInput);
                //is used to make sure the direction the joystick is going gets the larger value, either vert or horiz
                bool isVert = false;
                if (Mathf.Abs(verticalJoyInput) - Mathf.Abs(horizontalJoyInput) > 0)
                    isVert = true;
                else
                    isVert = false;

                //move up
                if (verticalInput > 0 || (verticalJoyInput > 0 && isVert))
                {
                    var localPlace = new Vector3Int((int)gameObjectToMove.transform.position.x, (int)gameObjectToMove.transform.position.y + 1, (int)gameObjectToMove.transform.position.z);
                    Debug.Log(localPlace);
                    try
                    {
                        //Debug.Log(tiles.TryGetValue(localPlace, out moveTile));
                        tiles.TryGetValue(localPlace, out moveTile);
                        if (moveTile != null && !moveTile.getOccupied())
                        {
                            movePlayer = localPlace;
                            isMovePlayer = true;
                        }
                    }
                    catch (KeyNotFoundException)
                    {
                        Debug.Log("Key  is not found y.");
                    }
                }

                //move left
                else if (horizontalInput < 0 || horizontalJoyInput < 0)
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

                //move down
                else if (verticalInput < 0 || (verticalJoyInput < 0 && isVert))
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

                //move right
                else if (horizontalInput > 0 || horizontalJoyInput > 0)
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
