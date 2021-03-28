using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerMinimap : MonoBehaviour
{
    public GameObject minimapWalls;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            minimapWalls.SetActive(true);
            
    }
}
