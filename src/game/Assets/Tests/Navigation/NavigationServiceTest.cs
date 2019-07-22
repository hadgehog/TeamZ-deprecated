using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TeamZ.Assets.Code.DependencyInjection;
using TeamZ.Assets.Code.Game.Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace TeamZ.Tests.Navigation
{
    public class NavigationServiceTest
    {
        [UnityTest]
        public IEnumerator StartAndEndOnSamePlane()
        {
            yield return SceneManager.LoadSceneAsync("Laboratory");
            yield return new WaitUntil(() => GameObject.FindObjectOfType<Main>());

            var navigationService = Dependency<NavigationService>.Resolve();
            navigationService.Activate();

            yield return new WaitUntil(() => navigationService.Planes.Any());

            var start = new Vector3(-14.5f, -4f);
            var end = new Vector3(-10.5f, -4f);

            var path = navigationService
                .CalculatePath(start, end)
                .ToArray();

            Assert.AreEqual(start, path.First());
            Assert.AreEqual(end, path.Last());
        }
        
        [UnityTest]
        public IEnumerator StartAndEndOnDifferentPlanes()
        {
            yield return SceneManager.LoadSceneAsync("Laboratory");
            yield return new WaitUntil(() => GameObject.FindObjectOfType<Main>());

            var navigationService = Dependency<NavigationService>.Resolve();
            navigationService.Activate();

            yield return new WaitUntil(() => navigationService.Planes.Any());

            var start = new Vector3(-14.5f, -4f);
            var end = new Vector3(23.5f, -4f);

            var path = navigationService
                .CalculatePath(start, end)
                .ToArray();

            Assert.AreEqual(start, path.First());
            Assert.AreEqual(end, path.Last());
        }
    }
}
