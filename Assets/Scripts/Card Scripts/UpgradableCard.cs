using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradableCard : Card
{

    public List<string> descriptions;

    public List<Sprite> cardSprites;

    public List<int> statusEffects;
    public List<int> damages;

    public int upgradeNum = 0;


    int hasBeenUpgraded = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(upgradeNum > hasBeenUpgraded)
        {
            description = descriptions[upgradeNum];
            statusEffect = statusEffects[upgradeNum];
            damage = damages[upgradeNum];
        }

        hasBeenUpgraded = upgradeNum;
    }
}
