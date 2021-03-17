using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyInRoom : MonoBehaviour
{
    GameObject room;
    private bool loadedRoom = false;


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("room") && !loadedRoom)
        {
            collision.gameObject.GetComponent<EnemiesInRoom>().enemiesInRoom += 1;
            room = collision.gameObject;
            loadedRoom = true;
        }

        //enemy leaves tile
        if (collision.gameObject.CompareTag("FloorTile"))
            collision.GetComponent<WorldTile>().setOccupied(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FloorTile"))
            collision.GetComponent<WorldTile>().setOccupied(false);
    }

    private void OnDestroy()
    {
        room.GetComponent<EnemiesInRoom>().enemiesInRoom -= 1;
    }
}
