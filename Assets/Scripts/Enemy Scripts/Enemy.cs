using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : MonoBehaviour
{
    GameManager gameManager;

    public bool enemyTurn = false;
    public Dictionary<Vector3, WorldTile> tiles = new Dictionary<Vector3, WorldTile>();
    public bool moveEnemy = false;
    public bool gettingMove = false; //enemy is running prep move and seeing if it can move
    bool checkSecondPos = false; //if enemy can not move to tile in direction of greatest distance -- it checks if it can move to a tile in the direction of the lesser distance
    Vector3 enemyMove;

    public Vector3 playerPos;

    //enemy data
    public string enemyType;
    public string enemyZone;
    public int enemyHealth;
    public int enemyDamage;

    public Dictionary<string, int> enemyStatusEffects = new Dictionary<string, int>();

    public List<GameObject> projectiles;

    public static int xdist = 0;
    public static int ydist = 0;
    WorldTile tiletoCheck;

    bool endEnemyTurn = false;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (enemyHealth <= 0)
        {
            gameObject.GetComponent<spawnCard>().spawnTheCard();
            Destroy(gameObject);
        }
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
            if (endEnemyTurn)
            {
                endEnemyTurn = false;
                foreach (string key in enemyStatusEffects.Keys.ToList())
                {
                    enemyStatusEffects[key] -= 1;
                }
            }
        }
        else
            endEnemyTurn = true;


        if (enemyTurn && !gettingMove)
            prepMove();
    }

    public void prepMove()
    {
        gettingMove = true;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

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
        //if ranged enemy then fire
        if((Mathf.Abs(xdist) == 0 ||  Mathf.Abs(ydist) == 0) && enemyType.Equals("ranged"))
        {
                spawnProjectile();

            enemyTurn = false;
        }
        //if neg
        else if (xdist < 0)
        {
            enemyMove = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, 0);

            if (tiles.ContainsKey(enemyMove))
            {
                tiles.TryGetValue(enemyMove, out tiletoCheck);
                if (tiletoCheck != null && !tiletoCheck.getOccupied())
                    moveEnemy = true;
                else if (!checkSecondPos)
                {
                    checkSecondPos = true;
                    checkY();
                }
                else
                    enemyTurn = false;
            }
            else if (!checkSecondPos)
            {
                checkSecondPos = true;
                checkY();
            }
            else
                enemyTurn = false;
        }
        else if (xdist > 0)
        {
            enemyMove = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, 0);

            if (tiles.ContainsKey(enemyMove))
            {
                tiles.TryGetValue(enemyMove, out tiletoCheck);
                if (tiletoCheck != null && !tiletoCheck.getOccupied())
                    moveEnemy = true;
                else if (!checkSecondPos)
                {
                    checkSecondPos = true;
                    checkY();
                }
                else
                    enemyTurn = false;
            }
            else if (!checkSecondPos)
            {
                checkSecondPos = true;
                checkY();
            }
            else
                enemyTurn = false;
        }
        //enemy is a melee type and is 1 block away from the character, it attacks
        else if ((Mathf.Abs(xdist) + Mathf.Abs(ydist) == 1) && enemyType.Equals("melee"))
        {
            attackPlayer();
        }
        else
            enemyTurn = false;
    }

    public void checkY()
    {
        //if ranged enemy then fire
        if ((Mathf.Abs(xdist) == 0 || Mathf.Abs(ydist) == 0) && enemyType.Equals("ranged"))
        {
                spawnProjectile();

            enemyTurn = false;
        }
        //if neg
        else if (ydist < 0)
        {
            enemyMove = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, 0);

            if (tiles.ContainsKey(enemyMove))
            {
                tiles.TryGetValue(enemyMove, out tiletoCheck);
                if (tiletoCheck != null && !tiletoCheck.getOccupied())
                    moveEnemy = true;
                else if (!checkSecondPos)
                {
                    checkSecondPos = true;
                    checkX();
                }
                else
                    enemyTurn = false;
            }
            else if (!checkSecondPos)
            {
                checkSecondPos = true;
                checkX();
            }
            else
                enemyTurn = false;
        }
        else if (ydist > 0)
        {
            enemyMove = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, 0);

            if (tiles.ContainsKey(enemyMove))
            {
                tiles.TryGetValue(enemyMove, out tiletoCheck);
                if (tiletoCheck != null && !tiletoCheck.getOccupied())
                    moveEnemy = true;
                else if (!checkSecondPos)
                {
                    checkSecondPos = true;
                    checkX();
                }
                else
                    enemyTurn = false;
            }
            else if (!checkSecondPos)
            {
                checkSecondPos = true;
                checkX();
            }
            else
                enemyTurn = false;
        }
        //enemy is a melee type and is 1 block away from the character, it attacks
        else if ((Mathf.Abs(xdist) + Mathf.Abs(ydist) == 1) && enemyType.Equals("melee"))
        {
            attackPlayer();
        }   
        else
            enemyTurn = false;
    }

    public void attackPlayer()
    {
        int damageToDoToPlayer = enemyDamage;
        if (enemyStatusEffects.ContainsKey("weak"))
        {
            if (enemyStatusEffects["weak"] <= 0)
                enemyStatusEffects.Remove("weak");
            else
                damageToDoToPlayer = Mathf.FloorToInt(damageToDoToPlayer * 0.75f);
        }

        if (gameManager.playerStatusEffects.ContainsKey("defense"))
        {
            damageToDoToPlayer = Mathf.FloorToInt(damageToDoToPlayer * 0.75f);
        }

        if(damageToDoToPlayer - gameManager.playerDefense < 0)
        {
            gameManager.playerDefense -= damageToDoToPlayer;
            damageToDoToPlayer = 0;       
        }
        else
        {
            damageToDoToPlayer -= gameManager.playerDefense;
            gameManager.playerDefense = 0;
        }
        
        gameManager.playerHealth -= damageToDoToPlayer;
        enemyTurn = false;

        if (enemyType.Equals("projectile"))
            Destroy(gameObject);
    }

    public void spawnProjectile()
    {
        if (xdist == 0)
        {
            if (ydist < 0)
                Instantiate(projectiles[0], new Vector2(gameObject.transform.position.x, transform.position.y + 0.5f), Quaternion.identity, gameObject.transform);
            else
                Instantiate(projectiles[0], new Vector2(gameObject.transform.position.x, transform.position.y - 0.5f), Quaternion.identity, gameObject.transform);
        }
        else
        {
            if (xdist < 0)
                Instantiate(projectiles[0], new Vector2(gameObject.transform.position.x + 0.5f, transform.position.y), Quaternion.identity, gameObject.transform);
            else
                Instantiate(projectiles[0], new Vector2(gameObject.transform.position.x - 0.5f, transform.position.y), Quaternion.identity, gameObject.transform);
        }
    }
}
