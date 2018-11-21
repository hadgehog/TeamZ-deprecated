using System;
using Assets.Code.Helpers;
using UniRx;
using UnityEngine;

namespace TeamZ.Assets.Code.GameObjects.Enemies.Turel
{
	public class Turel : MonoBehaviour
	{
		public ParticleSystem BulletParticleSystem;

		UnityDependency<Lizard> lizard;
		private readonly IDisposable shooting;

		private void Start()
		{
			Observable.Interval(TimeSpan.FromSeconds(1)).
				Where(o => this.lizard).
				Subscribe(o =>
				{
					this.transform.LookAt(this.lizard);
					this.BulletParticleSystem.Emit(1);
				}).
				AddTo(this);
		}

	}
}
