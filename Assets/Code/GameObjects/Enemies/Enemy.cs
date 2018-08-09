using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP = 100;
    public int Damage = 10;

	public void TakeDamage(int value)
	{
		this.Damage = value;
	}

	public int MakeDamage()
	{
		return this.Damage;
	}
}