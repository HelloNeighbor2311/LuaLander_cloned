using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioClip fuelPickUpAudioClip;
    [SerializeField] private AudioClip coinPickUpAudioClip;
    [SerializeField] private AudioClip crashLandingAudioClip;
    [SerializeField] private AudioClip successLandingAudioClip;

    public event EventHandler onSoundVolumeChanged;

    private const int SOUND_VOLUME_MAX = 10;
    private int soundVolume = 5;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Lander.Instance.onPickUpCoin += Lander_onPickUpCoin;
        Lander.Instance.onPickUpFuel += Lander_onPickUpFuel;
        Lander.Instance.onLanded += Lander_onLanded;
    }

    private void Lander_onLanded(object sender, Lander.onLandedEventArgs e)
    {
        switch (e.type) {
            case Lander.LandingType.SuccessLanding:
                AudioSource.PlayClipAtPoint(successLandingAudioClip, Camera.main.transform.position, GetSoundVolumeNormalized());
                break;
            default:
                AudioSource.PlayClipAtPoint(crashLandingAudioClip, Camera.main.transform.position, GetSoundVolumeNormalized());
                break;
        }
    }

    private void Lander_onPickUpFuel(object sender, Lander.onPickUpFuelEventArg e)
    {
        AudioSource.PlayClipAtPoint(fuelPickUpAudioClip, Camera.main.transform.position, GetSoundVolumeNormalized());
    }

    private void Lander_onPickUpCoin(object sender, Lander.onPickUpCoinEventArg e)
    {
        AudioSource.PlayClipAtPoint(coinPickUpAudioClip, Camera.main.transform.position, GetSoundVolumeNormalized());
    }

    public void ChangeSoundVolume()
    {

        soundVolume = (soundVolume + 1) % SOUND_VOLUME_MAX;
        onSoundVolumeChanged?.Invoke(this, EventArgs.Empty);
    }
    public int GetSoundVolume()
    {
        return soundVolume;
    }
    public float GetSoundVolumeNormalized()
    {
        return (float)soundVolume / SOUND_VOLUME_MAX;
    }
}
