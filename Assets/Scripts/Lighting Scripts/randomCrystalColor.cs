using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class randomCrystalColor : MonoBehaviour
{
    public List<Sprite> crystalSprites;
    public List<Color> crystalLightColors;
    public Light2D light;

    private void Awake()
    {
        int randCrystal = Random.Range(0, crystalSprites.Count);
        gameObject.GetComponent<SpriteRenderer>().sprite = crystalSprites[randCrystal];
        light.color = crystalLightColors[randCrystal];
    }
}
