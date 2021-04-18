using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardAcrossScreen : MonoBehaviour
{
    public List<Sprite> cardSprites;
    public Vector2 position;
    public Vector2 moveTo;
    GameObject titleCanvas;
    public float canWidth;
    public float canHeight;

    int randNum;
    void Start()
    {
        randNum = Random.Range(0, cardSprites.Count);
        GetComponent<Image>().sprite = cardSprites[randNum];
        titleCanvas = GameObject.FindGameObjectWithTag("titleCanvas");
        canWidth = titleCanvas.GetComponentInParent<RectTransform>().rect.width;
        canHeight = titleCanvas.GetComponentInParent<RectTransform>().rect.height;

        gameObject.transform.position = new Vector2(Random.Range(-20, (int)canWidth + 20), Random.Range(-20, (int)canHeight + 20)); 
        position = gameObject.transform.position;


        moveTo = getRandVector();
    }

    // Update is called once per frame
    void Update()
    {
        position = gameObject.transform.position;

        if (position == moveTo)
        {
            
            getRandNum(randNum);

            gameObject.GetComponent<Image>().sprite = cardSprites[randNum];
            position = moveTo;
            moveTo = getRandVector(); 
            //new Vector2(Random.Range(-20, (int)canWidth + 20), Random.Range(-20, (int)canHeight + 20));
        }
        
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, moveTo, 200 * Time.deltaTime);
    }

    public void getRandNum(int r)
    {
        while(randNum == r)
        {
            randNum = Random.Range(0, cardSprites.Count);
        }
    }

    public Vector2 getRandVector()
    {
        Vector2 vec;

        switch(Random.Range(0,4))
        {
            case 0:
                vec = new Vector2((int)canWidth - 60, Random.Range(0, (int)canHeight - 85));
                break;
            case 1:
                vec = new Vector2(0, Random.Range(60, (int)canHeight - 85));
                break;
            case 2:
                vec = new Vector2(Random.Range(0,(int)canWidth - 60), (int)canHeight - 85);
                break;
            case 3:
                vec = new Vector2(Random.Range(0, (int)canWidth - 60), 85);
                break;
            default:
                vec = new Vector2(0, 0);
                break;
        }

        return vec;
    }
}
