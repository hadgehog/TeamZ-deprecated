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
using Assets.Code.Helpers;
using Assets.UI;
using UnityEngine.UI;
using System;

namespace Tests
{
    public class MainMenuTests
    {
        [UnityTest]
        public IEnumerator LoadGameSaveFromMainMenu()
        {
            if (!GameObject.FindObjectOfType<Main>())
            {
                var levelManager = new LevelManager();
			    yield return levelManager.LoadAsync(Level.Core).AsUniTask().ToCoroutine();
            }

            yield return null;
            yield return null;

            var router = GameObject.FindObjectOfType<ViewRouter>();
            router.ShowMainView();

            yield return null;

            var mainView = GameObject.FindObjectOfType<MainView>();
            mainView.Load();

            var time = Time.time;
            LoadItemView testSlot = null;
            while (!(testSlot = GameObject.FindObjectsOfType<LoadItemView>().FirstOrDefault(o => o.SlotName == "test")) && time < 10)
			{
				yield return null;
			}

			testSlot.Load();
			
			// TODO: figure out better wayt to do it
			yield return new WaitForSeconds(5);

			Assert.True(Dependency<LevelManager>.Resolve().CurrentLevel != null);
			//
		}

        [UnityTest]
        public IEnumerator SaveGameFromMainMenu()
        {
            yield return this.LoadGameSaveFromMainMenu();

            yield return null;
            yield return null;

            var router = GameObject.FindObjectOfType<ViewRouter>();
            router.ShowMainView();

            yield return null;
            var mainView = GameObject.FindObjectOfType<MainView>();
            mainView.Save();

            var saveName = GameObject.Find("SaveName_InputField").GetComponent<InputField>();
            var saveSlotName = "test";
            saveName.text = saveSlotName;

            var saveButton = GameObject.Find("Save_Button").GetComponent<Button>();
            saveButton.onClick.Invoke();

            yield return this.LoadGameSaveFromMainMenu();
        }
    }
}
