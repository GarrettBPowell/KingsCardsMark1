using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyInRoom : MonoBehaviour
{
    GameObject room;
    Collider2D tileEnemyIsOn;
    private bool loadedRoom = false;

    //script is used from enemies to tell room that they are in it
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("room") && !loadedRoom)
        {
            collision.gameObject.GetComponent<EnemiesInRoom>().enemiesInRoom += 1;
            collision.gameObject.GetComponent<EnemiesInRoom>().enemiesInRoomList.Add(gameObject);
            room = collision.gameObject;
            loadedRoom = true;
        }

        //enemy leaves tile
        if (collision.gameObject.CompareTag("FloorTile"))
        {
            collision.GetComponent<WorldTile>().setOccupied(true);
            collision.GetComponent<WorldTile>().setObject(gameObject);
            tileEnemyIsOn = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FloorTile"))
        {
            collision.GetComponent<WorldTile>().setOccupied(false);
            collision.GetComponent<WorldTile>().setObject(null);
        }
    }

    private void OnDestroy()
    {
        if (room != null)
        {
            room.GetComponent<EnemiesInRoom>().enemiesInRoomList.Remove(gameObject);
            room.GetComponent<EnemiesInRoom>().enemiesInRoom -= 1;

            //makes sure tile is no longer occupied when enemy is killed
            tileEnemyIsOn.GetComponent<WorldTile>().setOccupied(false);
        }
    }
}

