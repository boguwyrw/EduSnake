using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TMP_Text bestScoreText;
    [SerializeField] private TMP_Text yourScoreText;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void AssignFinalScore(int finalScore)
    {
        yourScoreText.text = "Your score: " + finalScore.ToString();
    }
}
