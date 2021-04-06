using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesInRoom : MonoBehaviour
{
    GameManager gameManager;

    public int enemiesInRoom = 0;
    public bool playerInRoom = false;
    public List<GameObject> enemiesInRoomList = new List<GameObject>();
    int i = 0;

    //Script is used to communicate with everything in the room -- mainly getting what enemies are in the room
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //player is not out of combat -- enemies in room
        if (enemiesInRoom > 0 && playerInRoom)
            gameManager.GetComponent<GameManager>().outOfCombat = false;

        //player is out of combat -- no ememies in room
        if (enemiesInRoom == 0 && playerInRoom)
        {
            gameManager.outOfCombat = true;
            gameManager.enemiesInRoomDiedHideMoveButton = true;
        }

        if (!gameManager.playerTurn && playerInRoom)
        {
            gameManager.enemyMoving = true;

            IEnumerator coroutine = WaitAndPrint(1.0f);
            StartCoroutine(coroutine);

            gameManager.playerTurn = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            playerInRoom = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            playerInRoom = false;
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (i < enemiesInRoomList.Count)
        {
            enemiesInRoomList[i].GetComponent<Enemy>().enemyTurn = true;
            yield return new WaitForSeconds(waitTime);
            i++;
        }
        i = 0;
        gameManager.enemyMoving = false;
    }
}