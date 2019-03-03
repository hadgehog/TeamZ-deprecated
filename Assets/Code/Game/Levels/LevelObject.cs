using System;
using GameSaving.MonoBehaviours;
using TeamZ.Assets.Code.Game.Tips;
using TeamZ.Assets.GameSaving.States;
using UniRx;
using UnityEngine;

public class LevelObject : MonoBehaviourWithState<LevelObjectState>, IDamageable
{
	[Serializable]
	public struct VisualState
	{
		public int hp;
		public Sprite texture;
	}

	public IntReactiveProperty Strength = new IntReactiveProperty(100);

	public bool IsDestructible
	{
		get { return this.isDestructible; }
		set { this.isDestructible = value; }
	}

	public VisualState[] VisualStates;

	protected SpriteRenderer Renderer2D;


	[SerializeField]
	private bool isDestructible = false;

	protected virtual void Start()
	{
		this.Renderer2D = this.GetComponent<SpriteRenderer>();

		if (this.Renderer2D != null && this.VisualStates.Length > 0)
		{
			this.Renderer2D.sprite = this.VisualStates[0].texture;
		}

		this.Strength.Subscribe(value =>
		{
			this.SwitchVisualState();
			if (value <= 0)
			{
				this.StrengthTooLow();
			}
		});
	}

	public void TakeDamage(int damage, int impulse)
	{
		if (this.IsDestructible)
		{
			this.Strength.Value = Mathf.Max(this.Strength.Value - damage, 0);
		}
		else
		{
			this.TakeImpuls(impulse * damage);
		}
	}

	public virtual void StrengthTooLow()
	{
		if (this.GetComponent<ConstantMessageTip>() != null)
		{
			Destroy(this.GetComponent<ConstantMessageTip>());
		}

		Destroy(this.GetComponent<BoxCollider2D>());
	}

	private void TakeImpuls(float impulse)
	{
		var rigidBody = this.GetComponent<Rigidbody2D>();

		if (rigidBody != null)
		{
			rigidBody.AddForce(new Vector2(impulse, Math.Abs(impulse / 2.0f)));
		}
	}

	private void SwitchVisualState()
	{
		foreach (var state in this.VisualStates)
		{
			if (this.Strength.Value >= state.hp)
			{
				this.Renderer2D.sprite = state.texture;
				return;
			}
		}
	}

	public override LevelObjectState GetState()
	{
		return new LevelObjectState
		{
			IsDestructible = this.IsDestructible,
			Strength = this.Strength.Value
		};
	}

	public override void SetState(LevelObjectState state)
	{
		this.IsDestructible = state.IsDestructible;
		this.Strength.Value = state.Strength;
	}
}
