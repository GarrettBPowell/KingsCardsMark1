using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Camera playerCamera;
    public Vector3 hallwayPos;

    public bool inHallway;
    // Update is called once per frame
    void Update()
    {
        if (inHallway)
            playerCamera.transform.position = new Vector3(hallwayPos.x, hallwayPos.y, -10);
        else
            playerCamera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("hallway"))
        {
            inHallway = true;
            hallwayPos = collision.gameObject.transform.localPosition;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        inHallway = false;
    }
}
