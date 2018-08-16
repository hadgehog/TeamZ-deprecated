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

	public List<Sprite> VisualStates;
	protected SpriteRenderer Renderer2D;

	[SerializeField]
	private int weight = 100;

	[SerializeField]
	private int strength = 100;

	[SerializeField]
	private bool isDestructible = false;

	private float hitImpuls = 250.0f;

	// values for switching destroy states of the object
	private int FULL_HP;
	private int NORM_HP;
	private int LOW_HP;
	private int ZERO_HP;

	protected virtual void Start()
	{
		Debug.Log("LevelObject Start");

		this.FULL_HP = this.Strength;
		this.NORM_HP = this.Strength - (this.Strength / 4);
		this.LOW_HP = this.Strength - (this.Strength / 4) * 2;
		this.ZERO_HP = this.Strength - (this.Strength / 4) * 3;

		this.Renderer2D = GetComponent<SpriteRenderer>();

		if (this.Renderer2D != null && this.VisualStates.Count > 0)
		{
			this.Renderer2D.sprite = this.VisualStates[0];
		}
		else
		{
			Debug.Log("!!!!!!!!!!!  this.Renderer2D != null " + this.Renderer2D != null);
			Debug.Log("!!!!!!!!!!!  this.VisualStates.Count " + this.VisualStates.Count);
		}
	}

	public void TakeDamage(int damage)
	{
		if (this.IsDestructible)
		{
			this.Strength -= damage;

			this.SwitchVisualState();
		}
		else
		{
			this.TakeImpuls(this.hitImpuls * damage);
		}
	}

	protected virtual void FixedUpdate()
	{
		if (this.VisualStates.Count <= 0 || this.Strength <= 0)
		{
			return;
		}

		this.SwitchVisualState();
	}

	private void TakeImpuls(float value)
	{
		var rigidBody = GetComponent<Rigidbody2D>();

		if (rigidBody != null)
		{
			rigidBody.AddForce(new Vector2(value, hitImpuls * 30.0f));

			Debug.Log("LevelObject TakeImpuls done ");
		}
	}

	private void SwitchVisualState()
	{
		if (this.Strength <= 0)
		{
			this.Strength = 0;

			Debug.Log("LevelObject is destroyed!");

			Destroy(GetComponent<BoxCollider2D>().gameObject);
		}
		else if (this.Strength >= this.FULL_HP)
		{
			this.Renderer2D.sprite = this.VisualStates[0];
		}
		else if (this.Strength < this.FULL_HP && this.Strength >= this.NORM_HP)
		{
			this.Renderer2D.sprite = this.VisualStates[0];
		}
		else if (this.Strength < this.NORM_HP && this.Strength >= this.LOW_HP)
		{
			this.Renderer2D.sprite = this.VisualStates[1];
		}
		else if (this.Strength < this.LOW_HP && this.Strength >= this.ZERO_HP)
		{
			this.Renderer2D.sprite = this.VisualStates[2];
		}
		else if (this.Strength < this.ZERO_HP)
		{
			this.Renderer2D.sprite = this.VisualStates[3];
		}
	}
}
