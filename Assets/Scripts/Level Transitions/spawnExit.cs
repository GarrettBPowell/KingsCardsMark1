using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnExit : MonoBehaviour
{
    public GameObject[] caveExits;
    public string levelType = "caves";
    public Transform roomParent;
    void Start()
    {
        Vector3 spawnPosition = gameObject.transform.position;
        if (levelType.Equals("caves"))
        {
            Instantiate(caveExits[Random.Range(0, caveExits.Length)], spawnPosition, Quaternion.identity, roomParent);
        }
    }
}
