using System.Collections;
using System.Collections.Generic;
using GameSaving.States.Charaters;
using UnityEngine;

public class Lizard : Character<LizardState>
{
    // TODO: add specific Lizard properties and behavior


    public override LizardState GetState()
    {
        return new LizardState
        {
            Armor = this.Armor,
            Damage = this.Damage,
            Health = this.Health
        };
    }

    public override void SetState(LizardState state)
    {
        base.SetState(state);
    }
}
