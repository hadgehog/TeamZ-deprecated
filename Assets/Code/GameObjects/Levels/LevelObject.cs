using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
	public int Weight = 100;
	public int Strength = 100;
    public bool IsDestructible = false;

	private float hitImpuls = 0.5f;

	public void TakeDamage(int value)
	{
		this.Strength -= value;

		if (this.Strength <= 0)
		{
			this.Strength = 0;
		}

		Debug.Log("LevelObject's HP = " + this.Strength);
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
			Debug.Log("LevelObject is destroyed!");
		}
	}
}
