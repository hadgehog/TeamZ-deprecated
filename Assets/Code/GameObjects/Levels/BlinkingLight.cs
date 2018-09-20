using UnityEngine;

namespace TeamZ.Assets.Code.GameObjects.Levels
{
	[RequireComponent(typeof(Light))]
	public class BlinkingLight : MonoBehaviour
	{
		public float Speed = 1;
		public float MaximumIntensityDelta = 0.05f;
		public bool Synchonized = true;
		private Light currentLight;
		private float initialIntensity;
		public float Shift = 0;

		private void Start()
		{
			this.currentLight = this.GetComponent<Light>();
			this.initialIntensity = this.currentLight.intensity;

			if (this.Synchonized)
			{
				return;
			}

			this.Shift = Random.Range(0, 3.14f);
		}

		private void FixedUpdate()
		{
			var scaleByTime = Mathf.Sin(this.Speed * (this.Shift + Time.time));
			var change = this.initialIntensity * scaleByTime * this.MaximumIntensityDelta;
			this.currentLight.intensity = this.initialIntensity + change;
		}
	}
}
