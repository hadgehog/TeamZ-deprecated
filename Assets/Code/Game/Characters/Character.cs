using GameSaving.MonoBehaviours;
using GameSaving.States.Charaters;
using UnityEngine;
using static CharacterControllerScript;

public interface ICharacter
{
    int Health { get; set; }
    int Armor { get; set; }
    int PunchDamage { get; set; }
	int KickDamage { get; set; }
	int PunchImpulse { get; }
	int KickImpulse { get; }

	void TakeArmor(int armor);
    int MakeDamage(FightMode fightMode);
    void TakeDamage(int damage);
    void TakeHealth(int health);
}


public abstract class Character<TState> : MonoBehaviourWithState<TState>, ICharacter
    where TState : CharacterState
{
    [SerializeField]
    private int health;

    [SerializeField]
    private int armor;

    [SerializeField]
    private int punchDamage;

	[SerializeField]
	private int kickDamage;

	[SerializeField]
	private int punchImpulse;

	[SerializeField]
	private int kickImpulse;

	public int Health
    {
        get { return this.health; }
        set { this.health = value; }
    }

    public int Armor
    {
        get { return this.armor; }
        set { this.armor = value; }
    }

    public int PunchDamage
    {
        get { return this.punchDamage; }
        set { this.punchDamage = value; }
    }

	public int KickDamage
	{
		get { return this.kickDamage; }
		set { this.kickDamage = value; }
	}

	public int PunchImpulse
	{
		get { return this.punchImpulse; }
	}

	public int KickImpulse
	{
		get { return this.kickImpulse; }
	}

	public override void SetState(TState state)
    {
        this.Armor = state.Armor;
        this.PunchDamage = state.PunchDamage;
		this.KickDamage = state.KickDamage;
		this.Health = state.Health;
    }

	// Use this for initialization
	protected virtual void Start()
	{
		this.punchImpulse = this.PunchDamage * 20;
		this.kickImpulse = this.KickDamage * 20;
	}


	public void TakeDamage(int value)
    {
        int blockedDamage = this.Armor - value;

        if (blockedDamage >= 0)
        {
            this.Armor = blockedDamage;
        }
        else
        {
            this.Armor = 0;
            this.Health += blockedDamage;
        }

        if (this.Health <= 0)
        {
            this.Health = 0;

            Debug.Log("You are die!");
        }
    }

    public int MakeDamage(FightMode fightMode)
    {
		if (fightMode == FightMode.Punch)
			return this.PunchDamage;
		else
			return this.KickDamage;
    }

    public void TakeHealth(int value)
    {
        this.Health += value;

        if (this.Health > 100)
        {
            this.Health = 100;
        }
    }

    public void TakeArmor(int value)
    {
        this.Armor += value;
    }
}