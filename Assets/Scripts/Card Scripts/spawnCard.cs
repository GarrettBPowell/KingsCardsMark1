using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnCard : MonoBehaviour
{
    public int chance;
    public List<GameObject> cardsToSpawnFrom;
    private void OnDestroy()
    {
        int randNum = Random.Range(0, 100);
        if(randNum < chance)
        {
            Instantiate(cardsToSpawnFrom[Random.Range(0, cardsToSpawnFrom.Count)], gameObject.transform.position, Quaternion.identity);
        }
    }
}
