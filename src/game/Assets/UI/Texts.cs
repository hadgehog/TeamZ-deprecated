namespace Assets.UI.Texts
{
	public class Texts
	{
		private const string LEVEL_1_STAGE_1 = "Level 1: Laboratory \n Stage 1: Initializing system";
		private const string LEVEL_1_STAGE_2 = "Level 1: Laboratory \n Stage 2: Through the catacombs";

		public static string GetLevelText(string levelName)
		{
			switch (levelName)
			{
				case Level.LABORATORY:
					return LEVEL_1_STAGE_1;
				case Level.LABORATORY2:
					return LEVEL_1_STAGE_2;
				default:
					return "Oh, something new! :)";
			}
		}
	}
}
