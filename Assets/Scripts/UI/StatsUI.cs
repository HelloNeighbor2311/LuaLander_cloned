using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statsTextUGUI;
    [SerializeField] private GameObject upArrow;
    [SerializeField] private GameObject downArrow;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private Image fuelFill;

    private void Update()
    {
        UpdateStatsText();
    }

    private void UpdateStatsText()
    {
        fuelFill.fillAmount = Lander.Instance.getFuelNormalized();
        upArrow.SetActive(Lander.Instance.getSpeedY() >=0);
        downArrow.SetActive(Lander.Instance.getSpeedY() < 0);
        leftArrow.SetActive(Lander.Instance.getSpeedX() < 0);
        rightArrow.SetActive(Lander.Instance.getSpeedX() > 0);
        statsTextUGUI.text = GameManager.gameLevel + "\n"
                            + GameManager.Instance.getTimer() + "\n"
                            + GameManager.Instance.getScore() + "\n"
                            + Lander.Instance.getSpeedX() + "\n"
                            + Lander.Instance.getSpeedY();
                            
    }
}
