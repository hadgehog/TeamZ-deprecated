using UnityEngine;
using UnityEngine.UI;

namespace Effects
{
	public class BackgroundImage : MonoBehaviour
	{
		public Image Image;

		public void Hide()
		{
			var color = this.Image.color;
			color.a = 0.0f;
			this.Image.color = color;
		}

		public void Show()
		{
			var color = this.Image.color;
			color.a = 1.0f;
			this.Image.color = color;
		}
	}
}