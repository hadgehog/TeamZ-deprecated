using System;
using Assets.Code.Helpers;
using UniRx;
using UnityEngine;

namespace TeamZ.Assets.Code.GameObjects.Enemies.Turel
{
	public class Turel : MonoBehaviour
	{
		public ParticleSystem BulletParticleSystem;
        public int Damage = 25;

        private UnityDependency<Lizard> lizard;
        private UnityDependency<Hedgehog> hedgehog;

        private readonly IDisposable shooting;

		private void Start()
		{
            if (this.lizard.Value != null && this.hedgehog.Value != null)
            {
                bool firstCharacterIsAim = true;

                Observable.Interval(TimeSpan.FromSeconds(1)).
                    Where(o => this.lizard || this.hedgehog).
                    Subscribe(o =>
                    {
                        if (firstCharacterIsAim)
                        {
                            this.transform.LookAt(this.lizard);
                        }
                        else
                        {
                            this.transform.LookAt(this.hedgehog);
                        }

                        this.BulletParticleSystem.Emit(1);
                        firstCharacterIsAim = !firstCharacterIsAim;
                    }).
                    AddTo(this);
            }
            else
            {
                Observable.Interval(TimeSpan.FromSeconds(1)).
                    Where(o => this.lizard).
                    Subscribe(o =>
                    {
                        this.transform.LookAt(this.lizard);
                        this.BulletParticleSystem.Emit(1);
                    }).
                    AddTo(this);

                Observable.Interval(TimeSpan.FromSeconds(1)).
                    Where(o => this.hedgehog).
                    Subscribe(o =>
                    {
                        this.transform.LookAt(this.hedgehog);
                        this.BulletParticleSystem.Emit(1);
                    }).
                    AddTo(this);
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            if (other.GetComponentInParent<ICharacter>() is ICharacter character)
            {
                character.TakeDamage(this.Damage);
            }
        }
    }
}
