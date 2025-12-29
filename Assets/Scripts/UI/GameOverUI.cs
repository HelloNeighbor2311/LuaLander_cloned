using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private TextMeshProUGUI finalScore;
    private void Awake()
    {
        mainMenuBtn.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.SceneEnum.MainMenuScene);
        });
    }
    private void Start()
    {
       finalScore.text =  "FINAL SCORE: "+GameManager.Instance.getTotalScore().ToString();   
    }
}
