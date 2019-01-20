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
    // game music
    public AudioClip MenuBackgroungMusic;
    public AudioClip Level1BackgroungMusic;
    public AudioClip Level2BackgroungMusic;

    private AudioSource audioSource;

    // Use this for initialization
    void Start ()
    {
        this.audioSource = GetComponent<AudioSource>();

        MessageBroker.Default.Receive<IdleHappened>().Subscribe(this.StopSound);
        MessageBroker.Default.Receive<RunHappened>().Subscribe(this.PlayStepsSound);
        MessageBroker.Default.Receive<JumpHappened>().Subscribe(this.PlayJumpSound);
        MessageBroker.Default.Receive<PunchHappened>().Subscribe(this.PlayPunchSound);
        MessageBroker.Default.Receive<KickHappened>().Subscribe(this.PlayKickSound);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void StopSound(IdleHappened sound)
    {
        this.audioSource.Stop();
    }

    private void PlayStepsSound(RunHappened sound)
    {
        if (this.Steps != null && !this.audioSource.isPlaying)
        {
            this.audioSource.PlayOneShot(this.Steps);
        }
    }

    private void PlayJumpSound(JumpHappened sound)
    {
        if (this.Jump != null && !this.audioSource.isPlaying)
        {
            this.audioSource.PlayOneShot(this.Jump);
        }
    }

    private void PlayPunchSound(PunchHappened sound)
    {
        if (this.Punch != null && !this.audioSource.isPlaying)
        {
            this.audioSource.PlayOneShot(this.Punch);
        }
    }

    private void PlayKickSound(KickHappened sound)
    {
        if (this.Kick != null && !this.audioSource.isPlaying)
        {
            this.audioSource.PlayOneShot(this.Kick);
        }
    }
}
