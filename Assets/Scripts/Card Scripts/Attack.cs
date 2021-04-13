using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    public GameManager gameManager; 
    public Transform start;  //Location where to start adding my cards
    public Transform HandDeck; //The hand panel reference
    public float howManyAdded; // How many cards I added so far

    public bool drewThisTurn = false;

    //buttons
    public Button moveButton;
    public Button attackButton;
    public Button cancelAttackButton;
    public Button endTurnButton;

    
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        howManyAdded = 0.0f;
        shuffle();
    }

    private void Update()
    {
        if(gameManager.playerAttacked)
        {
            cancelAttackButton.gameObject.SetActive(false);
        }
        if(gameManager.outOfCombat)
        {
            cancelAttackButton.gameObject.SetActive(false);
            endTurnButton.gameObject.SetActive(false);
        }
    }

    public void endTurn()
    {
        gameManager.playerTurn = false;
        gameManager.playerAttacked = false;

        cancelAttackButton.gameObject.SetActive(false);
        moveButton.gameObject.SetActive(true);
        attackButton.interactable = true;
        endTurnButton.gameObject.SetActive(false);
        gameManager.wantsToAttack = false;
        attackButton.GetComponent<Attack>().drewThisTurn = false;
    }

    public void cancelAttack()
    {
        if (!gameManager.playerAttacked)
        {
            gameManager.cancelAttackHit = true;
            endTurnButton.gameObject.SetActive(false);
            attackButton.interactable = true;
            cancelAttackButton.gameObject.SetActive(false);
            gameManager.wantsToAttack = false;
        }
    }

    //normal draw card function
    public void draw()
    {
        gameManager.wantsToAttack = true;
        attackButton.interactable = false;

        cancelAttackButton.gameObject.SetActive(true);
        endTurnButton.gameObject.SetActive(true);

        if (!drewThisTurn)
        {
            drewThisTurn = true;
            for (int i = 0; i < gameManager.numCardsToDraw; i++)
            {
                if (gameManager.playerDeck.Count == 0)
                    shuffleDiscard();
                GameObject g = gameManager.playerDeck[0];
                gameManager.playerHand.Add(g);

                gameManager.playerDeck.RemoveAt(0);

                gameManager.displayHand = true;
            }
        }
        else
            gameManager.cancelAttackHit = false;
    }

    //if card causes you to draw extra cards
    public void drawExtra(int cardsToDraw)
    {
        for (int i = 0; i <cardsToDraw; i++)
        {
            if (gameManager.playerDeck.Count == 0)
                shuffleDiscard();
            GameObject g = gameManager.playerDeck[0];
            gameManager.playerHand.Add(g);

            gameManager.playerDeck.RemoveAt(0);

            gameManager.displayHand = true;
            gameManager.drawExtra = true;
        }
    }

    public void shuffle()
    {
        int index = 0;
        for (int i = 0; i < gameManager.playerDeck.Count; i++)
        {
            int rand = Random.Range(0, gameManager.playerDeck.Count);
            GameObject c = gameManager.playerDeck[index];
            gameManager.playerDeck[index] = gameManager.playerDeck[rand];
            gameManager.playerDeck[rand] = c;
        }
    }

    public void shuffleDiscard()
    {
        int index = 0;
        for (int i = 0; i < gameManager.discardPile.Count; i++)
        {
            int rand = Random.Range(0, gameManager.discardPile.Count);
            GameObject c = gameManager.discardPile[index];
            gameManager.discardPile[index] = gameManager.discardPile[rand];
            gameManager.discardPile[rand] = c;
        }
        foreach (GameObject c in gameManager.discardPile)
        {
            gameManager.playerDeck.Add(c);
            
        }
        gameManager.discardPile.RemoveRange(0, gameManager.discardPile.Count);
    }

}
