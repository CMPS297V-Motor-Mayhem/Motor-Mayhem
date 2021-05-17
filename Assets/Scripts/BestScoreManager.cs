using UnityEngine;

public class BestScoreManager : MonoBehaviour
{
    private void Awake()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", -1);
        if (bestScore == -1)
        {
            // first time running game:
            this.SaveInitialBestScore();
        }
    }

    private void SaveInitialBestScore()
    {
        PlayerPrefs.SetInt("BestScore", 0);
    }

    // Public static methods:

    public static void SaveBestScore(int score)
    {
        // only update best score when input is greater than existing best score:

        int bestScore = GetBestScore();
        if (score > bestScore)
        {
            // update best score:
            PlayerPrefs.SetInt("BestScore", score);
        }
    }

    public static int GetBestScore()
    {
        return PlayerPrefs.GetInt("BestScore");
    }
}
