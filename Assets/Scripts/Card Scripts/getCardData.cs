using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getCardData : MonoBehaviour
{
    public Card card;
    public Image cardSpriteUI;
    public Text Description;
    public Text Name;
    // Start is called before the first frame update
    void Start()
    { 
        Description.text = card.description;
        Name.text = card.name;
    }

    // Update is called once per frame
    void Update()
    {
        if(cardSpriteUI.sprite == null)
            cardSpriteUI.sprite = card.cardSprite;
        if (Description.text != card.description)
        {
            cardSpriteUI.sprite = card.cardSprite;
            Description.text = card.description;
        }
    }
}
