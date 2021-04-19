using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{
    public int numTimes = 0;
    public List<GameObject> enemies;
    public string levelType = "caves";

    public Transform roomParent;

    GameManager gameManager;
    public Dictionary<Vector3, WorldTile> tiles = new Dictionary<Vector3, WorldTile>();

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (tiles.Count > 0)
        {
            for (int i = 0; i < numTimes; i++)
            {
                WorldTile tileToSpawnOn = null;
                Vector3 spawnPosition = gameObject.transform.position;
                int count = 0;
                while (tileToSpawnOn == null && count < 10)
                {
                    int randX = Random.Range((int)spawnPosition.x - 5, (int)spawnPosition.x + 5);
                    int randY = Random.Range((int)spawnPosition.y - 5, (int)spawnPosition.y + 5);
                    Vector3Int dictKey = new Vector3Int(randX, randY, 0);


                    if (tiles.TryGetValue(dictKey, out tileToSpawnOn))
                    {
                        if (!tileToSpawnOn.occupied)
                        {
                            if (levelType.Equals("caves"))
                            {
                                Instantiate(enemies[Random.Range(0, enemies.Count)], dictKey, Quaternion.identity, roomParent);
                            }
                        }
                        else
                            tileToSpawnOn = null;
                    }
                    else
                        tileToSpawnOn = null;

                    count++;
                }
            }
            Destroy(gameObject);
        }
        if (tiles.Count == 0)
            tiles = GameObject.FindGameObjectWithTag("levelCollider").GetComponent<levelToDict>().tiles;
        if (numTimes == 0)
            numTimes = Random.Range(0, gameManager.level + 2);
    }
}
