using System.Timers;
using GameSaving;
using GameSaving.MonoBehaviours;
using TeamZ.Assets.Code.DependencyInjection;
using TeamZ.Assets.GameSaving.States;
using UniRx;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviourWithState<CharacterControllerState>
{
	public float RunSpeed;
	public float CreepSpeed;
	public float JumpForce;

	public Transform GroundCheck;
	public Transform ClimbCheck;

	public LayerMask WhatIsGround;
	public LayerMask WhatIsLevelObject;
	public LayerMask WhatIsEnemy;
	public LayerMask WhatIsSurfaceForClimbing;

	public Transform Punch;
	public float PunchRadius;

	public Transform Kick;
	public float KickRadius;

	public enum Direction
	{
		Empty,
		Left,
		Right,
		Up,
		Down
	}

	public enum FightMode
	{
		None = -1,
		Punch = 0,
		Kick,
		TailHit,
		HullHit
	}

	protected ReactiveProperty<bool> IsGrounded
		= new ReactiveProperty<bool>();

	protected ReactiveProperty<bool> CanClimb
		= new ReactiveProperty<bool>();

	protected ReactiveProperty<bool> IsClimbed
		= new ReactiveProperty<bool>();

	protected ReactiveProperty<Direction> HorizontalDirection
		= new ReactiveProperty<Direction>(Direction.Empty);

	protected ReactiveProperty<Direction> VerticalDirection
		= new ReactiveProperty<Direction>(Direction.Up);

	protected float GroundRadius = 0.15f;
	protected float ClimbRadius = 0.4f;

	protected Animator anim;
	protected Rigidbody2D rigidBody;

	protected ICharacter Character;

	protected FightMode fightMode = FightMode.None;

	private bool loadingStarted;

	private int[] activeLayersToInteraction = { 8, 9, 10 };

	private int impulseDirection = 1;

	private Timer strikeCooldownTimer = new Timer(600);
	private Timer jumpCooldownTimer = new Timer(600);

	protected ReactiveProperty<float> HorizontalValue
		= new ReactiveProperty<float>();

	protected ReactiveProperty<float> VerticalValue
		= new ReactiveProperty<float>();

	// Use this for initialization
	protected virtual void Start()
	{
		this.anim = this.GetComponent<Animator>();
		this.rigidBody = this.GetComponentInChildren<Rigidbody2D>();

		this.Character = this.GetComponent<Lizard>();

		this.strikeCooldownTimer.Elapsed += new ElapsedEventHandler(this.OnStrikeCooldownTimerEvent);
		this.jumpCooldownTimer.Elapsed += new ElapsedEventHandler(this.OnJumpCooldownTimerEvent);

		var prevHorizontalValue = 0f;
		this.HorizontalValue
			.Subscribe(value =>
			{
				if (value > 0)
				{
					this.HorizontalDirection.Value = Direction.Left;
				}

				if (value < 0)
				{
					this.HorizontalDirection.Value = Direction.Right;
				}

				var magnitude = Mathf.Abs(value);
				if (prevHorizontalValue == 0 && magnitude > 0)
				{
					MessageBroker.Default.Publish(new RunHappened(this.IsClimbed.Value));
				}

				if (prevHorizontalValue > 0 && magnitude == 0)
				{
					MessageBroker.Default.Publish(new RunEnded(this.IsClimbed.Value));
				}

				prevHorizontalValue = magnitude;
			})
			.AddTo(this);

		this.VerticalValue
			.Subscribe(o =>
			{
				if (o > 0)
				{
					this.VerticalDirection.Value = Direction.Up;
				}

				if (o < 0)
				{
					this.VerticalDirection.Value = Direction.Down;
				}
			})
			.AddTo(this);

		this.VerticalValue
			.Where(value => this.CanClimb.Value && !this.IsClimbed.Value && Mathf.Abs(value) > 0)
			.Subscribe(_ => this.IsClimbed.Value = true)
			.AddTo(this);

		this.HorizontalDirection
			.Subscribe(o => this.Flip())
			.AddTo(this);

		this.CanClimb
			.Where(canClimb => !canClimb)
			.Subscribe(_ => this.IsClimbed.Value = false)
			.AddTo(this);

		this.IsClimbed.Subscribe(isClimbed =>
		{
			this.anim.SetBool("Climbing", isClimbed);
			if (isClimbed)
			{
				this.rigidBody.gravityScale = 0.0f;
			}
			else
			{
				this.rigidBody.gravityScale = 1.0f;
			}
		})
		.AddTo(this);

		this.IsGrounded
			.Subscribe(o => this.anim.SetBool("Ground", o))
			.AddTo(this);
	}

	protected virtual void FixedUpdate()
	{
		this.HorizontalValue.Value = Input.GetAxis("Horizontal");
		this.VerticalValue.Value = Input.GetAxis("Vertical");

		this.IsGrounded.Value = Physics2D.OverlapCircle(this.GroundCheck.position, this.GroundRadius, this.WhatIsGround | this.WhatIsLevelObject | this.WhatIsEnemy) && !this.IsClimbed.Value;
		this.CanClimb.Value = Physics2D.OverlapCircle(this.ClimbCheck.position, this.ClimbRadius, this.WhatIsSurfaceForClimbing);

		if (this.IsClimbed.Value)
		{
			this.rigidBody.velocity = new Vector2(this.HorizontalValue.Value * this.CreepSpeed, this.VerticalValue.Value * this.CreepSpeed);
		}

		if (!this.IsClimbed.Value)
		{
			this.rigidBody.velocity = new Vector2(this.HorizontalValue.Value * this.RunSpeed, this.rigidBody.velocity.y);
		}

		this.anim.SetFloat("Speed", Mathf.Abs(this.HorizontalValue.Value));
		this.anim.SetFloat("ClimbSpeed", Mathf.Max(Mathf.Abs(this.HorizontalValue.Value), Mathf.Abs(this.VerticalValue.Value)));
	}

	// called once per frame
	protected virtual void Update()
	{
		if (Input.GetKeyDown(KeyCode.Z) && !this.strikeCooldownTimer.Enabled)
		{
			this.fightMode = FightMode.Punch;
			this.anim.SetTrigger("Punch");
			this.strikeCooldownTimer.Start();
		}

		if (Input.GetKeyDown(KeyCode.X) && !this.strikeCooldownTimer.Enabled)
		{
			this.fightMode = FightMode.Kick;
			this.anim.SetTrigger("Kick");
			this.strikeCooldownTimer.Start();
		}

		if (this.IsGrounded.Value && Input.GetKeyDown(KeyCode.Space) && !this.jumpCooldownTimer.Enabled)
		{
			this.IsGrounded.Value = false;
			this.IsClimbed.Value = false;

			this.rigidBody.AddForce(new Vector2(0.0f, this.JumpForce));
			this.anim.SetFloat("JumpSpeed", this.rigidBody.velocity.y);

			this.jumpCooldownTimer.Start();
			MessageBroker.Default.Publish(new JumpHappened());
		}
	}

	protected virtual void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.GetComponent<FirstAidKit>() != null)
		{
			MessageBroker.Default.Publish(new TakeObjectHappened());
			// TODO: add effect of flying aid kit to health bar on HUD
			Destroy(col.gameObject);
		}

		if (col.gameObject.GetComponent<ArmorKit>() != null)
		{
			MessageBroker.Default.Publish(new TakeObjectHappened());
			// TODO: add effect of flying armor kit to armor bar on HUD
			Destroy(col.gameObject);
		}

		if (col.gameObject.GetComponent<AbyssCollider>() != null)
		{
			// Something strange happening with this OnTriggerEnter
			// It called OnTriggerEnter several times when it ought to only one
			if (this.loadingStarted)
			{
				return;
			}

			this.loadingStarted = true;

			var lastSave = Dependency<GameController>.Resolve().LoadLastSavedGameAsync();
		}
	}

	private void Flip()
	{
		var sign = Mathf.Sign(this.HorizontalValue.Value);
		Vector3 currentScale = this.transform.localScale;

		currentScale.x = sign * Mathf.Abs(currentScale.x);
		this.impulseDirection = (int)sign * Mathf.Abs(this.impulseDirection);
		this.transform.localScale = currentScale;
	}

	public void AlertObservers(string message)
	{
		if (message.Equals("AttackAnimationEnded"))
		{
			switch (this.fightMode)
			{
				case FightMode.Punch:
					Fight2D.Action(this.Punch.position, this.PunchRadius, this.activeLayersToInteraction, false, this.Character.PunchDamage, this.Character.PunchImpulse * this.impulseDirection);
					break;
				case FightMode.Kick:
					Fight2D.Action(this.Kick.position, this.KickRadius, this.activeLayersToInteraction, false, this.Character.KickDamage, this.Character.KickImpulse * this.impulseDirection);
					break;
				default:
					break;
			}

			this.fightMode = FightMode.None;
		}

		if (message.Equals("PunchHappened"))
		{
			MessageBroker.Default.Publish(new PunchHappened());
		}

		if (message.Equals("KickHappened"))
		{
			MessageBroker.Default.Publish(new KickHappened());
		}
	}

	private void OnStrikeCooldownTimerEvent(object sender, ElapsedEventArgs e)
	{
		this.strikeCooldownTimer.Stop();
	}

	private void OnJumpCooldownTimerEvent(object sender, ElapsedEventArgs e)
	{
		this.jumpCooldownTimer.Stop();
	}

	public override CharacterControllerState GetState()
		=> new CharacterControllerState
		{
			IsClimbed = this.IsClimbed.Value,
		};

	public override void SetState(CharacterControllerState state)
	{
		this.HorizontalDirection.Value = Direction.Empty;
		this.IsClimbed.Value = state.IsClimbed;
	}
}

public class RunHappened
{
	public bool isClimbing = false;

	public RunHappened(bool _isClimbing)
	{
		this.isClimbing = _isClimbing;
	}
}

public class RunEnded
{
	public bool isClimbing = false;

	public RunEnded(bool _isClimbing)
	{
		this.isClimbing = _isClimbing;
	}
}

public class JumpHappened
{
	public JumpHappened()
	{
	}
}

public class PunchHappened
{
	public PunchHappened()
	{
	}
}

public class KickHappened
{
	public KickHappened()
	{
	}
}

public class TakeObjectHappened
{
	public TakeObjectHappened()
	{
	}
}