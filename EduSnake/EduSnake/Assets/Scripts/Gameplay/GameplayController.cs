using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private int[] increaseSpeed = new int[2];

    private int lives = 5;
    private int points = 0;
    public int Points { get { return points; } }

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
        if (PlayerPrefs.HasKey("BestScore"))
        {
            int currentBestScore = PlayerPrefs.GetInt("BestScore");
            if (currentBestScore < points)
            {
                PlayerPrefs.SetInt("BestScore", points);
            }
        }
        else
        {
            PlayerPrefs.SetInt("BestScore", points);
        }

        if (points == increaseSpeed[0])
        {
            GameManager.InstanceGM.SetNormalSpeed();
        }
        else if (points == increaseSpeed[1])
        {
            GameManager.InstanceGM.SetFastSpeed();
        }
        ShowPoints();
    }

    public void RemoveLife()
    {
        if (lives > 0)
        {
            lives -= 1;
        }

        ShowLives();

        if (lives == 0)
        {
            GameManager.InstanceGM.LoseGameOver();
        }
    }
}
