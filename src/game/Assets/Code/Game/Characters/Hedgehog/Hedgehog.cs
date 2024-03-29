﻿using GameSaving.States.Charaters;

public class Hedgehog : Character<HedgehogState>
{
    // TODO: add specific Hedgehog property (ACCELERATION)

    public override HedgehogState GetState()
    {
        return new HedgehogState
        {
            Armor = this.Armor,
            PunchDamage = this.PunchDamage,
            KickDamage = this.KickDamage,
            Health = this.Health,
            Name = this.Name
        };
    }

    public override void SetState(HedgehogState state)
    {
        base.SetState(state);
    }

    public override void ApplyMutagen(int duration)
    {
        base.ApplyMutagen(duration);
    }
}
