using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CodeGeneratorController
{
    [MenuItem("Assets/Start Code Generation")]
    private static void StartCodeGeneration()
    {
        Process.Start(@"Assets\GameSaving\Nugets\ZeroFormatter.Interfaces.1.6.4\tools\zfc.exe", $@"-i ""Game.csproj"" -o ""Assets\ZeroFormatter.g.cs"" ");
    }
}
