using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displayCards : MonoBehaviour
{
    public GameManager gameManager;
    public bool display = true;
    private List<GameObject> cardsToDestroy;
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.playerHand.Count > 0 && display)
        {
            display = false;
            int distOver = gameManager.playerHand.Count * 100;
            foreach(GameObject c in gameManager.playerHand)
            {
                Vector3 pos = new Vector3(gameObject.transform.position.x - distOver, gameObject.transform.position.y, 0);
                Instantiate(c, pos, Quaternion.identity, gameObject.transform);
                //cardsToDestroy.Add(c);
                distOver -= 100;
            }
        }
    }

    public void destroyUnusedCards()
    {
        for(int i = 0; i < cardsToDestroy.Count; i++)
        {
            Destroy(cardsToDestroy[0]);
            cardsToDestroy.RemoveAt(0);
        }
    }
}
