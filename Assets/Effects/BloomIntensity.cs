using UnityEngine;

namespace Assets.Effects
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class BloomIntensity : MonoBehaviour
	{
		public float deltaIntesity;
		public float speed;
		private SpriteRenderer spriteRender;
		private float initialIntensity;

		private void Start()
		{
			this.spriteRender = this.GetComponent<SpriteRenderer>();
			this.initialIntensity = this.spriteRender.material.GetColor("_EmissionColor").r;
		}

		private void Update()
		{
			var currentIntensity = this.initialIntensity + Mathf.Sin(Time.deltaTime) / this.speed * this.deltaIntesity;
			var color = new Color(currentIntensity, currentIntensity, currentIntensity);
			this.spriteRender.material.SetColor("_EmissionColor", color);
		}
	}
}
