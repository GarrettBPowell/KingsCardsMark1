using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnCard : MonoBehaviour
{
    public int chance;
    public List<GameObject> caveCards;
    public List<GameObject> mountainRainCards;
    public List<GameObject> mountainSnowCards;
    public List<GameObject> forestCards;
    public void spawnTheCard()
    {
        int randNum = Random.Range(0, 100);
        if(randNum < chance)
        {
            if(gameObject.GetComponent<Enemy>().enemyZone.Equals("cave"))
                Instantiate(caveCards[Random.Range(0, caveCards.Count)], gameObject.transform.position, Quaternion.identity);

            else if (gameObject.GetComponent<Enemy>().enemyZone.Equals("snow"))
                Instantiate(mountainSnowCards[Random.Range(0, mountainSnowCards.Count)], gameObject.transform.position, Quaternion.identity);

            else if (gameObject.GetComponent<Enemy>().enemyZone.Equals("rain"))
                Instantiate(mountainSnowCards[Random.Range(0, mountainSnowCards.Count)], gameObject.transform.position, Quaternion.identity);

            else if (gameObject.GetComponent<Enemy>().enemyZone.Equals("forest"))
                Instantiate(forestCards[Random.Range(0, forestCards.Count)], gameObject.transform.position, Quaternion.identity);
        }
    }
}
