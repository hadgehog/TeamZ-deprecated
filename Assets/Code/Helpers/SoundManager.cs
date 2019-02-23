using UnityEngine;
using UniRx;
using Game.Levels;
using System;
using UniRx.Async;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    // characters effects
    public AudioClip Steps;
    public AudioClip Jump;
    public AudioClip Climb;
    public AudioClip Punch;
    public AudioClip Kick;
    public AudioClip Hurting;
    public AudioClip Die;
    // world effects
    public AudioClip Portal;
    public AudioClip TakeObject;
    public AudioClip MoveObject;
    public AudioClip DropObject;
    public AudioClip KillObject;
	public AudioClip EnemyGunShooting;
	// ambient sounds
	public AudioClip AmbientNoize1;
	public AudioClip AmbientNoize2;
	public AudioClip AmbientNoize3;
	// game effects
	public AudioClip MenuOpenClose;
    public AudioClip MenuClick;
	// game music
	public AudioClip MenuBackgroungMusic;
    public AudioClip Level1BackgroungMusic;
    public AudioClip Level2BackgroungMusic;

    private AudioSource audioSource;
	private Queue<AudioSource> freeAudioSources = new Queue<AudioSource>();
	private Dictionary<string, AudioSource> audioSourceCache = new Dictionary<string, AudioSource>();

	private string currentLevelName = "Level_Laboratory";

	// Use this for initialization
	void Start ()
    {
        this.audioSource = GetComponent<AudioSource>();
		this.audioSource.Stop();
		this.audioSource.volume = 0.3f;
		this.audioSource.loop = false;

		MessageBroker.Default.Receive<RunHappened>().Subscribe(this.PlayStepsSound);
		MessageBroker.Default.Receive<RunEnded>().Subscribe(this.StopStepsSound);
		MessageBroker.Default.Receive<JumpHappened>().Subscribe(this.PlayJumpSound);
        MessageBroker.Default.Receive<PunchHappened>().Subscribe(this.PlayPunchSound);
        MessageBroker.Default.Receive<KickHappened>().Subscribe(this.PlayKickSound);
		MessageBroker.Default.Receive<TakeObjectHappened>().Subscribe(this.PlayTakeObjectSound);
		MessageBroker.Default.Receive<PortalToNextLevelHappened>().Subscribe(this.PlayPortalToNextLevelSound);
		MessageBroker.Default.Receive<CharacterDead>().Subscribe(o => this.PlayOnce(this.Die, "Death", 1.0f));
		MessageBroker.Default.Receive<GamePaused>().Subscribe(this.OnGamePaused);
		MessageBroker.Default.Receive<GameResumed>().Subscribe(this.OnGameResumed);
	}
	
	public AudioSource Lend(string name)
	{
		if (this.audioSourceCache.TryGetValue(name, out var audioSource))
		{
			return audioSource;
		}

		if(this.freeAudioSources.Count <= 0)
		{
			this.freeAudioSources.Enqueue(this.gameObject.AddComponent<AudioSource>());
		}

		audioSource = this.freeAudioSources.Dequeue();
		audioSource.Stop();
		audioSource.volume = 0.3f;
		audioSource.loop = false;

		this.audioSourceCache.Add(name, audioSource);

		return audioSource;
	}

	public void Release(string name)
	{
		if(!this.audioSourceCache.TryGetValue(name, out var audioSource))
		{
			Debug.Log($"Audio source { name } is missing");
			return;
		}

		audioSource.Stop();
		this.audioSourceCache.Remove(name);
		this.freeAudioSources.Enqueue(audioSource);
	}

	public async UniTask Play(AudioClip audio, float volume = 0.3f)
	{
		await PlayOnce(audio, Guid.NewGuid().ToString(), volume);
	}

	public async UniTask PlayOnce(AudioClip audio, string name, float volume)
	{
		var audioSource = this.Lend(name);

		if (audioSource.isPlaying)
		{
			return;
		}

		audioSource.PlayOneShot(audio, volume);

		await UniTask.Delay(TimeSpan.FromSeconds(audio.length + 0.5));
		this.Release(name);
	}

	public void PlayLooped(AudioClip audio, string name, float volume)
	{
		var audioSource = this.Lend(name);

		if (audioSource.isPlaying)
		{
			return;
		}

		audioSource.loop = true;
		audioSource.clip = audio;
		audioSource.volume = volume;

		audioSource.Play();
	}

	private void OnGamePaused(GamePaused soundObj)
	{
		Debug.Log($"Game paused");

		this.audioSource.loop = false;
		this.audioSource.Stop();
		this.audioSource.clip = null;

		this.Release("Noize");
		this.Release(this.currentLevelName);
		this.PlayLooped(this.MenuBackgroungMusic, "Menu", 0.11f);
	}

	private void OnGameResumed(GameResumed soundObj)
	{
		Debug.Log($"Game resumed");

		this.audioSource.loop = false;
		this.audioSource.Stop();
		this.audioSource.clip = null;

		this.Release("Menu");
		this.Release(this.currentLevelName);

		this.PlayLooped(this.AmbientNoize2, "Noize", 0.05f);

		string levelName = "Level_" + soundObj.Level;

		switch (soundObj.Level)
		{
			case "Laboratory":
				this.PlayLooped(this.Level1BackgroungMusic, levelName, 0.09f);
				break;
			case "Laboratory2":
				this.PlayLooped(this.Level2BackgroungMusic, levelName, 0.09f);
				break;
		}

		this.currentLevelName = levelName;
	}

	private void PlayStepsSound(RunHappened soundObj)
    {
		if (soundObj.isClimbing)
		{
			if (this.Climb != null && this.audioSource.clip != this.Climb)
			{
				this.audioSource.volume = 0.08f;
				this.audioSource.loop = true;
				this.audioSource.clip = this.Climb;
				this.audioSource.Play();
			}
		}
		else
		{
			if (this.Steps != null && this.audioSource.clip != this.Steps)
			{
				this.audioSource.loop = true;
				this.audioSource.clip = this.Steps;
				this.audioSource.Play();
			}
		}
    }

	private void StopStepsSound(RunEnded soundObj)
	{
		if (soundObj.isClimbing)
		{
			if (this.Climb != null && this.audioSource.clip == this.Climb && this.audioSource.isPlaying)
			{
				this.audioSource.volume = 0.3f;
				this.audioSource.loop = false;
				this.audioSource.Stop();
				this.audioSource.clip = null;
			}
		}
		else
		{
			if (this.Steps != null && this.audioSource.clip == this.Steps && this.audioSource.isPlaying)
			{
				this.audioSource.loop = false;
				this.audioSource.Stop();
				this.audioSource.clip = null;
			}
		}
	}

	private void PlayJumpSound(JumpHappened soundObj)
    {
        if (this.Jump != null)
        {
			this.Play(this.Jump, 0.5f);
		}
    }

    private void PlayPunchSound(PunchHappened soundObj)
    {
        if (this.Punch != null)
        {
			this.Play(this.Punch, 0.2f);
		}
    }

    private void PlayKickSound(KickHappened soundObj)
    {
        if (this.Kick != null)
        {
			this.Play(this.Kick, 0.15f);
		}
    }

	private void PlayTakeObjectSound(TakeObjectHappened soundObj)
	{
		if (this.TakeObject != null)
		{
			this.Play(this.TakeObject);
		}
	}

	private void PlayPortalToNextLevelSound(PortalToNextLevelHappened soundObj)
	{
		if (this.Portal != null)
		{
			this.Play(this.Portal, 1.0f);
		}
	}
}
