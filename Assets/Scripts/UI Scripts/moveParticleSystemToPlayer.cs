using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveParticleSystemToPlayer : MonoBehaviour
{
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + 9, player.transform.position.y + 9);
    }
}
