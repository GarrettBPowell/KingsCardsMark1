using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displayCards : MonoBehaviour
{
    public GameManager gameManager;
    Vector2 lastCardPos;
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.playerHand.Count > 0 && gameManager.displayHand && gameManager.drewExtraCards)
        {
            gameManager.drewExtraCards = false;
            gameManager.displayHand = false;
            int distOver = 150;
            foreach (GameObject c in gameManager.playerHand)
            {
                Vector3 pos = new Vector3(lastCardPos.x + distOver, gameObject.transform.position.y, 0);
                gameManager.discardPile.Add(c);
                Instantiate(c, pos, Quaternion.identity, gameObject.transform);
                lastCardPos = pos;
                distOver += 150;
            }
            gameManager.playerHand.RemoveRange(0, gameManager.playerHand.Count);
        }

        else if (gameManager.playerHand.Count > 0 && gameManager.displayHand)
        {
            gameManager.displayHand = false;
            int distOver = 0;
            foreach(GameObject c in gameManager.playerHand)
            {
                Vector3 pos = new Vector3(gameObject.transform.position.x + distOver, gameObject.transform.position.y, 0);
                gameManager.discardPile.Add(c);
                Instantiate(c, pos, Quaternion.identity, gameObject.transform);
                lastCardPos = pos;
                distOver += 150;
            }
            gameManager.playerHand.RemoveRange(0, gameManager.playerHand.Count);
        }
    }
}
