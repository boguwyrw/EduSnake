using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TMP_Text bestScoreText;
    [SerializeField] private TMP_Text yourScoreWinText;
    [SerializeField] private TMP_Text yourScoreLoseText;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void AssignFinalScore(int finalScore)
    {
        yourScoreWinText.text = "Your score: " + finalScore.ToString();
        yourScoreLoseText.text = "Your score: " + finalScore.ToString();
    }
}
