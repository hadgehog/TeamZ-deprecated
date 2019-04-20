using System.Linq;
using Assets.Code.Helpers;
using Effects;
using GameSaving;
using GameSaving.States;
using TeamZ.Assets.Code.DependencyInjection;
using TeamZ.Mediator;
using UnityEngine;

namespace TeamZ.Handlers
{
	public class DeathHandler : IHandler<CharacterDead>
	{
		public UnityDependency<BlackScreen> BlackScreen { get; set; }
		public Dependency<GameController> GameController { get; set; }

		public async void Handle(CharacterDead characterDead)
		{
			var effect = this.BlackScreen.Value;
			var delay = effect.Delay;

			Time.timeScale = 0.5f;
			effect.Delay = 2;
			await effect.ShowAsync();
			effect.Delay = delay;
			Time.timeScale = 1;

			var loading = this.GameController.Value.LoadLastSavedGameAsync();
		}
	}
}
