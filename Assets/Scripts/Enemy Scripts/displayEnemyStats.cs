using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayEnemyStats : MonoBehaviour
{
    public GameObject enemyHealth;

    private void Start()
    {
        enemyHealth = GameObject.FindGameObjectWithTag("displayEnemyStats");
    }
    private void OnMouseDown()
    {
        if (gameObject.CompareTag("enemy"))
        {
            enemyHealth.GetComponent<Text>().text = "Enemy Health: " + gameObject.GetComponent<Enemy>().enemyHealth;
            StartCoroutine(hideEnemyHealth());
        }
    }

    private IEnumerator hideEnemyHealth()
    {
        yield return new WaitForSeconds(2f);
        enemyHealth.GetComponent<Text>().text = "";
    }

    private void OnDestroy()
    {
        if (enemyHealth != null)
            enemyHealth.GetComponent<Text>().text = "";
    }
}
