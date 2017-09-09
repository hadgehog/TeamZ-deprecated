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
        var projectName = Directory.GetFiles(".", "*.csproj").Min();

        Process.Start(@"Assets\GameSaving\Nugets\ZeroFormatter.Interfaces.1.6.4\tools\zfc.exe", $@"-i ""{ projectName }"" -o ""Assets\ZeroFormatter.g.cs"" ");
    }
}
