using UnityEngine;

public class LanderAudio : MonoBehaviour
{
    [SerializeField] private AudioSource thrusterAudioSource;
    private Lander lander;
    private void Awake()
    {
        lander = GetComponent<Lander>();
    }
    private void Start()
    {
        SoundManager.instance.onSoundVolumeChanged += SoundManager_onSoundVolumeChanged;
        lander.onIdling += Lander_onIdling;
        lander.onMovingRight += Lander_onMovingRight;
        lander.onMovingLeft += Lander_onMovingLeft;
        lander.onMovingUp += Lander_onMovingUp;
        
        thrusterAudioSource.Pause();
    }

    private void SoundManager_onSoundVolumeChanged(object sender, System.EventArgs e)
    {
        thrusterAudioSource.volume = SoundManager.instance.GetSoundVolumeNormalized();
    }

    private void Lander_onMovingUp(object sender, System.EventArgs e)
    {
        if (!thrusterAudioSource.isPlaying)
        {
            thrusterAudioSource.Play();
        }
    }

    private void Lander_onMovingLeft(object sender, System.EventArgs e)
    {
        if (!thrusterAudioSource.isPlaying)
        {
            thrusterAudioSource.Play();
        }
    }

    private void Lander_onMovingRight(object sender, System.EventArgs e)
    {
        if (!thrusterAudioSource.isPlaying)
        {
            thrusterAudioSource.Play();
        }
    }

    private void Lander_onIdling(object sender, System.EventArgs e)
    {
        thrusterAudioSource.Pause();
    }
}
