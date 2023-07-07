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

    private bool isSnakeAlive = true;
    public bool IsSnakeAlive { get { return isSnakeAlive; } }

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
        if (points == 5)
        {
            GameManager.InstanceGM.SetNormalSpeed();
        }
        else if (points == 10)
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
            isSnakeAlive = false;
            GameManager.InstanceGM.GameOver();
        }
    }
}
