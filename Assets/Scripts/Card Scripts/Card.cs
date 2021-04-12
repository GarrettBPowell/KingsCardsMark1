using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{

	public new string name;

	public string cardType;
	public string cardArea;

	[TextArea(1, 3)]
	public string description;

	public Sprite cardSprite;

	public int statusEffect;
	public int damage;



	
	public void Print()
	{
		Debug.Log(name + ": " + description + " The card costs: " + damage);
	}

	public void attack(Enemy enemy)
	{
		enemy.enemyHealth -= damage;
	}

}
