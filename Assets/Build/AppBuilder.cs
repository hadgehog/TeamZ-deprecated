using UnityEditor;

namespace Build
{
#if UNITY_EDITOR
    public class AppBuilder
    {
        public static void BuildGame()
        {
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
