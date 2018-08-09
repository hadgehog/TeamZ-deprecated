using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Code.Helpers;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSaving.MonoBehaviours
{
    public class LevelBootstraper : MonoBehaviour
    {
        public string LevelName;

        Dependency<Main> Main;
        Dependency<MainView> MainView;

        private void Start()
        {
            if (this.Main.Value == null)
            {
                SceneManager.LoadScene("Core", LoadSceneMode.Additive);
                Observable.NextFrame().Subscribe(_ =>
                {
                    this.Main.Value.GameController.LevelManager.CurrentLevel = Level.All[this.LevelName];
                    this.Main.Value.GameController.Bootstrap(true);
                    this.MainView.Value.Deactivate();
                });
            }
        }
    }
}
