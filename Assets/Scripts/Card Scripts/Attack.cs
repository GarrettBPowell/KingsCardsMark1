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
    float gapFromOneItemToTheNextOne; //the gap I need between each card

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        howManyAdded = 0.0f;
        gapFromOneItemToTheNextOne = 1.0f;
    }

    public void draw()
    {
        Debug.Log("here");
        for(int i = 0; i < gameManager.numCardsToDraw; i++)
        {
            if (gameManager.playerDeck.Count == 0)
                shuffle();
            GameObject g = gameManager.playerDeck[0];
            gameManager.playerHand.Add(g);
            gameManager.discardPile.Add(g);
            gameManager.playerDeck.RemoveAt(0);
        }

        //FitCards();

    }

    public void FitCards()
    {
        if (gameManager.playerHand.Count == 0) //if list is null, stop function
            return;

        GameObject cardUI = gameManager.playerHand[0]; //Reference to first image in my list

       // cardUI.transform.position = start.position; //relocating my card to the Start Position
        //cardUI.transform.position += new Vector3((howManyAdded * gapFromOneItemToTheNextOne), 0, 0); // Moving my card 1f to the right
        //cardUI.transform.SetParent(HandDeck); //Setting ym card parent to be the Hand Panel

        gameManager.playerHand.RemoveAt(0);
        howManyAdded++;
    }
    
    public void shuffle()
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
        for (int i = 0; i < gameManager.discardPile.Count; i++)
            gameManager.discardPile.RemoveAt(0);
    }
}
