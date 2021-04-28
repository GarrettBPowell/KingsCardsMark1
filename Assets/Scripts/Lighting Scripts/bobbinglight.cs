using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bobbinglight : MonoBehaviour
{
    bool moveUpper = true;
    Vector3 upper;
    Vector3 lower;
    private void Start()
    {
        upper = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, 0);
        lower = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.2f, 0);
    }

    void Update()
    {
        if(moveUpper)
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, upper, Time.deltaTime * 0.1f);
        else
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, lower, Time.deltaTime * 0.1f);

        if (gameObject.transform.position == upper)
            moveUpper = !moveUpper;
        else if (gameObject.transform.position == lower)
            moveUpper = !moveUpper;
    }
}
