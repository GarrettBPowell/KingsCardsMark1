using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnLayout : MonoBehaviour
{
    public GameObject[] caveLayouts;
    public string levelType = "caves";
    public Transform roomParent;
    void Start()
    {
        Vector3 spawnPosition = gameObject.transform.position;
        if (levelType.Equals("caves"))
        {
            Instantiate(caveLayouts[Random.Range(0, caveLayouts.Length)], spawnPosition, Quaternion.identity, roomParent);
        }
    }
}

