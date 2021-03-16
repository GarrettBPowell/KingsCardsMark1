using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesInRoom : MonoBehaviour
{
    GameManager gameManager;

    public bool enemiesInRoom = false;
    public bool playerInRoom = false;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesInRoom && playerInRoom)
            gameManager.GetComponent<GameManager>().outOfCombat = false;
        if (!enemiesInRoom && playerInRoom)
        {
            gameManager.GetComponent<GameManager>().outOfCombat = true;
            gameManager.GetComponent<GameManager>().enemiesInRoomDiedHideMoveButton = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("enemy"))
            enemiesInRoom = true;

        else if (collision.gameObject.tag.Equals("Player"))
            playerInRoom = true;

        else
            enemiesInRoom = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            playerInRoom = true;
    }
}