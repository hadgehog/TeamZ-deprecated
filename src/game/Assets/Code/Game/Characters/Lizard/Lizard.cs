﻿using GameSaving.States.Charaters;

public class Lizard : Character<LizardState>
{
    // TODO: add specific Lizard properties and behavior

    public override LizardState GetState()
    {
        return new LizardState
        {
            Armor = this.Armor,
            PunchDamage = this.PunchDamage,
			KickDamage = this.KickDamage,
			Health = this.Health
        };
    }

    public override void SetState(LizardState state)
    {
        base.SetState(state);
    }
}
