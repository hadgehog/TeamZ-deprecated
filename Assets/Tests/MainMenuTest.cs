using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TeamZ.Assets.UI.Load;
using UnityEngine;
using UnityEngine.TestTools;
using UniRx.Async;
using UnityEngine.SceneManagement;
using TeamZ.Assets.Code.DependencyInjection;

namespace Tests
{
    public class MainMenuTests
    {
        [UnityTest]
        public IEnumerator LoadGameSaveFromMainMenu()
        {
			var levelManager = new LevelManager();
			yield return levelManager.LoadAsync(Level.Core).AsUniTask().ToCoroutine();

			GameObject.FindObjectOfType<MainView>().Load();

			while (!GameObject.FindObjectOfType<LoadItemView>())
			{
				yield return null;
			}

			GameObject.FindObjectOfType<LoadItemView>().Load();
			
			// TODO: figure out better wayt to do it
			yield return new WaitForSeconds(2);

			Assert.True(Dependency<LevelManager>.Resolve().CurrentLevel != null);
			//
		}
	}
}
