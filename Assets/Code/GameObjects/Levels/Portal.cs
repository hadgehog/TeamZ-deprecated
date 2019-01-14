using System;
using GameObjects.Activation.Core;
using Inspectors;
using UniRx;
using UnityEngine;

namespace GameObjects.Levels
{
    public class Portal : MonoBehaviour, IActivable
    {
#if UNITY_EDITOR
        public SceneReactiveProperty Scene;

        public void OnEnable()
        {
            if (this.Scene == null)
                this.Scene = new SceneReactiveProperty();

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

            FindObjectOfType<Main>().GameController.SwitchLevelAsync(Level.All[this.sceneName], this.Location);
        }
    }
}