using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnRoom : MonoBehaviour
{
    public GameObject[] cave1Rooms;
    public string levelType = "cave1Rooms";

    void Start()
    {
        Vector3 spawnPosition = gameObject.transform.position;
        if(levelType.Equals("cave1Rooms"))
        {
            Instantiate(cave1Rooms[Random.Range(0, cave1Rooms.Length)], spawnPosition, Quaternion.identity, gameObject.transform);
        }
    }
}
