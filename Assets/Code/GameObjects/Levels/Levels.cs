using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Level
{
    static Level()
    {
        Laboratory = new LevelInformation
        {
            Name = "Laboratory",
            Scene = "Laboratory",
            Id = Guid.Parse("F2B428DA-4C1E-4761-83F8-A6998F08B72F")
        };

        Laboratory2 = new LevelInformation
        {
            Name = "Laboratory2",
            Scene = "Laboratory2",
            Id = Guid.Parse("F09EE6AB-A9D2-4AF1-98EA-BD1D09CDE6E2")
        };

        All = new[]
        {
            Laboratory,
            Laboratory2
        };
    }

    public static LevelInformation Laboratory { get; }
    public static LevelInformation Laboratory2 { get; }
    public static LevelInformation[] All { get; }
}
