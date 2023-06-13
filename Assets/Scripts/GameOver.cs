using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text Score;
    public Text BestScore;
    public GameObject BestScoreRecord;

    public void Start()
    {
        if(PlayerPrefs.GetInt("score") > PlayerPrefs.GetInt("best_score"))
        {
            PlayerPrefs.SetInt("best_score", PlayerPrefs.GetInt("score"));
            BestScoreRecord.SetActive(true);
        }
        Score.text = $"점수 <color=white>{PlayerPrefs.GetInt("score")}</color>점";
        BestScore.text = $"최고 점수 <color=white>{PlayerPrefs.GetInt("best_score")}</color>점";
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.Save();
    }
}
