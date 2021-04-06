using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool enemyTurn = false;
    public Dictionary<Vector3, WorldTile> tiles = new Dictionary<Vector3, WorldTile>();
    public bool moveEnemy = false;
    public bool gettingMove = false; //enemy is running prep move and seeing if it can move
    bool checkSecondPos = false; //if enemy can not move to tile in direction of greatest distance -- it checks if it can move to a tile in the direction of the lesser distance
    Vector3 enemyMove;

    int xdist = 0;
    int ydist = 0;
    WorldTile tiletoCheck = new WorldTile();

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
        {
            gettingMove = false;
            checkSecondPos = false;
        }

        if (enemyTurn && !gettingMove)
            prepMove();
    }

    public void prepMove()
    {
        gettingMove = true;
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        xdist = (int)gameObject.transform.position.x - (int)playerPos.x;
        ydist = (int)gameObject.transform.position.y - (int)playerPos.y;

       
        //if x dist is the greatest move in x direction
        if (Mathf.Abs(xdist) > Mathf.Abs(ydist))
        {
            checkX();
        }

        //if y dist is the greatest move in x direction
        else 
        {
            checkY();
        }
    }

    public void checkX()
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
                else if (!checkSecondPos)
                {
                    checkSecondPos = true;
                    checkY();
                }
                else
                    enemyTurn = false;
            }
        }
        else if (xdist > 0)
        {
            enemyMove = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, 0);

            if (tiles.ContainsKey(enemyMove))
            {
                tiles.TryGetValue(enemyMove, out tiletoCheck);
                if (tiletoCheck != null && !tiletoCheck.occupied)
                    moveEnemy = true;
                else if (!checkSecondPos)
                {
                    checkSecondPos = true;
                    checkY();
                }
                else
                    enemyTurn = false;
            }
        }
        else
            enemyTurn = false;
    }

    public void checkY()
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
                else if (!checkSecondPos)
                {
                    checkSecondPos = true;
                    checkX();
                }
                else
                    enemyTurn = false;
            }
        }
        else if (ydist > 0)
        {
            enemyMove = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, 0);

            if (tiles.ContainsKey(enemyMove))
            {
                tiles.TryGetValue(enemyMove, out tiletoCheck);
                if (tiletoCheck != null && !tiletoCheck.occupied)
                    moveEnemy = true;
                else if (!checkSecondPos)
                {
                    checkSecondPos = true;
                    checkX();
                }
                else
                    enemyTurn = false;
            }
        }
        else
            enemyTurn = false;
    }
}
