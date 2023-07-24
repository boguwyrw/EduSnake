using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
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

    #region SerializeField Variables
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject crashPanel;
    [SerializeField] private GameObject loseGameOverPanel;
    [SerializeField] private GameObject winGameOverPanel;
    [SerializeField] private GameObject mathTaskGeneratorCanvas;
    [SerializeField] private GameObject gameplayControllerCanvas;
    [SerializeField] private GameObject joystickGO;

    [SerializeField] private TMP_Text levelNumberText;

    [SerializeField] private SnakeHeadMovement snakeHeadMovement;
    [SerializeField] private GameplayController gameplayController;
    [SerializeField] private GameOverManager gameOverManager;
    [SerializeField] private MathTaskGenerator mathTaskGenerator;
    [SerializeField] private SnakeParticleEffects snakeParticleEffects;
    [SerializeField] private SnakeOnFireController snakeOnFireController;

    [SerializeField] private int gameSizeX = 23;
    [SerializeField] private int gameSizeY = 23;
    #endregion

    #region PC vs. Android Settings
    [SerializeField] private GameObject startGameLevelTextGO;
    [SerializeField] private GameObject startLevelButtonGO;
    #endregion

    private float crashEffectDelayTime = 1.5f;

    private int currentSceneIndex = -1;
    public int CurrentSceneIndex { get { return currentSceneIndex; } }

    public int GameSizeX { get { return gameSizeX; } }
    public int GameSizeY { get { return gameSizeY; } }

    private void Start()
    {
        fadePanel.SetActive(true);
        startPanel.SetActive(true);

        levelPanel.SetActive(false);
        settingsPanel.SetActive(false);
        crashPanel.SetActive(false);
        joystickGO.SetActive(false);
        mathTaskGeneratorCanvas.SetActive(false);
        gameplayControllerCanvas.SetActive(false);
        loseGameOverPanel.SetActive(false);
        winGameOverPanel.SetActive(false);

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelNumberText.text = "Level " + currentSceneIndex.ToString();

        if (MainGameManager.IsGameRestarted)
        {
            MainGameManager.IsGameRestarted = false;

            TurnOnLevelPanel();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenuButton();
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.C))
        {
            DeleteSavesButton();
        }
#endif
        if (Input.touchCount > 0 && levelPanel.activeSelf)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                StartGameLevel();
            }
        }
    }

    private IEnumerator ActivateCrashEffectDelay()
    {
        mathTaskGeneratorCanvas.SetActive(false);
        joystickGO.SetActive(false);
        gameplayControllerCanvas.SetActive(false);

        yield return new WaitUntil(() => snakeParticleEffects.GetWrongParticleEffectStopped());
        fadePanel.SetActive(true);
        crashPanel.SetActive(true);

        yield return new WaitForSeconds(crashEffectDelayTime);
        gameOverManager.ActivateDeactivateFade(true);
        fadePanel.SetActive(false);
        LoseGameOver();
    }

    public void RemoveSnakeLife()
    {
        gameplayController.RemoveLife();
    }
    public void StartGameLevel()
    {
        fadePanel.SetActive(false);
        levelPanel.SetActive(false);

        joystickGO.SetActive(true);
        mathTaskGeneratorCanvas.SetActive(true);
        gameplayControllerCanvas.SetActive(true);

        snakeHeadMovement.StartMovingSnakeHead();
    }

    public void StopGame()
    {
        ActivateStopMovingSnakeHead();

        RemoveSnakeLife();
        StartCoroutine(ActivateCrashEffectDelay());
    }

    public void ActivateStopMovingSnakeHead()
    {
        snakeHeadMovement.StopMovingSnakeHead();
    }

    public void LoseGameOver()
    {
        ActivateStopMovingSnakeHead();

        crashPanel.SetActive(false);
        joystickGO.SetActive(false);
        mathTaskGeneratorCanvas.SetActive(false);
        gameplayControllerCanvas.SetActive(false);

        loseGameOverPanel.SetActive(true);

        gameOverManager.AssignFinalScore(gameplayController.Points);
    }

    public void WinGameOver()
    {
        ActivateStopMovingSnakeHead();

        mathTaskGeneratorCanvas.SetActive(false);
        joystickGO.SetActive(false);
        gameplayControllerCanvas.SetActive(false);

        gameOverManager.ActivateDeactivateFade(true);
        winGameOverPanel.SetActive(true);

        gameOverManager.AssignFinalScore(gameplayController.Points);
    }

    public void ActivateParticleEffect(Vector3 particlePosition)
    {
        snakeParticleEffects.ActivateCorrectParticleEffect(particlePosition);
    }

    public void RestartGameButton()
    {
        loseGameOverPanel.SetActive(false);
        MainGameManager.IsGameRestarted = true;
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void NextLevelButton()
    {
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void TurnOnLevelPanel()
    {
        startPanel.SetActive(false);
        levelPanel.SetActive(true);
        fadePanel.SetActive(true);

#if UNITY_EDITOR
        startLevelButtonGO.SetActive(true);
        startGameLevelTextGO.SetActive(false);
#endif
    }

    public void TurnOnSettingsPanel()
    {
        startPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void BackButton()
    {
        settingsPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void AssignSnakePoints()
    {
        gameplayController.AssignPoints();
        mathTaskGenerator.ShowPlayerCorrectChoose();
    }

    public void SetNextSpeed()
    {
        snakeHeadMovement.AssignNextSpeed();
    }

    public void DeleteSavesButton()
    {
        PlayerPrefs.DeleteAll();
    }

    public void HideDirArrow()
    {
        snakeHeadMovement.HideDirectionArrow();
    }

    public void ShowDirArrow()
    {
        snakeHeadMovement.ShowDirectionArrow();
    }

    public void TurnOffSnakeOnFireEffect()
    {
        snakeOnFireController.TurnOffFireEffect();
    }

    public void TurnOnSnakeOnFireEffect()
    {
        snakeOnFireController.TurnOnFireEffect();
    }

    public float GetMaxTasksNumber()
    {
        return mathTaskGenerator.MaxTasksNumber;
    }

    public List<Transform> GetAllSnakeParts()
    {
        return snakeHeadMovement.AllSnakeParts();
    }

    public Transform GetCorrectAnswer()
    {
        return mathTaskGenerator.CorrectAnswer();
    }

    public bool GetAreAnswersSpawned()
    {
        return mathTaskGenerator.AreAnswersSpawned;
    }

    public float GetDetectionRange()
    {
        return mathTaskGenerator.DetectionRange;
    }
}
