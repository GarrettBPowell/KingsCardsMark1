using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterStats : MonoBehaviour
{
    GameManager gameManager;

    //All of the movement Stuff
        public int moveDistance; //distance character can move
        public bool wantsToMove = false; //indicates if the character wants to move this turn
        public WorldTile[] tilesToMoveTo = new WorldTile[15]; //stores tiles character wants to move to
        public int tilesInArray; //number of tiles in above array
    //

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
}


