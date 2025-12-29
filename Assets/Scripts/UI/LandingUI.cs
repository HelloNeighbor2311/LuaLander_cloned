using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LandingUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bannerText;
    [SerializeField] TextMeshProUGUI statsText;
    [SerializeField] Button restartBtn;
    [SerializeField] Button nextLevelBtn;
    [SerializeField] Button quitBtn;

    private void Awake()
    {
        quitBtn.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.SceneEnum.MainMenuScene);
        });
        restartBtn.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.SceneEnum.GamePlayScene);
        });
    }
    private void Start()
    {
        
        nextLevelBtn.onClick.AddListener(GameManager.Instance.GoToNextLevel);
        Lander.Instance.onLanded += Lander_onLanded;
        Hide();
    }

    private void Lander_onLanded(object sender, Lander.onLandedEventArgs e)
    {
        if (e.type == Lander.LandingType.SuccessLanding)
        {
            bannerText.text = "SUCCESSFUL LANDING!";
            nextLevelBtn.gameObject.SetActive(true);
        }
        else
        {
            bannerText.text = "<color=#ff0000>CRASH !</color>";
        }
        statsText.text = e.landingSpeed + "\n" + e.landingAngle + "\n" + "x" +e.multiplier + "\n" + e.score;
        Show();
    }
    private void Show()
    {
        gameObject.SetActive(true);
        restartBtn.Select();
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    
}
