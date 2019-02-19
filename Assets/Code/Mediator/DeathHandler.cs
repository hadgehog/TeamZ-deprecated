using System.Linq;
using Assets.Code.Helpers;
using Effects;
using TeamZ.Mediator;
using UnityEngine;

namespace TeamZ.Handlers
{
	public class DeathHandler : IHandler<CharacterDead>
	{
		public UnityDependency<Main> Main { get; set; }
		public UnityDependency<BlackScreen> BlackScreen { get; set; }

		public async void Handle(CharacterDead characterDead)
		{
			var effect = this.BlackScreen.Value;
			var delay = effect.Delay;

			effect.Delay = 4;
			await effect.ShowAsync();
			effect.Delay = delay;

			var gameController = this.Main.Value.GameController;
			var lastSave = gameController.Storage.Slots.OrderByDescending(o => o.Modified).First();
			var loading = gameController.LoadSavedGameAsync(lastSave.Name);
		}
	}
}
