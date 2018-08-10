using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP = 100;
	public int Armor = 0;
    public int Damage = 10;

	public void TakeDamage(int value)
	{
		int blockedDamage = this.Armor - value;

		if (blockedDamage >= 0)
		{
			this.Armor = blockedDamage;
		}
		else
		{
			this.Armor = 0;
			this.HP += blockedDamage;
		}

		if (this.HP <= 0)
		{
			this.HP = 0;
		}

		Debug.Log("Enemy's HP = " + this.HP);
	}

	public int MakeDamage()
	{
		return this.Damage;
	}

	protected virtual void FixedUpdate()
	{
		// TODO: add logic

		if(this.HP <= 0)
		{
			Debug.Log("Enemy is die!");
		}
	}
}