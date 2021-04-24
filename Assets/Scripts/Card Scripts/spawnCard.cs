using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnCard : MonoBehaviour
{
    public int chance;
    public List<GameObject> caveCards;
    public List<GameObject> mountainCards;
    public List<GameObject> forestCards;
    public void spawnTheCard()
    {
        int randNum = Random.Range(0, 100);
        if(randNum < chance)
        {
            if(gameObject.GetComponent<Enemy>().enemyZone.Equals("cave"))
                Instantiate(caveCards[Random.Range(0, caveCards.Count)], gameObject.transform.position, Quaternion.identity);

            else if (gameObject.GetComponent<Enemy>().enemyZone.Equals("mountain"))
                Instantiate(mountainCards[Random.Range(0, mountainCards.Count)], gameObject.transform.position, Quaternion.identity);

            else if (gameObject.GetComponent<Enemy>().enemyZone.Equals("forest"))
                Instantiate(forestCards[Random.Range(0, forestCards.Count)], gameObject.transform.position, Quaternion.identity);
        }
    }
}
