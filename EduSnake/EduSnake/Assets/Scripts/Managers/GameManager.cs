using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    // TO DO
    // 1. System zapisu
    // 2. Efekty Do Tween

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

    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject crashPanel;
    [SerializeField] private GameObject loseGameOverPanel;
    [SerializeField] private GameObject winGameOverPanel;
    [SerializeField] private GameObject mathTaskGeneratorCanvas;
    [SerializeField] private GameObject gameplayControllerCanvas;
    [SerializeField] private GameObject joystickGO;

    [SerializeField] private Toggle rightToggle;
    [SerializeField] private Toggle leftToggle;

    [SerializeField] private SnakeHeadMovement snakeHeadMovement;
    [SerializeField] private GameplayController gameplayController;
    [SerializeField] private GameOverManager gameOverManager;
    [SerializeField] private MathTaskGenerator mathTaskGenerator;
    [SerializeField] private MovementJoystickManager movementJoystickManager;

    [SerializeField] private int gameSizeX = 23;
    [SerializeField] private int gameSizeY = 23;

    private float crashEffectDelayTime = 1.25f;

    public int GameSizeX { get { return gameSizeX; } }
    public int GameSizeY { get { return gameSizeY; } }

    private void Start()
    {
        startPanel.SetActive(true);

        levelPanel.SetActive(false);
        settingsPanel.SetActive(false);
        crashPanel.SetActive(false);
        joystickGO.SetActive(false);
        mathTaskGeneratorCanvas.SetActive(false);
        gameplayControllerCanvas.SetActive(false);
        loseGameOverPanel.SetActive(false);
        winGameOverPanel.SetActive(false);

        TogglesListeners();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGameButton();
        }
    }

    private void TogglesListeners()
    {
        rightToggle.onValueChanged.AddListener(delegate {
            RightToggleValueChanged(rightToggle);
        });

        leftToggle.onValueChanged.AddListener(delegate {
            LeftToggleValueChanged(leftToggle);
        });
    }

    private void RightToggleValueChanged(Toggle rightToggle)
    {
        movementJoystickManager.RightSideJoystick();
    }

    private void LeftToggleValueChanged(Toggle leftToggle)
    {
        movementJoystickManager.LeftSideJoystick();
    }

    private IEnumerator ActivateCrashEffectDelay()
    {
        mathTaskGeneratorCanvas.SetActive(false);
        joystickGO.SetActive(false);
        gameplayControllerCanvas.SetActive(false);

        if (gameplayController.IsSnakeAlive)
        {
            crashPanel.SetActive(true);

            yield return new WaitForSeconds(crashEffectDelayTime);

            mathTaskGenerator.SpawnAnswers();

            mathTaskGeneratorCanvas.SetActive(true);
            joystickGO.SetActive(true);
            gameplayControllerCanvas.SetActive(true);

            crashPanel.SetActive(false);

            mathTaskGenerator.ShowAllAnswers();
            snakeHeadMovement.ResumeMovingSnakeHead();
        }
    }

    public void RemoveSnakeLife()
    {
        gameplayController.RemoveLife();
    }
    public void StartLevelButton()
    {
        levelPanel.SetActive(false);

        joystickGO.SetActive(true);
        mathTaskGeneratorCanvas.SetActive(true);
        gameplayControllerCanvas.SetActive(true);

        snakeHeadMovement.StartMovingSnakeHead();
    }

    public void StopGame()
    {
        snakeHeadMovement.StopMovingSnakeHead();

        RemoveSnakeLife();
        StartCoroutine(ActivateCrashEffectDelay());
    }

    public void LoseGameOver()
    {
        snakeHeadMovement.StopMovingSnakeHead();

        crashPanel.SetActive(false);
        joystickGO.SetActive(false);
        mathTaskGeneratorCanvas.SetActive(false);
        gameplayControllerCanvas.SetActive(false);

        loseGameOverPanel.SetActive(true);

        gameOverManager.AssignFinalScore(gameplayController.Points);
    }

    public void WinGameOver()
    {
        snakeHeadMovement.StopMovingSnakeHead();

        mathTaskGeneratorCanvas.SetActive(false);
        joystickGO.SetActive(false);
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

    public void TurnOnLevelPanel()
    {
        startPanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    public void TurnOnSettingsPanel()
    {
        startPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void SideOkButton()
    {
        settingsPanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    public void AssignSnakePoints()
    {
        gameplayController.AssignPoints();
        mathTaskGenerator.ShowPlayerCorrectChoose();
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
