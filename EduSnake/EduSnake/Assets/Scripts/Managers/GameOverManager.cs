using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject fadeGameOverPanel;

    [SerializeField] private TMP_Text bestWinScoreText;
    [SerializeField] private TMP_Text bestLoseScoreText;
    [SerializeField] private TMP_Text yourScoreWinText;
    [SerializeField] private TMP_Text yourScoreLoseText;

    private void Start()
    {
        ActivateDeactivateFade(false);
    }

    /// <summary>
    /// Method responsible for turning on and off fade during the game
    /// </summary>
    /// <param name="fadeOnOff"></param>
    public void ActivateDeactivateFade(bool fadeOnOff)
    {
        fadeGameOverPanel.SetActive(fadeOnOff);
    }

    /// <summary>
    /// Method responsible for showing on UI final score
    /// </summary>
    /// <param name="finalScore"></param>
    public void AssignFinalScore(int finalScore)
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            int currentBestScore = PlayerPrefs.GetInt("BestScore");
            bestWinScoreText.text = "Best score: " + currentBestScore.ToString();
            bestLoseScoreText.text = "Best score: " + currentBestScore.ToString();
        }

        yourScoreWinText.text = "Your score: " + finalScore.ToString() + "/" + GameManager.InstanceGM.GetMaxTasksNumber();
        yourScoreLoseText.text = "Your score: " + finalScore.ToString() + "/" + GameManager.InstanceGM.GetMaxTasksNumber();
    }
}
