using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // TO DO
    // 1. System zapisu

    #region GameManager Instance
    public static GameManager InstanceGM { get; private set; }

    private void Awake()
    {
        if (InstanceGM != null && InstanceGM != this)
        {
            Destroy(this);
        }
        else
        {
            InstanceGM = this;
        }
    }
    #endregion

    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject crashPanel;
    [SerializeField] private GameObject loseGameOverPanel;
    [SerializeField] private GameObject winGameOverPanel;
    [SerializeField] private GameObject movementCanvas;
    [SerializeField] private GameObject mathTaskGeneratorCanvas;
    [SerializeField] private GameObject gameplayControllerCanvas;

    [SerializeField] private SnakeHeadMovement snakeHeadMovement;
    [SerializeField] private GameplayController gameplayController;
    [SerializeField] private GameOverManager gameOverManager;

    [SerializeField] private int gameSizeX = 23;
    [SerializeField] private int gameSizeY = 23;

    private float crashEffectDelayTime = 1.25f;

    public int GameSizeX { get { return gameSizeX; } }
    public int GameSizeY { get { return gameSizeY; } }

    private void Start()
    {
        levelPanel.SetActive(true);

        crashPanel.SetActive(false);
        movementCanvas.SetActive(false);
        mathTaskGeneratorCanvas.SetActive(false);
        gameplayControllerCanvas.SetActive(false);
        loseGameOverPanel.SetActive(false);
        winGameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGameButton();
        }
    }

    private void RemoveSnakeLife()
    {
        gameplayController.RemoveLife(); 
    }

    private IEnumerator ActivateCrashEffectDelay()
    {
        mathTaskGeneratorCanvas.SetActive(false);
        movementCanvas.SetActive(false);
        gameplayControllerCanvas.SetActive(false);

        if (gameplayController.IsSnakeAlive)
        {
            crashPanel.SetActive(true);

            yield return new WaitForSeconds(crashEffectDelayTime);

            mathTaskGeneratorCanvas.SetActive(true);
            movementCanvas.SetActive(true);
            gameplayControllerCanvas.SetActive(true);

            crashPanel.SetActive(false);

            snakeHeadMovement.ResumeMovingSnakeHead();
        }
    }

    public void StartLevelButton()
    {
        levelPanel.SetActive(false);

        movementCanvas.SetActive(true);
        mathTaskGeneratorCanvas.SetActive(true);
        gameplayControllerCanvas.SetActive(true);

        snakeHeadMovement.StartMovingSnakeHead();
    }

    public void StopGame()
    {
        RemoveSnakeLife();
        StartCoroutine(ActivateCrashEffectDelay());
        snakeHeadMovement.StopMovingSnakeHead();
    }

    public void LoseGameOver()
    {
        snakeHeadMovement.StopMovingSnakeHead();

        crashPanel.SetActive(false);

        loseGameOverPanel.SetActive(true);
    }

    public void WinGameOver()
    {
        snakeHeadMovement.StopMovingSnakeHead();

        mathTaskGeneratorCanvas.SetActive(false);
        movementCanvas.SetActive(false);
        gameplayControllerCanvas.SetActive(false);

        winGameOverPanel.SetActive(true);

        gameOverManager.AssignFinalScore(gameplayController.Points);
    }

    public void RestartGameButton()
    {
        loseGameOverPanel.SetActive(false);

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }

    public void AssignSnakePoints()
    {
        gameplayController.AssignPoints();
    }

    public void SetNormalSpeed()
    {
        snakeHeadMovement.AssignNormalSpeed();
    }

    public void SetFastSpeed()
    {
        snakeHeadMovement.AssignFastSpeed();
    }
}
