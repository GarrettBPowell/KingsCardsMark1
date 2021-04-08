using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getCardData : MonoBehaviour
{
    public Card card;
    public Image cardSprite;
    public Text Description;
    public Text Name;
    // Start is called before the first frame update
    void Start()
    {
        cardSprite.sprite = card.cardSprites[0];
        Description.text = card.description;
        Name.text = card.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
