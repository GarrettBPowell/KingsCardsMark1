using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpCard : MonoBehaviour
{
    GameManager gameManager;
    public List<GameObject> cardData;

    void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject cardToAdd = cardData[Random.Range(0, cardData.Count)];
            gameManager.playerDeck.Add(cardToAdd);
            Destroy(gameObject);
        }
    }
}
