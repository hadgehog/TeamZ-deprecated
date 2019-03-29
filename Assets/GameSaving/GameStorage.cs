using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GameSaving.States;
using UniRx;
using UnityEngine;
using ZeroFormatter;

namespace GameSaving
{
	public class GameSlot
    {
        public DateTime Modified { get; set; }

        public string Name { get; set; }
    }

    public class GameStorage
	{
        private const string SaveDirectory = "Saves";

        private readonly string path;
        private readonly Dictionary<string, GameSlot> slots;

        public IEnumerable<GameSlot> Slots
        {
            get
            {
                return this.slots.Values;
            }
        }

        public GameStorage()
        {
            this.path = Path.Combine(Application.persistentDataPath, SaveDirectory);

            if (!Directory.Exists(this.path))
                Directory.CreateDirectory(this.path);

            this.slots = Directory.EnumerateFiles(this.path).Select(o =>
            {
                var fileInfo = new FileInfo(o);
                var name = Path.GetFileNameWithoutExtension(o);

                return new GameSlot
                {
                    Modified = fileInfo.LastWriteTimeUtc,
                    Name = name
                };
            }).ToDictionary(o => o.Name);
        }

        public async Task<GameState> LoadAsync(string slotName)
        {
            using (var reader = new FileStream(this.CreateFilePath(slotName), FileMode.Open))
            {
                var bytes = new byte[reader.Seek(0, SeekOrigin.End)];
                reader.Seek(0, SeekOrigin.Begin);

                await reader.ReadAsync(bytes, 0, bytes.Length);

                var gameState = ZeroFormatterSerializer.Deserialize<GameState>(bytes);

                return gameState;
            }
        }

        public async Task SaveAsync(GameState game, string slotName)
        {
            var bytes = ZeroFormatterSerializer.Serialize(game);
            var path = this.CreateFilePath(slotName);
            using (var writer = new FileStream(path, FileMode.Create))
            {
                await writer.WriteAsync(bytes, 0, bytes.Length);
            }

            var gameSlot = new GameSlot
            {
                Modified = DateTime.UtcNow,
                Name = slotName
            };

            this.slots[slotName] = gameSlot;
        }

        private string CreateFilePath(string slotName)
        {
            return Path.Combine(this.path, slotName + ".save");
        }
    }
}