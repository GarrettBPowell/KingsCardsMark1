using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingSeed : MonoBehaviour
{
    public string stringSeed = "base seed";
    public bool useStringSeed;
    public int seed;
    public bool randomizeSeed;

    private void Awake()
    {
        if(useStringSeed)
            seed = stringSeed.GetHashCode();
        

        if(randomizeSeed)
            seed = Random.Range(0, 99999);

        Random.InitState(seed);
    }
}
