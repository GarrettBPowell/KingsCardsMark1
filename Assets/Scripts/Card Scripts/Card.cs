using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{ 	
	public List<string> names;

	public string cardType;
	public string cardArea;

	[TextArea(1, 3)]
	public List<string> descriptions;

	public List<Sprite> cardSprites;

	public List<int> statusEffects;
	public List<int> damages;

	public string statusEffectName;

	public List<int> defenses;
	public List<int> heals;


	public bool upgradable;
	public int upgradeNum = 0;

	public bool canAttackEnemy;
	public void attack(GameManager gameManager, Enemy enemy)
	{
		int damageToDo = damages[0 + upgradeNum];

		if (gameManager.playerStatusEffects.ContainsKey("strength"))
			damageToDo += 2;
		if (gameManager.playerStatusEffects.ContainsKey("weak"))
			damageToDo = Mathf.CeilToInt(damageToDo * 0.75f);

		enemy.enemyHealth -= damageToDo;
	}

	public void heal(GameManager gameManager)
    {
		if (gameManager.playerMaxHealth >= gameManager.playerHealth + heals[0 + upgradeNum])
			gameManager.playerHealth += heals[0 + upgradeNum];
		else
			gameManager.playerHealth = gameManager.playerMaxHealth;
    }

	public void addEffect(GameManager gameManager)
    {
		if(gameManager.playerStatusEffects.ContainsKey(statusEffectName))
        {
			Debug.Log("first block");
			gameManager.playerStatusEffects[statusEffectName] += statusEffects[0 + upgradeNum];
        }
		else
        {
			Debug.Log("else block");
			gameManager.playerStatusEffects.Add(statusEffectName, statusEffects[0 + upgradeNum]);
        }
    }

	public void defend(GameManager gameManager)
    {
		gameManager.playerDefense += defenses[0 + upgradeNum];
    }
}
