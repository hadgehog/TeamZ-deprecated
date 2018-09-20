using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour, IDamageable
{
	[Serializable]
	public struct VisualState
	{
		public int hp;
		public Sprite texture;
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

	public VisualState[] VisualStates;

	protected SpriteRenderer Renderer2D;

	[SerializeField]
	private int strength = 100;

	[SerializeField]
	private bool isDestructible = false;

	protected virtual void Start()
	{
		Debug.Log("LevelObject Start");

		this.Renderer2D = GetComponent<SpriteRenderer>();

		if (this.Renderer2D != null && this.VisualStates.Length > 0)
		{
			this.Renderer2D.sprite = this.VisualStates[0].texture;
		}
	}

	public void TakeDamage(int damage, int impulse)
	{
		if (this.IsDestructible)
		{
			this.Strength -= damage;

			if (this.Strength <= 0 && GetComponent<BoxCollider2D>() != null)
			{
				this.Strength = 0;

				Destroy(GetComponent<BoxCollider2D>());
				Debug.Log("LevelObject is destroyed!");
			}

			this.SwitchVisualState();
		}
		else
		{
			this.TakeImpuls(impulse * damage);
		}
	}

	protected virtual void FixedUpdate()
	{
		if (this.Strength <= 0)
		{
			return;
		}

		this.SwitchVisualState();
	}

	private void TakeImpuls(float impulse)
	{
		var rigidBody = GetComponent<Rigidbody2D>();

		if (rigidBody != null)
		{
			rigidBody.AddForce(new Vector2(impulse, Math.Abs(impulse / 2.0f)));

			Debug.Log("LevelObject TakeImpuls done " + impulse);
		}
	}

	private void SwitchVisualState()
	{
		foreach (var state in this.VisualStates)
		{
			if (this.Strength >= state.hp)
			{
				this.Renderer2D.sprite = state.texture;
				return;
			}
		}
	}
}
