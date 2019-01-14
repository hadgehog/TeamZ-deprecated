using UnityEngine;
using System.Timers;

public class CharacterControllerScript : MonoBehaviour
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

	protected bool IsGrounded = true;
	protected bool IsClimbed = false;

	protected float GroundRadius = 0.15f;
	protected float ClimbRadius = 0.4f;

	protected Animator anim;
	protected Rigidbody2D rigidBody;

	protected Direction currentHorizontalDirection = Direction.Right;
	protected Direction currentVerticalDirection = Direction.Up;

	protected ICharacter Character;

	protected FightMode fightMode = FightMode.None;

	private bool loadingStarted;

	private int[] activeLayersToInteraction = { 8, 9, 10 };

	private int impulseDirection = 1;

	private Timer strikeCooldownTimer = new Timer(600);

	private bool isKeyUpWasPressed = false;


	// Use this for initialization
	protected virtual void Start()
	{
		this.anim = GetComponent<Animator>();
		this.rigidBody = GetComponent<Rigidbody2D>();

		this.Character = GetComponent<Lizard>();

		this.strikeCooldownTimer.Elapsed += new ElapsedEventHandler(OnStrikeCooldownTimerEvent);
	}

	protected virtual void FixedUpdate()
	{
		this.IsGrounded = Physics2D.OverlapCircle(this.GroundCheck.position, this.GroundRadius, (this.WhatIsGround | this.WhatIsLevelObject | this.WhatIsEnemy));
		this.IsClimbed = Physics2D.OverlapCircle(this.ClimbCheck.position, this.ClimbRadius, this.WhatIsSurfaceForClimbing);

        float horizontalMove = Input.GetAxis("Horizontal");
		Direction horizontalDirection = this.currentHorizontalDirection;

		if (horizontalMove < 0)
			horizontalDirection = Direction.Left;
		else if (horizontalMove > 0)
			horizontalDirection = Direction.Right;

		if (horizontalDirection != this.currentHorizontalDirection)
		{
			this.currentHorizontalDirection = horizontalDirection;
			this.Flip();
		}

        this.anim.SetBool("Ground", true);
        this.anim.SetFloat("Speed", 0.0f);
        this.anim.SetBool("Climbing", false);
        this.anim.SetFloat("ClimbSpeed", 0.0f);

        if (this.isKeyUpWasPressed && this.IsClimbed)
		{
			this.rigidBody.gravityScale = 0.0f;

			float verticalMove = Input.GetAxis("Vertical");
			Direction verticalDirection = this.currentVerticalDirection;

			if (verticalMove < 0)
				verticalDirection = Direction.Down;
			else if (verticalMove > 0)
				verticalDirection = Direction.Up;

			if (verticalDirection != this.currentVerticalDirection)
			{
				this.currentVerticalDirection = verticalDirection;
			}

            this.anim.SetBool("Climbing", this.IsClimbed);
            this.anim.SetFloat("ClimbSpeed", horizontalMove > 0.0f || horizontalMove < 0.0f ? Mathf.Abs(horizontalMove) : Mathf.Abs(verticalMove));
            this.rigidBody.velocity = new Vector2(horizontalMove * this.CreepSpeed, verticalMove * this.CreepSpeed);
        }
		else
		{
            this.isKeyUpWasPressed = false;

			if (this.rigidBody.gravityScale != 1)
			{
				Debug.Log("gravity reset to 1");
				this.rigidBody.gravityScale = 1.0f;
			}

			if (Direction.Down == this.currentVerticalDirection)
			{
				this.currentVerticalDirection = Direction.Up;
			}

            this.anim.SetBool("Ground", this.IsGrounded);
            this.anim.SetFloat("Speed", Mathf.Abs(horizontalMove));
            this.anim.SetFloat("JumpSpeed", this.rigidBody.velocity.y);
            this.rigidBody.velocity = new Vector2(horizontalMove * this.RunSpeed, this.rigidBody.velocity.y);
        }
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

		if (this.IsGrounded && Input.GetKeyDown(KeyCode.Space))
		{
			this.anim.SetBool("Ground", false);
			this.rigidBody.AddForce(new Vector2(0.0f, this.JumpForce));
		}

		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
		{
			this.isKeyUpWasPressed = true;
		}

        if (this.isKeyUpWasPressed && this.IsClimbed)
        {
            this.anim.SetBool("Climbing", this.IsClimbed);
        }
    }

	protected virtual void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.GetComponent<FirstAidKit>() != null)
		{
			// TODO: add effect of flying aid kit to health bar on HUD
			Destroy(col.gameObject);
		}

		if (col.gameObject.GetComponent<ArmorKit>() != null)
		{
			// TODO: add effect of flying armor kit to armor bar on HUD
			Destroy(col.gameObject);
		}

		if (col.gameObject.GetComponent<AbyssCollider>() != null)
		{
			// Something strange happening with this OnTriggerEnter
			// It called OnTriggerEnter several times when it ought to only one
			if (this.loadingStarted)
				return;

			this.loadingStarted = true;

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
			GameObject.FindObjectOfType<Main>().GameController.LoadAsync("test");
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
		}
	}

	private void Flip()
	{
		Vector3 currentScale = this.transform.localScale;

		currentScale.x *= -1;
		this.impulseDirection *= -1;

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
		}

		this.fightMode = FightMode.None;
	}

	private void OnStrikeCooldownTimerEvent(object sender, ElapsedEventArgs e)
	{
		this.strikeCooldownTimer.Stop();
	}
}