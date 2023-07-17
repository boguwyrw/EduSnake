using System.Collections;
using System.Collections.Generic;
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

    public void ActivateDeactivateFade(bool fadeOnOff)
    {
        fadeGameOverPanel.SetActive(fadeOnOff);
    }

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
