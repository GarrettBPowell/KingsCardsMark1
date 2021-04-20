using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnRoom : MonoBehaviour
{
    public GameObject[] Rooms;

    void Start()
    {
        Vector3 spawnPosition = gameObject.transform.position;
            Instantiate(Rooms[Random.Range(0, Rooms.Length)], spawnPosition, Quaternion.identity, gameObject.transform);

    }
}
