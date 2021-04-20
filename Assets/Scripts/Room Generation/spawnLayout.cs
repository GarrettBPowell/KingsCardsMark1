using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnLayout : MonoBehaviour
{
    public GameObject[] Layouts;
    public string levelType = "caves";
    public Transform roomParent;
    void Start()
    {
        Vector3 spawnPosition = gameObject.transform.position;
        Instantiate(Layouts[Random.Range(0, Layouts.Length)], spawnPosition, Quaternion.identity, roomParent);
        
    }
}

