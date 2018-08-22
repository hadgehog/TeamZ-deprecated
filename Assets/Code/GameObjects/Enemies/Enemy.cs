using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public int HP
	{
		get { return this.hp; }
		set { this.hp = value; }
	}

	public int Armor
	{
		get { return this.armor; }
		set { this.armor = value; }
	}

    public int Damage
	{
		get { return this.damage; }
		set { this.damage = value; }
	}

	[SerializeField]
	private int hp = 100;

	[SerializeField]
	private int armor = 0;

	[SerializeField]
	private int damage = 10;

	public void TakeDamage(int damage, int impulse)
	{
		int blockedDamage = this.Armor - damage;

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

			Debug.Log("Enemy is die!");
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
			return;
		}
	}
}