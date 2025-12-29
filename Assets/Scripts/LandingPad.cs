using UnityEngine;

public class LandingPad : MonoBehaviour
{
    [SerializeField] int scoreMultiplier;

    public int getScoreMultiplier()
    {
        return scoreMultiplier;
    }
}
