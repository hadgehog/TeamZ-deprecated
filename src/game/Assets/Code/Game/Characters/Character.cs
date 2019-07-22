using GameSaving.MonoBehaviours;
using GameSaving.States.Charaters;
using TeamZ.Mediator;
using UniRx;
using UnityEngine;
using static CharacterControllerScript;

public interface ICharacter
{
    string Name { get; set; }

    int Health { get; set; }
    int Armor { get; set; }
    int PunchDamage { get; set; }
	int KickDamage { get; set; }
	int PunchImpulse { get; }
	int KickImpulse { get; }

    int MakeDamage(FightMode fightMode);
    void TakeDamage(int damage);
    void TakeHealth(int health);
    void TakeArmor(int armor);
    void ApplyMutagen(int duration);
}


public abstract class Character<TState> : MonoBehaviourWithState<TState>, ICharacter
    where TState : CharacterState
{
    [SerializeField]
    private string characterName;

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

    [SerializeField]
    private int mutagenDuration;    // in minutes

    public string Name
    {
        get { return this.characterName; }
        set { this.characterName = value; }
    }

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

    public int MutagenDuration
    {
        get { return this.mutagenDuration; }
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


    public int MakeDamage(FightMode fightMode)
    {
        if (fightMode == FightMode.Punch)
            return this.PunchDamage;
        else
            return this.KickDamage;
    }

    public void TakeDamage(int value)
    {
        if (this.Health == 0)
        {
            return;
        }

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

            MessageBroker.Default.Publish(new CharacterDead(this));
            Debug.Log("You are die!");
        }
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

    public virtual void ApplyMutagen(int duration)
    {
        this.mutagenDuration = duration;
    }
}

public class CharacterDead : ICommand
{
	private ICharacter character;

	public CharacterDead(ICharacter character)
	{
		this.character = character;
	}
}