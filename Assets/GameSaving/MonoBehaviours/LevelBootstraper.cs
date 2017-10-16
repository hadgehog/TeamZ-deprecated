using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSaving.MonoBehaviours
{
    public class LevelBootstraper : MonoBehaviour
    {
        public string LevelName;

        private void Start()
        {
            if(!GameObject.FindObjectOfType<Main>())
            {
                SceneManager.LoadScene("Core", LoadSceneMode.Additive);
            }
        }
    }
}
