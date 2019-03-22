using UnityEngine;
using UniRx;
using Game.Levels;
using System;
using UniRx.Async;
using System.Collections.Generic;
using System.Collections;
using TeamZ.Assets.Code.Helpers;
using System.Linq;

public class SoundManager : MonoBehaviour
{
	private const float MUSIC_CHANGE_RATE = 0.4f;
	private const float MUSIC_CHANGE_RATEx4 = MUSIC_CHANGE_RATE * 4;
	private const float MUSIC_VOLUME = .3f;

	private const string NOISE = "NOISE";
	private const string MENU = "MENU";

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

    private AudioSource defaultAudioSource;

	private string currentLevelMusic = "Level_Laboratory";
	private AudioSourcePull sounds;

	// Use this for initialization
	void Start ()
    {
        this.defaultAudioSource = GetComponent<AudioSource>();
		this.defaultAudioSource.Stop();
		this.defaultAudioSource.volume = 0.3f;
		this.defaultAudioSource.loop = false;
		this.sounds = new AudioSourcePull();

		MessageBroker.Default.Receive<RunHappened>().Subscribe(this.PlayStepsSound);
		MessageBroker.Default.Receive<RunEnded>().Subscribe(this.StopStepsSound);
		MessageBroker.Default.Receive<JumpHappened>().Subscribe(this.PlayJumpSound);
        MessageBroker.Default.Receive<PunchHappened>().Subscribe(this.PlayPunchSound);
        MessageBroker.Default.Receive<KickHappened>().Subscribe(this.PlayKickSound);
		MessageBroker.Default.Receive<TakeObjectHappened>().Subscribe(this.PlayTakeObjectSound);
		MessageBroker.Default.Receive<PortalToNextLevelHappened>().Subscribe(this.PlayPortalToNextLevelSound);
		MessageBroker.Default.Receive<CharacterDead>().Subscribe(o => this.sounds.PlayOnce(this.Die, "Death", 1.0f));

		MessageBroker.Default.Receive<GamePaused>().Subscribe(this.OnGamePausedAsync);
		MessageBroker.Default.Receive<GameResumed>().Subscribe(this.OnGameResumedAsync);
	}
	
	private async void OnGamePausedAsync(GamePaused soundObj)
	{
		Debug.Log($"Game paused");

		this.defaultAudioSource.loop = false;
		this.defaultAudioSource.Stop();
		this.defaultAudioSource.clip = null;

		var fadingNoise = this.sounds.SoftPause(NOISE, MUSIC_CHANGE_RATEx4);
		var fadingMusic = this.sounds.SoftPause(this.currentLevelMusic, MUSIC_CHANGE_RATEx4);

		this.sounds.PlayLooped(this.MenuBackgroungMusic, MENU, MUSIC_VOLUME);
	}

	private async void OnGameResumedAsync(GameResumed message)
	{
		if (string.IsNullOrWhiteSpace(message.Level))
		{
			return;
		}

		Debug.Log($"Game resumed");

		this.defaultAudioSource.loop = false;
		this.defaultAudioSource.Stop();
		this.defaultAudioSource.clip = null;

		var fadingMenu = this.sounds.SoftRelease(MENU, MUSIC_CHANGE_RATEx4);
		this.sounds.PlayLooped(this.AmbientNoize2, NOISE, MUSIC_VOLUME / 4, MUSIC_CHANGE_RATE);

		string levelName = "Level_" + message.Level;
		switch (message.Level)
		{
			case Level.LABORATORY:
				this.sounds.PlayLooped(this.Level1BackgroungMusic, levelName, MUSIC_VOLUME, MUSIC_CHANGE_RATE);
				break;
			case Level.LABORATORY2:
				this.sounds.PlayLooped(this.Level2BackgroungMusic, levelName, MUSIC_VOLUME, MUSIC_CHANGE_RATE);
				break;
		}

		if (this.currentLevelMusic != levelName)
		{
			var releasingPrevLevelMusic = this.sounds.SoftRelease(this.currentLevelMusic);
		}
		this.currentLevelMusic = levelName;
	}

	private void PlayStepsSound(RunHappened soundObj)
    {
		if (soundObj.isClimbing)
		{
			if (this.Climb != null && this.defaultAudioSource.clip != this.Climb)
			{
				this.defaultAudioSource.volume = .8f;
				this.defaultAudioSource.loop = true;
				this.defaultAudioSource.clip = this.Climb;
				this.defaultAudioSource.Play();
			}
		}
		else
		{
			if (this.Steps != null && this.defaultAudioSource.clip != this.Steps)
			{
				this.defaultAudioSource.loop = true;
				this.defaultAudioSource.clip = this.Steps;
				this.defaultAudioSource.Play();
			}
		}
    }

	private void StopStepsSound(RunEnded soundObj)
	{
		if (soundObj.isClimbing)
		{
			if (this.Climb != null && this.defaultAudioSource.clip == this.Climb && this.defaultAudioSource.isPlaying)
			{
				this.defaultAudioSource.volume = 1f;
				this.defaultAudioSource.loop = false;
				this.defaultAudioSource.Stop();
				this.defaultAudioSource.clip = null;
			}
		}
		else
		{
			if (this.Steps != null && this.defaultAudioSource.clip == this.Steps && this.defaultAudioSource.isPlaying)
			{
				this.defaultAudioSource.loop = false;
				this.defaultAudioSource.Stop();
				this.defaultAudioSource.clip = null;
			}
		}
	}

	private void PlayJumpSound(JumpHappened soundObj)
    {
        if (this.Jump != null)
        {
			this.sounds.Play(this.Jump, 1f);
		}
    }

    private void PlayPunchSound(PunchHappened soundObj)
    {
        if (this.Punch != null)
        {
			this.sounds.Play(this.Punch, 1f);
		}
    }

    private void PlayKickSound(KickHappened soundObj)
    {
        if (this.Kick != null)
        {
			this.sounds.Play(this.Kick, 0.15f);
		}
    }

	private void PlayTakeObjectSound(TakeObjectHappened soundObj)
	{
		if (this.TakeObject != null)
		{
			this.sounds.Play(this.TakeObject);
		}
	}

	private void PlayPortalToNextLevelSound(PortalToNextLevelHappened soundObj)
	{
		if (this.Portal != null)
		{
			this.sounds.Play(this.Portal, 1.0f);
		}
	}
}
