using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : Enemy
{
    public bool overTile = true;
    public int overTileCount = 2;
    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FloorTile"))
        {
            overTile = true;
            overTileCount = 2;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FloorTile"))
            overTile = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FloorTile"))
            overTile = false;
    }


    private void Update()
    {
        if (gameObject.transform.position != playerPos)
        {
            playerPos = GameObject.FindGameObjectWithTag("Player").transform.position; 
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, playerPos, Time.deltaTime * 10f);
        }
        else
            attackPlayer();

        if (!overTile)
            overTileCount--;
        if(overTileCount < 0)
            Destroy(gameObject);
    }
}
