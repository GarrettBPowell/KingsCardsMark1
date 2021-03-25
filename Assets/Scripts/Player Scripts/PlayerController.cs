using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    public Joystick joystick;

    public GameObject gameObjectToMove;
    public Dictionary<Vector3, WorldTile> tiles = new Dictionary<Vector3, WorldTile>();
    public bool outOfCombat;
    public bool isMovePlayer;
    public string currentScene;

    Vector3 movePlayer;
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        outOfCombat = gameManager.GetComponent<GameManager>().outOfCombat;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
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
            {
                isMovePlayer = false;
            }

            gameObjectToMove.transform.position = Vector3.MoveTowards(gameObjectToMove.transform.position, movePlayer, Time.deltaTime * 2f);
        }
        else
            movePlayer = new Vector3(gameObjectToMove.transform.position.x, gameObjectToMove.transform.position.y, gameObjectToMove.transform.position.z);

        //add bool wrapper here
        if (!gameManager.isInCombatMoving)
        {
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
                        //Debug.Log(localPlace);
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
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //go get dict if the level is not the level screen which has 0 tiles
        if(!SceneManager.GetActiveScene().name.Equals("Level Screen"))
            tiles = GameObject.FindGameObjectWithTag("levelCollider").GetComponent<levelToDict>().tiles;
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
