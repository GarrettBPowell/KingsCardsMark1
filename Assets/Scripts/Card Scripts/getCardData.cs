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

   // public int indexInGameManagerList;
    // Start is called before the first frame update
    void Start()
    { 
        Description.text = card.descriptions[0 + card.upgradeNum];
        Name.text = card.names[0 + card.upgradeNum];
    }

    // Update is called once per frame
    void Update()
    {
        if(cardSpriteUI.sprite == null)
            cardSpriteUI.sprite = card.cardSprites[0 + card.upgradeNum];
        if (Description.text != card.descriptions[0 + card.upgradeNum])
        {
            cardSpriteUI.sprite = card.cardSprites[0 + card.upgradeNum];
            Description.text = card.descriptions[0 + card.upgradeNum];
            Name.text = card.names[0 + card.upgradeNum];
        }
    }
}
