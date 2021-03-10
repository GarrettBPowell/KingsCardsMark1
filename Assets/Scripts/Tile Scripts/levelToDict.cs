using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelToDict : MonoBehaviour
{
    public Dictionary<Vector3, WorldTile> tiles = new Dictionary<Vector3, WorldTile>();

    public void OnTriggerEnter2D (Collider2D collision)
    {
        var localPlace = new Vector3Int((int)collision.transform.position.x, (int)collision.transform.position.y, (int)collision.transform.position.z);

        if (collision.gameObject.tag == "FloorTile" && !tiles.ContainsKey(localPlace))  
            tiles.Add(localPlace, collision.gameObject.GetComponent<WorldTile>()); 
    }


    private void Update()
    {
        //Vector3Int place = new Vector3Int(1, 9, 0);
        //Debug.Log(tiles.ContainsKey(place));
    }
}
