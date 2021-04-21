using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnHallway : MonoBehaviour
{

    public GameObject[] Hallways;

    void Start()
    {
        Vector3 spawnPosition = gameObject.transform.position;
        Instantiate(Hallways[Random.Range(0, Hallways.Length)], spawnPosition, Quaternion.identity, gameObject.transform);
        
    }

}
