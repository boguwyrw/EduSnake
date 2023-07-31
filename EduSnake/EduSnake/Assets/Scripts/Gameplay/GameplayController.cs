using UnityEngine;
using TMPro;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TMP_Text directionArrowText;

    [SerializeField] private float timeToShowDirectionArrow = 8.0f;

    [SerializeField] private int maxDirectionArrow = 4;

    private float currentTimeToShowArrow = 0.0f;

    private int lives = 5;
    private int points = 0;
    private int directionArrowNumber = 0;

    public int Points { get { return points; } }

    private void Start()
    {
        ShowLives();
        ShowPoints();

        directionArrowNumber = maxDirectionArrow;
        ShowDirectionArrowText();

        currentTimeToShowArrow = timeToShowDirectionArrow;
    }

    private void Update()
    {
        if (currentTimeToShowArrow > 0.0f && directionArrowNumber > 0)
        {
            currentTimeToShowArrow -= Time.deltaTime;

            if (currentTimeToShowArrow < 0.0f)
            {
                GameManager.Instance.ShowDirArrow();
                directionArrowNumber--;
                ShowDirectionArrowText();
                currentTimeToShowArrow = 0.0f;
            }
        }
    }

    /// <summary>
    /// Method for display snake lives in UI element
    /// </summary>
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

    /// <summary>
    /// Method for display player lives in UI element
    /// </summary>
    private void ShowPoints()
    {
        pointsText.text = "Points: " + points.ToString();
    }

    /// <summary>
    /// Method for display number of remaining direction arrows in UI element
    /// </summary>
    private void ShowDirectionArrowText()
    {    
        directionArrowText.text = directionArrowNumber.ToString();
    }

    /// <summary>
    /// Methods responsible for increasing points during game and display them in UI element
    /// </summary>
    public void AssignPoints()
    {
        points += 1;

        GameManager.Instance.HideDirArrow();
        currentTimeToShowArrow = timeToShowDirectionArrow;

        if (PlayerPrefs.HasKey("BestScore"))
        {
            int currentBestScore = PlayerPrefs.GetInt("BestScore");
            if (currentBestScore < points)
            {
                PlayerPrefs.SetInt("BestScore", points); // wydzieliæ do innej klasy
            }
        }
        else
        {
            PlayerPrefs.SetInt("BestScore", points);
        }

        GameManager.Instance.SetNextSpeed();

        ShowPoints();
    }

    /// <summary>
    /// Methods responsible for decreasing lives during game and display them in UI element
    /// </summary>
    public void RemoveLife()
    {
        if (lives > 0)
        {
            lives -= 1;
        }

        ShowLives();

        if (lives == 0)
        {
            GameManager.Instance.LoseGameOver();
        }
    }
}
