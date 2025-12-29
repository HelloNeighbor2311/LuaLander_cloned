using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public event EventHandler onMusicVolumeChanged;

    private static float musicTime;
    private static int MUSIC_VOLUME_MAX = 10;
    private int musicVolume = 4;
    
    private AudioSource musicSource;

    private void Awake()
    {
        instance = this;
        musicSource = GetComponent<AudioSource>();
        musicSource.time = musicTime;
    }
    private void Start()
    {
        musicSource.volume = GetMusicVolumeNormalized();
    }
    void Update()
    {
        musicTime = musicSource.time;
        
    }

    public void ChangeMusicVolume()
    {

        musicVolume = (musicVolume + 1) % MUSIC_VOLUME_MAX;
        musicSource.volume = GetMusicVolumeNormalized();
        onMusicVolumeChanged?.Invoke(this, EventArgs.Empty);
    }
    public int GetMusicVolume()
    {
        return musicVolume;
    }
    public float GetMusicVolumeNormalized()
    {
        return (float)musicVolume / MUSIC_VOLUME_MAX;
    }
}
