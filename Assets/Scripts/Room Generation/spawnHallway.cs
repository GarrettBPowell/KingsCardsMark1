using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnHallway : MonoBehaviour
{

    public GameObject[] cave1Hallways;
    public string levelType = "cave1Rooms";

    void Start()
    {
        Vector3 spawnPosition = gameObject.transform.position;
        if (levelType.Equals("cave1Rooms"))
        {
            Instantiate(cave1Hallways[Random.Range(0, cave1Hallways.Length)], spawnPosition, Quaternion.identity);
        }
    }

}
