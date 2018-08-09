using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using ZeroFormatter;

namespace GameSaving
{
	public class GameStorage<TGameState>
	{
		private const string SaveDirectory = "Saves";

		private readonly string path;
		private readonly HashSet<string> slots;

		public IEnumerable<string> Slots
		{
			get
			{
				return this.slots;
			}
		}

		public GameStorage()
		{
			this.path = Path.Combine(Application.persistentDataPath, SaveDirectory);

			if (!Directory.Exists(this.path))
				Directory.CreateDirectory(this.path);

			this.slots = new HashSet<string>(Directory.EnumerateFiles(this.path).Select(Path.GetFileNameWithoutExtension));
		}

		public async Task<TGameState> LoadAsync(string slotName)
		{
			using (var reader = new FileStream(Path.Combine(this.path, slotName + ".save"), FileMode.Open))
			{
				var bytes = new byte[reader.Seek(0, SeekOrigin.End)];
				reader.Seek(0, SeekOrigin.Begin);

				await reader.ReadAsync(bytes, 0, bytes.Length);

				var gameState = ZeroFormatterSerializer.Deserialize<TGameState>(bytes);

				return gameState;
			}
		}

		public async Task SaveAsync(TGameState game, string slotName)
		{
			var bytes = ZeroFormatterSerializer.Serialize(game);
			using (var writer = new FileStream(this.path + slotName + ".save", FileMode.Create))
			{
				await writer.WriteAsync(bytes, 0, bytes.Length);
			}

			if (!this.slots.Contains(slotName))
			{
				this.slots.Add(slotName);
			}
		}
	}
}