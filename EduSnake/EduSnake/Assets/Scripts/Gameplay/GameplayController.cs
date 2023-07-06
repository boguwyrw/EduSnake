using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text pointsText;

    private int lives = 5;
    private int points = 0;

    private void Start()
    {
        ShowLives();
        ShowPoints();
    }

    private void Update()
    {
        
    }

    private void ShowLives()
    {
        if (lives > 1)
        {
            livesText.text = "Lives: " + lives.ToString();
        }
        else
        {
            livesText.text = "Life: " + lives.ToString();
        }
    }

    private void ShowPoints()
    {
        pointsText.text = "Points: " + points.ToString();
    }

    public void AssignPoints()
    {
        points += 1;
        ShowPoints();
    }

    public void RemoveLife()
    {
        lives -= 1;
        ShowLives();
    }
}