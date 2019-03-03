using System;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Build
{
#if UNITY_EDITOR
	using UnityEditor;
	using UnityEditor.AddressableAssets;

	public class AppBuilder
    {
        public static void BuildGame()
        {
			var settings = AddressableAssetSettingsDefaultObject.Settings;
			if (Directory.Exists(Addressables.BuildPath))
			{
				try
				{
					Directory.Delete(Addressables.BuildPath, true);
				}
				catch (Exception e)
				{
					Debug.Log(e);
				}
			}

			var buildContext = new AddressablesBuildDataBuilderContext(settings,
				BuildPipeline.GetBuildTargetGroup(BuildTarget.StandaloneWindows64),
				BuildTarget.StandaloneWindows64, EditorUserBuildSettings.development,
				false, "1");

			settings.ActivePlayerDataBuilder.BuildData<AddressablesPlayerBuildResult>(buildContext);

			var report = BuildPipeline.BuildPlayer(new[]
                {
                    "Assets/Levels/Core/Core.unity",
                    "Assets/Levels/Laboratory/Laboratory.unity",
                    "Assets/Levels/Laboratory/Laboratory2.unity",
                },
                "./BuildArtifacts/TeamZ.exe",
                BuildTarget.StandaloneWindows64,
                BuildOptions.None);
        }
    }
#endif
}
