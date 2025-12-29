using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private Button soundVolumeBtn;
    [SerializeField] private Button musicVolumeBtn;
    [SerializeField] private TextMeshProUGUI soundVolumeText;
    [SerializeField] private TextMeshProUGUI musicVolumeText;

    private void Awake()
    {
        resumeBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.UnPauseGame();
        });
        mainMenuBtn.onClick.AddListener(() =>
        {
           SceneLoader.LoadScene(SceneLoader.SceneEnum.MainMenuScene);
            GameManager.Instance.UnPauseGame();
        });
        soundVolumeBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.ChangeSoundVolume();
            soundVolumeText.text = "SOUND: " + SoundManager.instance.GetSoundVolume();  
        });
        musicVolumeBtn.onClick.AddListener(() =>
        {
            MusicManager.instance.ChangeMusicVolume();
            musicVolumeText.text = "MUSIC: " + MusicManager.instance.GetMusicVolume();
        });

    }
    private void Start()
    {
        
        GameManager.Instance.onGamePause += GameManager_onGamePause;
        GameManager.Instance.onGameUnPause += GameManager_onGameUnPause;

        soundVolumeText.text = "SOUND: " + SoundManager.instance.GetSoundVolume();
        musicVolumeText.text = "MUSIC: " + MusicManager.instance.GetMusicVolume();
        Hide();
    }

    private void GameManager_onGameUnPause(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_onGamePause(object sender, System.EventArgs e)
    {
        resumeBtn.Select();
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
