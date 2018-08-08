using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameObjects.Levels;
using UnityEngine;

public class Level
{
    static Level()
    {
        Laboratory = new Level
        {
            Name = "Laboratory",
            Scene = "Laboratory",
            Id = Guid.Parse("F2B428DA-4C1E-4761-83F8-A6998F08B72F")
        };

        Laboratory2 = new Level
        {
            Name = "Laboratory2",
            Scene = "Laboratory2",
            Id = Guid.Parse("F19EE6AB-A9D2-4AF1-98EA-BD1D09CDE6E2")
        };

        Core = new Level
        {
            Name = "Core",
            Scene = "Core",
            Id = Guid.Parse("F09EE6AB-A9D2-4AF1-98EA-BD1D09CDE6E2")
        };

        All = new[]
        {
            Core,
            Laboratory,
            Laboratory2
        }.ToDictionary(o => o.Name);
    }

    public string Name;
    public string Scene;
    public Guid Id;

    public static Level Core { get; }
    public static Level Laboratory { get; }
    public static Level Laboratory2 { get; }
    public static Dictionary<string, Level> All { get; }
}
