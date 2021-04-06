using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool enemyTurn = false;
    public Dictionary<Vector3, WorldTile> tiles = new Dictionary<Vector3, WorldTile>();
    public bool moveEnemy = false;
    public bool gettingMove = false;
    Vector3 enemyMove;

    // Update is called once per frame
    void Update()
    {

        if (tiles.Count == 0)
            tiles = GameObject.FindGameObjectWithTag("levelCollider").GetComponent<levelToDict>().tiles;
        
        if (moveEnemy)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, enemyMove, Time.deltaTime * 2f);
            if(gameObject.transform.position == enemyMove)
            {
                moveEnemy = false;
                enemyTurn = false;
                gettingMove = false;
            }
        }

        if (!enemyTurn)
            gettingMove = false;

        if (enemyTurn && !gettingMove)
            prepMove();
    }

    public void prepMove()
    {
        gettingMove = true;
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        int xdist = (int)gameObject.transform.position.x - (int)playerPos.x;
        int ydist = (int)gameObject.transform.position.y - (int)playerPos.y;

        WorldTile tiletoCheck = new WorldTile();
        Debug.Log(playerPos + " " + xdist + " " + ydist);
        //if x dist is the greatest move in x direction
        if (Mathf.Abs(xdist) > Mathf.Abs(ydist))
        {
            //if neg
            if (xdist < 0)
            {
               enemyMove = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, 0);

                if (tiles.ContainsKey(enemyMove))
                {
                    tiles.TryGetValue(enemyMove, out tiletoCheck);
                    if (tiletoCheck != null && !tiletoCheck.occupied)
                        moveEnemy = true;
                    else
                        enemyTurn = false;
                }    
            }
            else
            {
                enemyMove = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, 0);

                if (tiles.ContainsKey(enemyMove))
                {
                    tiles.TryGetValue(enemyMove, out tiletoCheck);
                    if (tiletoCheck != null && !tiletoCheck.occupied)
                        moveEnemy = true;
                    else
                        enemyTurn = false;
                }
            }
        }

        //if y dist is the greatest move in x direction
        else 
        {
            //if neg
            if (ydist < 0)
            {
                enemyMove = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, 0);

                if (tiles.ContainsKey(enemyMove))
                {
                    tiles.TryGetValue(enemyMove, out tiletoCheck);
                    if (tiletoCheck != null && !tiletoCheck.occupied)
                        moveEnemy = true;
                    else
                        enemyTurn = false;
                }
            }
            else
            {
                enemyMove = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, 0);

                if (tiles.ContainsKey(enemyMove))
                {
                    tiles.TryGetValue(enemyMove, out tiletoCheck);
                    if (tiletoCheck != null && !tiletoCheck.occupied)
                        moveEnemy = true;
                    else
                        enemyTurn = false;
                }
            }
            Debug.Log(tiletoCheck.tilePosition);
        }
        

    }
}
