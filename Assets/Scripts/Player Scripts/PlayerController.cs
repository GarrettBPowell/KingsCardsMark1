using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameObjectToMove;
    public 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown("w"))
        {
            gameObjectToMove.transform.position = new Vector3(gameObjectToMove.transform.position.x, gameObjectToMove.transform.position.y + 1, -1);
        }
        else if (Input.GetKeyDown("a"))
        {
            gameObjectToMove.transform.position = new Vector3(gameObjectToMove.transform.position.x - 1, gameObjectToMove.transform.position.y, -1);
        }
        else if (Input.GetKeyDown("s"))
        {
            gameObjectToMove.transform.position = new Vector3(gameObjectToMove.transform.position.x, gameObjectToMove.transform.position.y  - 1, -1);
        }
        else if (Input.GetKeyDown("d"))
        {
            gameObjectToMove.transform.position = new Vector3(gameObjectToMove.transform.position.x + 1, gameObjectToMove.transform.position.y, -1);
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
