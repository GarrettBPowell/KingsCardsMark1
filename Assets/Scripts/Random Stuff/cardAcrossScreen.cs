using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardAcrossScreen : MonoBehaviour
{
    public Vector2 position;
    public Vector2 moveTo;
    GameObject titleCanvas;
    public float canWidth;
    public float canHeight;
    void Start()
    {
        titleCanvas = GameObject.FindGameObjectWithTag("titleCanvas");
        canWidth = titleCanvas.GetComponentInParent<RectTransform>().rect.width;
        canHeight = titleCanvas.GetComponentInParent<RectTransform>().rect.height;

        gameObject.transform.position = new Vector2(Random.Range(-20, (int)canWidth + 20), Random.Range(-20, (int)canHeight + 20)); 
        position = gameObject.transform.position;


        moveTo = new Vector2(Random.Range(-20, (int)canWidth + 20), Random.Range(-20, (int)canHeight + 20));
    }

    // Update is called once per frame
    void Update()
    {
        position = gameObject.transform.position;

        if (position == moveTo)
        {
            position = moveTo;
            moveTo = new Vector2(Random.Range(-20, (int)canWidth + 20), Random.Range(-20, (int)canHeight + 20));
            Debug.Log("Log");
        }
        
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, moveTo, 200 * Time.deltaTime);
    }
}
