﻿using System;
using System.Linq;
using Game.Activation.Core;
using GameSaving;
using GameSaving.MonoBehaviours;
using TeamZ.Assets.Code.DependencyInjection;
using TeamZ.Assets.Code.Game.Characters;
using TeamZ.Assets.Code.Game.UserInput;
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
    public LayerMask WhatIsCharacter;

    public Transform Punch;
    public float PunchRadius;

    public Transform Kick;
    public float KickRadius;

    public ReactiveProperty<IUserInputProvider> UserInputProvider
        = new ReactiveProperty<IUserInputProvider>();

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

    protected float GroundRadius = 0.2f;
    protected float ClimbRadius = 0.4f;

    protected Animator anim;
    protected Rigidbody2D rigidBody;

    protected ICharacter Character;

    protected FightMode fightMode = FightMode.None;

    private bool loadingStarted;

    private int[] activeLayersToInteraction = { 8, 9, 10, 13, 14 };     // ground, level object, enemy, characters

    private int impulseDirection = 1;

    protected ReactiveProperty<float> HorizontalValue
        = new ReactiveProperty<float>();

    protected ReactiveProperty<float> VerticalValue
        = new ReactiveProperty<float>();

    protected ClimbingSurface climbingSurface = null;
    private IDisposable climbingMovement;

    private bool climbingSurfaceOnLeftIsMissing;
    private bool climbingSurfaceOnRightIsMissing;
    private bool climbingSurfaceOnTopIsMissing;
    private bool climbingSurfaceOnBottomIsMissing;

    private bool isTopOnStairs;
    private bool isBottomNotOnStairs;

    // Use this for initialization
    protected virtual void Start()
    {
        this.anim = this.GetComponent<Animator>();
        this.rigidBody = this.GetComponentInChildren<Rigidbody2D>();

        var prevHorizontalValue = 0f;

        this.UserInputProvider
            .Where(o => o != null)
            .Subscribe(userInputProvider =>
            {
                userInputProvider.Horizontal
                   .Subscribe(o => this.HorizontalValue.Value = o)
                   .AddTo(this);

                userInputProvider.Vertical
                    .Subscribe(o => this.VerticalValue.Value = o)
                    .AddTo(this);

                userInputProvider.Punch
                    .True()
                    .ThrottleFirst(TimeSpan.FromSeconds(0.6))
                    .Subscribe(o =>
                    {
                        this.fightMode = FightMode.Punch;
                        this.anim.SetTrigger("Punch");
                    })
                    .AddTo(this);

                userInputProvider.Kick
                    .True()
                    .ThrottleFirst(TimeSpan.FromSeconds(0.6))
                    .Subscribe(o =>
                    {
                        this.fightMode = FightMode.Kick;
                        this.anim.SetTrigger("Kick");
                    })
                    .AddTo(this);

                userInputProvider.Activate
                    .True()
                    .Subscribe(activate =>
                    {
                        var hits = Physics.RaycastAll(this.transform.position - Vector3.forward, Vector3.forward);
                        var firstActivable = hits
                            .Select(o => o.collider.gameObject.GetComponent<IActivable>())
                            .Where(o => o != null).FirstOrDefault();

                        firstActivable?.Activate();
                    })
                    .AddTo(this);

                userInputProvider.Jump
                    .True()
                    .Where(o => this.IsGrounded.Value || this.IsClimbed.Value)
                    .Subscribe(o =>
                    {
                        this.rigidBody.gravityScale = 1.0f;

                        if (this.IsGrounded.Value)
                        {
                            this.rigidBody.AddForce(new Vector2(0.0f, this.JumpForce));
                        }

                        MessageBroker.Default.Publish(new RunEnded(this.IsClimbed.Value));
                        MessageBroker.Default.Publish(new JumpHappened());

                        this.IsGrounded.Value = this.IsClimbed.Value = false;
                    })
                    .AddTo(this);
            })
            .AddTo(this);


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

                if ((this.IsGrounded.Value || this.IsClimbed.Value) && prevHorizontalValue == 0 && magnitude > 0)
                {
                    MessageBroker.Default.Publish(new RunHappened(this.IsClimbed.Value));
                }

                if ((this.IsGrounded.Value || this.IsClimbed.Value) && prevHorizontalValue > 0 && magnitude == 0)
                {
                    MessageBroker.Default.Publish(new RunEnded(this.IsClimbed.Value));
                }

                prevHorizontalValue = magnitude;
            })
            .AddTo(this);

        var prevVerticalValue = 0f;

        this.VerticalValue
            .Subscribe(value =>
            {
                if (value > 0)
                {
                    this.VerticalDirection.Value = Direction.Up;
                }

                if (value < 0)
                {
                    this.VerticalDirection.Value = Direction.Down;
                }

                var magnitude = Mathf.Abs(value);

                if (this.IsClimbed.Value && prevVerticalValue == 0 && magnitude > 0)
                {
                    MessageBroker.Default.Publish(new RunHappened(this.IsClimbed.Value));
                }

                if (this.IsClimbed.Value && prevVerticalValue > 0 && magnitude == 0)
                {
                    MessageBroker.Default.Publish(new RunEnded(this.IsClimbed.Value));
                }

                prevVerticalValue = magnitude;
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
            this.anim.SetBool("Climbing", isClimbed && this.climbingSurface);

            if (isClimbed && this.climbingSurface)
            {
                this.rigidBody.gravityScale = 0.0f;
            }
            else
            {
                this.rigidBody.gravityScale = 1.0f;
            }

            if (isClimbed && this.climbingSurface && (this.VerticalValue.Value > 0.0f || this.HorizontalValue.Value > 0.0f))
            {
                MessageBroker.Default.Publish(new RunHappened(this.IsClimbed.Value));
            }
            else
            {
                MessageBroker.Default.Publish(new RunEnded(this.IsClimbed.Value));
            }
        })
        .AddTo(this);

        this.IsClimbed.Subscribe(isClimbing =>
        {
            if (isClimbing && this.climbingSurface)
            {
                if (this.climbingSurface.Type == ClimbingSurface.ClimbingSurfaceType.Stairway)
                {
                    this.AlignCharacter();
                }

                this.climbingMovement = Observable
                    .EveryUpdate()
                    .Subscribe(_ => { this.CheckClimbingSurfaceBorders(); this.CheckGroundUnderTheStairs(); })
                    .AddTo(this);
            }
            else
            {
                this.climbingMovement?.Dispose();
            }
        })
        .AddTo(this);

        this.IsGrounded.Subscribe(value =>
        {
            this.anim.SetBool("Ground", value);

            if (!value)
            {
                MessageBroker.Default.Publish(new RunEnded(false));
            }

            if (value && Mathf.Abs(this.HorizontalValue.Value) > 0.0f)
            {
                MessageBroker.Default.Publish(new RunHappened(false));
            }
        })
        .AddTo(this);
    }

    protected virtual void FixedUpdate()
    {
        var hit = Physics2D.Raycast(this.transform.position - Vector3.forward * 2, Vector3.forward, 6.0f, this.WhatIsSurfaceForClimbing);

        if (hit.collider)
        {
            this.climbingSurface = hit.collider.GetComponent<ClimbingSurface>();
        }
        else
        {
            this.climbingSurface = null;
        }

        this.IsGrounded.Value = Physics2D.OverlapCircle(this.GroundCheck.position, this.GroundRadius, this.WhatIsGround | this.WhatIsLevelObject | this.WhatIsEnemy | this.WhatIsCharacter) && !this.IsClimbed.Value;
        this.CanClimb.Value = Physics2D.OverlapCircle(this.ClimbCheck.position, this.ClimbRadius, this.WhatIsSurfaceForClimbing) && this.climbingSurface;

        if (this.IsClimbed.Value && this.climbingSurface)
        {
            var horizontalValue = this.HorizontalValue.Value * this.CreepSpeed;
            var verticalValue = this.VerticalValue.Value * this.CreepSpeed;

            if (this.climbingSurfaceOnRightIsMissing && horizontalValue > 0)
            {
                horizontalValue = 0;
            }

            if (this.climbingSurfaceOnLeftIsMissing && horizontalValue < 0)
            {
                horizontalValue = 0;
            }

            if (this.climbingSurfaceOnTopIsMissing && verticalValue > 0)
            {
                verticalValue = 0;
            }

            if (this.climbingSurfaceOnBottomIsMissing && verticalValue < 0)
            {
                verticalValue = 0;
            }

            if (verticalValue < 0 && this.isTopOnStairs && this.isBottomNotOnStairs)
            {
                this.IsGrounded.Value = true;
                this.IsClimbed.Value = false;
                this.rigidBody.gravityScale = 1.0f;
            }
            else
            {
                this.rigidBody.velocity = new Vector2(horizontalValue, verticalValue);
            }
        }

        if (!this.IsClimbed.Value)
        {
            this.rigidBody.velocity = new Vector2(this.HorizontalValue.Value * this.RunSpeed, this.rigidBody.velocity.y);
        }

        this.anim.SetFloat("Speed", Mathf.Abs(this.HorizontalValue.Value));

        if (this.IsClimbed.Value && this.climbingSurface)
        {
            this.anim.SetFloat("ClimbSpeed", Mathf.Max(Mathf.Abs(this.HorizontalValue.Value), Mathf.Abs(this.VerticalValue.Value)));
        }

        this.anim.SetFloat("JumpSpeed", this.rigidBody.velocity.y);
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

        if (col.gameObject.GetComponent<MutagenCapsule>() != null)
        {
            MessageBroker.Default.Publish(new TakeObjectHappened());
            // TODO: add effect of flying mutagen capsule to mutagen bar on HUD
            // start mutagen timer
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

    public override CharacterControllerState GetState()
        => new CharacterControllerState
        {
            IsClimbed = this.IsClimbed.Value,
        };

    public override void SetState(CharacterControllerState state)
    {
        this.HorizontalDirection.Value = Direction.Empty;
        this.IsClimbed.Value = state.IsClimbed;
        this.CanClimb.Value = state.IsClimbed;
    }

    protected virtual void CheckClimbingSurfaceBorders()
    {
        var localScale = this.transform.localScale;
        var characterSizeX = Math.Abs(localScale.x);
        var characterSizeY = Math.Abs(localScale.y);

        var hitLeft = Physics2D.Raycast(this.transform.position - Vector3.forward * 2 - new Vector3(characterSizeX / 2, 0, 0), Vector3.forward, 6.0f, this.WhatIsSurfaceForClimbing);
        var hitRight = Physics2D.Raycast(this.transform.position - Vector3.forward * 2 + new Vector3(characterSizeX / 2, 0, 0), Vector3.forward, 6.0f, this.WhatIsSurfaceForClimbing);
        var hitTop = Physics2D.Raycast(this.transform.position - Vector3.forward * 2 + new Vector3(0, characterSizeY / 0.8f, 0), Vector3.forward, 6.0f, this.WhatIsSurfaceForClimbing);
        var hitBottom = Physics2D.Raycast(this.transform.position - Vector3.forward * 2 - new Vector3(0, characterSizeY / 1.5f, 0), Vector3.forward, 6.0f, this.WhatIsSurfaceForClimbing);

        this.climbingSurfaceOnLeftIsMissing = hitLeft.collider == null;
        this.climbingSurfaceOnRightIsMissing = hitRight.collider == null;
        this.climbingSurfaceOnTopIsMissing = hitTop.collider == null;
        this.climbingSurfaceOnBottomIsMissing = hitBottom.collider == null;

        if (this.Character.Name.Equals(Characters.Hedgehog.Name))
        {
            hitTop = Physics2D.Raycast(this.transform.position - Vector3.forward * 2 + new Vector3(0, characterSizeY / 1.5f, 0), Vector3.forward, 6.0f, this.WhatIsSurfaceForClimbing);
            hitBottom = Physics2D.Raycast(this.transform.position - Vector3.forward * 2 - new Vector3(0, characterSizeY / 0.8f, 0), Vector3.forward, 6.0f, this.WhatIsSurfaceForClimbing);

            this.climbingSurfaceOnLeftIsMissing = hitLeft.collider == null || (hitLeft.collider != null && hitLeft.collider.GetComponent<ClimbingSurface>().Type == ClimbingSurface.ClimbingSurfaceType.Fence);
            this.climbingSurfaceOnRightIsMissing = hitRight.collider == null || (hitRight.collider != null && hitRight.collider.GetComponent<ClimbingSurface>().Type == ClimbingSurface.ClimbingSurfaceType.Fence);
            this.climbingSurfaceOnTopIsMissing = hitTop.collider == null || (hitTop.collider != null && hitTop.collider.GetComponent<ClimbingSurface>().Type == ClimbingSurface.ClimbingSurfaceType.Fence);
            this.climbingSurfaceOnBottomIsMissing = hitBottom.collider == null || (hitBottom.collider != null && hitBottom.collider.GetComponent<ClimbingSurface>().Type == ClimbingSurface.ClimbingSurfaceType.Fence);
        }
    }

    private void AlignCharacter()
    {
        var stairwayPos = this.climbingSurface.Position;
        this.rigidBody.position = new Vector2(stairwayPos.x, this.rigidBody.position.y);
    }

    protected virtual void CheckGroundUnderTheStairs()
    {
        var spriteSize = GetComponent<SpriteRenderer>().size;
        var localScale = this.transform.localScale;
        var characterSizeY = Math.Abs(localScale.y * spriteSize.y);

        var hitTop = Physics2D.Raycast(this.transform.position - Vector3.forward * 2 + new Vector3(0, characterSizeY / 2.0f, 0), Vector3.forward, 6.0f, this.WhatIsSurfaceForClimbing);
        // tune the magic constant if it will work bad
        var hitBottom = Physics2D.Raycast(this.transform.position - Vector3.forward * 2 - new Vector3(0, characterSizeY / 1.2f, 0), Vector3.forward, 6.0f, this.WhatIsSurfaceForClimbing);

        this.isTopOnStairs = hitTop.collider != null && hitTop.collider.GetComponent<ClimbingSurface>().Type == ClimbingSurface.ClimbingSurfaceType.Stairway;
        this.isBottomNotOnStairs = hitBottom.collider == null;
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