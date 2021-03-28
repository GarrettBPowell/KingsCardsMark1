using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mapToggle : MonoBehaviour
{
    public RawImage minimap;
    public bool isVisible;
    public void toggleMap()
    {
        isVisible = !isVisible;

        if (isVisible)
            minimap.enabled = true;
        else
            minimap.enabled = false;
        
    }
}
