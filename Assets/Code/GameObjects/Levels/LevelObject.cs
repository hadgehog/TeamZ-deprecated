using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour, IDamageable
{
	public int Weight
	{
		get { return this.weight; }
		set { this.weight = value; }
	}

	public int Strength
	{
		get { return this.strength; }
		set { this.strength = value; }
	}

    public bool IsDestructible
	{
		get { return this.isDestructible; }
		set { this.isDestructible = value; }
	}

	[SerializeField]
	private int weight = 100;

	[SerializeField]
	private int strength = 100;

	[SerializeField]
	private bool isDestructible = false;

	private float hitImpuls = 0.5f;

	public void TakeDamage(int damage)
	{
		if (this.IsDestructible)
		{
			this.Strength -= damage;

			if (this.Strength <= 0)
			{
				this.Strength = 0;

				Debug.Log("LevelObject is destroyed!");
			}

			Debug.Log("LevelObject's HP = " + this.Strength);
		}
		else
		{
			this.TakeImpuls(this.hitImpuls * damage);
		}
	}

	public void TakeImpuls(float value)
	{
		// TODO: add logic

		Debug.Log("LevelObject take an impuls = " + value);
	}

	protected virtual void FixedUpdate()
	{
		// TODO: add logic

		if (this.Strength <= 0)
		{
			return;
		}
	}
}
