using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
	public float RunSpeed;
	public float CreepSpeed;
	public float JumpForce;

	public Transform GroundCheck;

	public LayerMask WhatIsGround;
	public LayerMask WhatIsLevelObject;
	public LayerMask WhatIsEnemy;

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
	protected float GroundRadius = 0.15f;

	protected Animator anim;
	protected Rigidbody2D rigidBody;
	protected Direction currentDirection = Direction.Right;

	protected ICharacter Character;

	protected FightMode fightMode = FightMode.None;

	private bool loadingStarted;

	int[] activeLayersToInteraction = { 8, 9, 10 };

	public void AlertObservers(string message)
	{
		switch (this.fightMode)
		{
			case FightMode.Punch:
				Fight2D.Action(Punch.position, PunchRadius, this.activeLayersToInteraction, this.Character.PunchDamage, false);
				break;
			case FightMode.Kick:
				Fight2D.Action(Kick.position, KickRadius, this.activeLayersToInteraction, this.Character.KickDamage, false);
				break;
			default:
				break;
		}

		this.fightMode = FightMode.None;
	}

	// Use this for initialization
	protected virtual void Start()
	{
		this.anim = GetComponent<Animator>();
		this.rigidBody = GetComponent<Rigidbody2D>();

		this.Character = GetComponent<Lizard>();
	}

	protected virtual void FixedUpdate()
	{
		this.IsGrounded = Physics2D.OverlapCircle(this.GroundCheck.position, this.GroundRadius, (this.WhatIsGround | this.WhatIsLevelObject | this.WhatIsEnemy));

		this.anim.SetBool("Ground", this.IsGrounded);
		this.anim.SetFloat("JumpSpeed", this.rigidBody.velocity.y);

		float move = Input.GetAxis("Horizontal");

		this.anim.SetFloat("Speed", Mathf.Abs(move));

		this.rigidBody.velocity = new Vector2(move * this.RunSpeed, this.rigidBody.velocity.y);

		Direction tempDirection = this.currentDirection;

		if (move < 0)
			tempDirection = Direction.Left;
		else if (move > 0)
			tempDirection = Direction.Right;

		if (tempDirection != this.currentDirection)
		{
			this.currentDirection = tempDirection;
			this.Flip();
		}
	}

	// called once per frame
	protected virtual void Update()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			this.fightMode = FightMode.Punch;
			this.anim.SetTrigger("Punch");
		}

		if (Input.GetKeyDown(KeyCode.X))
		{
			this.fightMode = FightMode.Kick;
			this.anim.SetTrigger("Kick");
		}

		if (this.IsGrounded && Input.GetKeyDown(KeyCode.Space))
		{
			this.anim.SetBool("Ground", false);
			this.rigidBody.AddForce(new Vector2(0.0f, this.JumpForce));
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

		this.transform.localScale = currentScale;
	}
}