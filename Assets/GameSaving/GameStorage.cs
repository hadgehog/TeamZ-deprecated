using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using ZeroFormatter;

namespace GameSaving
{
    public class GameStorage<TGameState>
    {
        private const string SaveDirectory = "/Saves/";

        private readonly string path;
        private readonly List<string> slots;

        public IEnumerable<string> Slots
        {
            get
            {
                return this.slots;
            }
        }

        public GameStorage()
        {
            this.slots = new List<string>();

            this.path = Application.persistentDataPath + SaveDirectory;

            if (!Directory.Exists(this.path))
                Directory.CreateDirectory(this.path);
        }

        public async Task<TGameState> LoadAsync(string slotName)
        {
            using (var reader = new FileStream(this.path + slotName + ".save", FileMode.Open))
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
        }
    }
}