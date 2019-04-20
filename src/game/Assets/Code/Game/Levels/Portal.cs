using System;
using Game.Activation.Core;
using GameSaving;
using GameSaving.States;
using Inspectors;
using TeamZ.Assets.Code.DependencyInjection;
using UniRx;
using UnityEngine;

namespace Game.Levels
{
    [ExecuteInEditMode]
    public class Portal : MonoBehaviour, IActivable
    {
		Dependency<GameController> gameController;

#if UNITY_EDITOR
        public SceneReactiveProperty Scene = new SceneReactiveProperty();

        public void OnEnable()
        {
            this.Scene.Subscribe(scene => this.sceneName = scene?.name);
        }

#endif
        public string Location;

        [SerializeField]
        private string sceneName;

        public void Activate()
        {
#if UNITY_EDITOR
            if (string.IsNullOrWhiteSpace(this.sceneName))
            {
                this.sceneName = this.Scene.Value?.name ?? string.Empty;
            }
#endif

            if (!Level.All.ContainsKey(this.sceneName))
            {
                throw new InvalidOperationException($"Level with name {this.sceneName} does not exist.");
            }

			MessageBroker.Default.Publish(new PortalToNextLevelHappened());
			gameController.Value.SwitchLevelAsync(Level.All[this.sceneName], this.Location);
        }
    }

	public class PortalToNextLevelHappened
	{
		public PortalToNextLevelHappened()
		{
		}
	}
}