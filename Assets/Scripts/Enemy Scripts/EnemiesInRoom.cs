using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesInRoom : MonoBehaviour
{
    GameManager gameManager;

    public int enemiesInRoom = 0;
    public bool playerInRoom = false;
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
            gameManager.GetComponent<GameManager>().outOfCombat = true;
            gameManager.GetComponent<GameManager>().enemiesInRoomDiedHideMoveButton = true;
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
}