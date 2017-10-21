using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Effects
{
    public class BlackScreen : MonoBehaviour
    {
        public float Delay;
        public Image Image;

        public async Task ShowAsync()
        {
            await Observable.FromMicroCoroutine(this.ShowInternal);
        }

        public async Task HideAsync()
        {
            await Observable.FromMicroCoroutine(this.HideInternal);
        }

        private IEnumerator HideInternal()
        {
            while (this.Image.color.a > 0)
            {
                var color = this.Image.color;
                color.a = Mathf.Clamp01(color.a - Time.deltaTime / this.Delay);
                this.Image.color = color;

                yield return null;
            }
        }

        private IEnumerator ShowInternal()
        {
            while (this.Image.color.a < 1)
            {
                var color = this.Image.color;
                color.a = Mathf.Clamp01(color.a + Time.deltaTime / this.Delay);
                this.Image.color = color;

                yield return null;
            }
        }
    }
}
