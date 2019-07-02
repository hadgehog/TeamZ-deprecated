using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UniRx.Async;
using UnityEngine;

namespace TeamZ.Assets.Code.Helpers
{
	public class AudioSourcePull
	{
		public AudioSourcePull()
		{
			this.gameObject = new GameObject("~Sounds");
			GameObject.DontDestroyOnLoad(this.gameObject);
		}

		private Queue<AudioSource> freeAudioSources = new Queue<AudioSource>();
		private HashSet<AudioSource> reducingVolumeAudioSources = new HashSet<AudioSource>();
		private Dictionary<object, AudioSource> audioSourceCache = new Dictionary<object, AudioSource>();
		private GameObject gameObject;

		public AudioSource Lend(object id)
		{
			if (this.audioSourceCache.TryGetValue(id, out var audioSource))
			{
				return audioSource;
			}

			if (this.freeAudioSources.Count <= 0)
			{
				this.freeAudioSources.Enqueue(this.gameObject.AddComponent<AudioSource>());
			}

			audioSource = this.freeAudioSources.Dequeue();
			audioSource.Stop();
			audioSource.volume = 0.4f;
			audioSource.loop = false;

			this.audioSourceCache.Add(id, audioSource);

			return audioSource;
		}

		public void Release(object id)
		{
			if (!this.audioSourceCache.TryGetValue(id, out var audioSource))
			{
				Debug.Log($"Audio source { id } is missing");
				return;
			}

			audioSource.Stop();
			this.audioSourceCache.Remove(id);
			this.freeAudioSources.Enqueue(audioSource);
		}


		public async UniTask SoftPause(object id, float rate = 1)
		{
			var audioSource = this.Lend(id);

			await this.ReduceVolume(audioSource, rate);
			
			audioSource.Pause();
		}

		public async UniTask SoftRelease(object id, float rate = 1)
		{
			var audioSource = this.Lend(id);

			await this.ReduceVolume(audioSource, rate);

			this.Release(id);
		}

		private async UniTask ReduceVolume(AudioSource audioSource, float rate)
		{
			IEnumerator ReduceVolume()
			{
				while (audioSource.volume > 0.001 && this.reducingVolumeAudioSources.Contains(audioSource))
				{
					audioSource.volume -= rate * Time.unscaledDeltaTime;
					yield return null;
				}
			}

			this.reducingVolumeAudioSources.Add(audioSource);
			await Observable.FromMicroCoroutine(ReduceVolume);
			this.reducingVolumeAudioSources.Remove(audioSource);
		}

		public async UniTask Play(AudioClip audio, float volume = 0.4f)
		{
			await PlayOnce(audio, Guid.NewGuid(), volume);
		}

		public async UniTask PlayOnce(AudioClip audio, object id, float volume)
		{
			var audioSource = this.Lend(id);

			if (audioSource.isPlaying)
			{
				return;
			}

			audioSource.PlayOneShot(audio, volume);

			await UniTask.Delay(TimeSpan.FromSeconds(audio.length + 0.5));
			this.Release(id);
		}

		public async void PlayLooped(AudioClip audio, object id, float volume, float rate = 1)
		{
			var audioSource = this.Lend(id);

			audioSource.loop = true;
			audioSource.clip = audio;
			audioSource.volume = 0;
			audioSource.Play();

			await this.InreaseVolume(audioSource, volume, rate);
		}

        public bool IsPlaying(object id, AudioClip audio)
        {
            var audioSource = this.Lend(id);

            return audioSource.isPlaying && audioSource.clip == audio;
        }

		private async UniTask InreaseVolume(AudioSource audioSource, float volume, float rate)
		{
			this.reducingVolumeAudioSources.Remove(audioSource);
			IEnumerator IncreaseVolume()
			{
				while (audioSource.volume < volume && !this.reducingVolumeAudioSources.Contains(audioSource))
				{
					audioSource.volume += rate * Time.unscaledDeltaTime;
					yield return null;
				}
			}

			await Observable.FromMicroCoroutine(IncreaseVolume);
			audioSource.volume = volume;
		}
	}
}
