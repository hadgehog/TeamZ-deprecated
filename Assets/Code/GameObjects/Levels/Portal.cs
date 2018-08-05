using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Code.Helpers;
using Effects;
using GameObjects.Activation.Core;
using GameSaving;
using Inspectors;
using UniRx;
using UnityEditor;
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

		private string sceneName;

		public void Activate()
		{
			if (string.IsNullOrWhiteSpace(this.sceneName))
			{
				this.sceneName = this.Scene.Value?.name ?? string.Empty;
			}

			if (!Level.All.ContainsKey(this.sceneName))
			{
				throw new InvalidOperationException($"Level with name {this.sceneName} does not exist.");
			}

			FindObjectOfType<Main>().GameController.SwitchLevelAsync(Level.All[this.sceneName], this.Location);
		}
	}
}