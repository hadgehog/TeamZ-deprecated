using UnityEngine;
using UniRx;

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
    // ambient effects
    public AudioClip Portal;
    public AudioClip TakeObject;
    public AudioClip MoveObject;
    public AudioClip DropObject;
    public AudioClip KillObject;
    public AudioClip MenuOpenClose;
    public AudioClip MenuClick;
	public AudioClip EnemyGunShooting;
	// game music
	public AudioClip MenuBackgroungMusic;
    public AudioClip Level1BackgroungMusic;
    public AudioClip Level2BackgroungMusic;

    private AudioSource audioSource;

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
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void PlayStepsSound(RunHappened sound)
    {
		if (sound.isClimbing)
		{
			if (this.Climb != null && this.audioSource.clip != this.Climb)
			{
				this.audioSource.volume = 0.1f;
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

	private void StopStepsSound(RunEnded sound)
	{
		if (sound.isClimbing)
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

	private void PlayJumpSound(JumpHappened sound)
    {
        if (this.Jump != null)
        {
			this.audioSource.PlayOneShot(this.Jump);
		}
    }

    private void PlayPunchSound(PunchHappened sound)
    {
        if (this.Punch != null)
        {
			this.audioSource.PlayOneShot(this.Punch);
		}
    }

    private void PlayKickSound(KickHappened sound)
    {
        if (this.Kick != null)
        {
			this.audioSource.PlayOneShot(this.Kick);
		}
    }
}
