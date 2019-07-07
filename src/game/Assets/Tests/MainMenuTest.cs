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
using TeamZ.Assets.Tests;

namespace TeamZ.Tests
{
    public class MainMenuLoadingTests
    {
        [UnityTest]
        public IEnumerator LoadGameSaveFromMainMenu()
        {
            yield return LoadGameSave();
        }

        public static IEnumerator LoadGameSave()
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
            while (Time.time - time < 10)
            {
                testSlot = GameObject.FindObjectsOfType<LoadItemView>().FirstOrDefault(o => o.SlotName == "test");
                if (testSlot)
                {
                    break;
                }

                yield return null;
            }

            testSlot.Load();

            var levelManagerDependency = Dependency<LevelManager>.Resolve();
            yield return new WaitUntilWithTimeout(() => levelManagerDependency.CurrentLevel == null, TimeSpan.FromSeconds(5));

            Assert.True(levelManagerDependency.CurrentLevel != null);
        }

        
    }
    public class MainMenuSavingTests
    {
        [UnityTest]
        public IEnumerator SaveGameFromMainMenu()
        {
            yield return MainMenuLoadingTests.LoadGameSave();

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

            yield return null;
            router.ShowGameHUDView();

            yield return MainMenuLoadingTests.LoadGameSave();
        }
    }
}
