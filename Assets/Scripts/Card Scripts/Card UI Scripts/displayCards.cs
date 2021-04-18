using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displayCards : MonoBehaviour
{
    public GameManager gameManager;

    Vector2[] cardLocationArr = new Vector2[5];
    GameObject[] cardsDisplayed = new GameObject[5];
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);

        for(int i = 0; i < 5; i++)
        {
            cardLocationArr[i] = new Vector3(pos.x + 375f * i, pos.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.playerHand.Count > 0 && gameManager.displayHand && gameManager.drewExtraCards)
        {
            gameManager.drewExtraCards = false;
            gameManager.displayHand = false;

            foreach (GameObject c in gameManager.playerHand)
            {
                int index = 0;
                foreach (GameObject g in cardsDisplayed)
                {
                    if (g == null)
                    {
                        GameObject cardMade = Instantiate(c, cardLocationArr[index], Quaternion.identity, gameObject.transform);
                        cardsDisplayed[index] = cardMade;
                        break;
                    }
                    index++;
                }
                gameManager.discardPile.Add(c);
            }
            gameManager.playerHand.RemoveRange(0, gameManager.playerHand.Count);
        }

        else if (gameManager.playerHand.Count > 0 && gameManager.displayHand)
        {
            gameManager.displayHand = false;

            foreach (GameObject c in gameManager.playerHand)
            {
                int index = 0;
                foreach (GameObject g in cardsDisplayed)
                {
                    if (g == null)
                    {
                        GameObject cardMade = Instantiate(c, cardLocationArr[index], Quaternion.identity, gameObject.transform);
                        cardsDisplayed[index] = cardMade;
                        break;
                    }
                    index++;
                }
                gameManager.discardPile.Add(c);
            }
            gameManager.playerHand.RemoveRange(0, gameManager.playerHand.Count);
        }
    }
}
