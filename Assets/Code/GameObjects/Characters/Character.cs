using GameSaving.MonoBehaviours;
using GameSaving.States.Charaters;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface ICharacter
{
    int Health { get; set; }
    int Armor { get; set; }
    int Damage { get; set; }

    void TakeArmor(int armor);
    int MakeDamage();
    void TakeDamage(int damage);
    void TakeHealth(int health);
}


public abstract class Character<TState> : MonoBehaviourWithState<TState>, ICharacter
    where TState : CharacterState
{
    public int Health
    {
        get;
        set;
    }

    public int Armor
    {
        get;
        set;
    }

    public int Damage
    {
        get;
        set;
    }

    public override void SetState(TState state)
    {
        this.Armor = state.Armor;
        this.Damage = state.Damage;
        this.Health = state.Health;
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

    public int MakeDamage()
    {
        return this.Damage;
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