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
    [SerializeField] private GameOverUIDisplayManager gameOverManager;
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

        if (GameRestartedManager.IsGameRestarted)
        {
            GameRestartedManager.IsGameRestarted = false;

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

    /// <summary>
    /// Coroutine responsible for delay crash effect panel during the game
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Method with reference to RemoveLife() in GameplayController responsible for decreasing lives during game and display them in UI element
    /// </summary>
    public void RemoveSnakeLife()
    {
        gameplayController.RemoveLife();
    }

    /// <summary>
    /// Method responsible for controlling panels during start playing game
    /// </summary>
    public void StartGameLevel()
    {
        fadePanel.SetActive(false);
        levelPanel.SetActive(false);

        joystickGO.SetActive(true);
        mathTaskGeneratorCanvas.SetActive(true);
        gameplayControllerCanvas.SetActive(true);

        snakeHeadMovement.StartMovingSnakeHead();
    }

    /// <summary>
    /// Method that brings together other methods that are activated when game is stopped
    /// </summary>
    public void StopGame()
    {
        ActivateStopMovingSnakeHead();

        RemoveSnakeLife();
        StartCoroutine(ActivateCrashEffectDelay());
    }

    /// <summary>
    /// Method with reference to StopMovingSnakeHead() in SnakeHeadMovement responsible for changing movement and rotation variables to stop snake
    /// </summary>
    public void ActivateStopMovingSnakeHead()
    {
        snakeHeadMovement.StopMovingSnakeHead();
    }

    /// <summary>
    /// Method responsible for controlling panels and assigning scores when player lose game
    /// </summary>
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

    /// <summary>
    /// Method responsible for controlling panels and assigning scores when player win game
    /// </summary>
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

    /// <summary>
    /// Method with reference to ActivateCorrectParticleEffect() in SnakeParticleEffects responsible for activate particle effect when player choose correct answer
    /// </summary>
    /// <param name="particlePosition"></param>
    public void ActivateParticleEffect(Vector3 particlePosition)
    {
        snakeParticleEffects.ActivateCorrectParticleEffect(particlePosition);
    }

    /// <summary>
    /// Method assign to UI button responsible for restart game
    /// </summary>
    public void RestartGameButton()
    {
        loseGameOverPanel.SetActive(false);
        GameRestartedManager.IsGameRestarted = true;
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// Method assign to UI button responsible for loading next game level
    /// </summary>
    public void NextLevelButton()
    {
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    /// <summary>
    /// Method assign to UI button responsible for loading main menu
    /// </summary>
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Method responsible for showing level panels during game
    /// </summary>
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

    /// <summary>
    /// Method responsible for showing settings panel during game
    /// </summary>
    public void TurnOnSettingsPanel()
    {
        startPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    /// <summary>
    /// Method assign to UI button responsible for backing from settings panel to game panel
    /// </summary>
    public void BackButton()
    {
        settingsPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    /// <summary>
    /// Method with reference to AssignPoints() in GameplayController and ShowPlayerCorrectChoose() in MathTaskGenerator responsible for assigning points and showing players's correct choice
    /// </summary>
    public void AssignSnakePoints()
    {
        gameplayController.AssignPoints();
        mathTaskGenerator.ShowPlayerCorrectChoose();
    }

    /// <summary>
    /// Method with reference to AssignNextSpeed() in SnakeHeadMovement responsible for increased snake speed
    /// </summary>
    public void SetNextSpeed()
    {
        snakeHeadMovement.AssignNextSpeed();
    }

    /// <summary>
    /// Method assign to UI button responsible for deleting all saves in game
    /// </summary>
    public void DeleteSavesButton()
    {
        PlayerPrefs.DeleteAll();
    }

    /// <summary>
    /// Method with reference to HideDirectionArrow() in SnakeHeadMovement responsible for hidding direction arrow during game
    /// </summary>
    public void HideDirArrow()
    {
        snakeHeadMovement.HideDirectionArrow();
    }

    /// <summary>
    /// Method with reference to ShowDirectionArrow() in SnakeHeadMovement responsible for showing direction arrow during game
    /// </summary>
    public void ShowDirArrow()
    {
        snakeHeadMovement.ShowDirectionArrow();
    }

    /// <summary>
    /// Method with reference to TurnOffFireEffect() in SnakeOnFireController responsible for turn off snake on fire particle effect
    /// </summary>
    public void TurnOffSnakeOnFireEffect()
    {
        snakeOnFireController.TurnOffFireEffect();
    }

    /// <summary>
    /// Method with reference to TurnOnFireEffect() in SnakeOnFireController responsible for turn on snake on fire particle effect
    /// </summary>
    public void TurnOnSnakeOnFireEffect()
    {
        snakeOnFireController.TurnOnFireEffect();
    }

    /// <summary>
    /// Method responsible for returning max task number on current level
    /// </summary>
    /// <returns></returns>
    public float GetMaxTasksNumber()
    {
        return mathTaskGenerator.MaxTasksNumber;
    }

    /// <summary>
    /// Method responsible for returning current snake length as number of parts body
    /// </summary>
    /// <returns></returns>
    public List<Transform> GetAllSnakeParts()
    {
        return snakeHeadMovement.AllSnakeParts();
    }

    /// <summary>
    /// Method with reference to CorrectAnswer() in MathTaskGenerator responsible for returning correct answer transform
    /// </summary>
    /// <returns></returns>
    public Transform GetCorrectAnswer()
    {
        return mathTaskGenerator.CorrectAnswer();
    }

    /// <summary>
    /// Method with reference to AreAnswersSpawned in MathTaskGenerator responsible for returning information about spawned answers
    /// </summary>
    /// <returns></returns>
    public bool GetAreAnswersSpawned()
    {
        return mathTaskGenerator.AreAnswersSpawned;
    }

    /// <summary>
    /// Method with reference to DetectionRange in MathTaskGenerator returning detection range value
    /// </summary>
    /// <returns></returns>
    public float GetDetectionRange()
    {
        return mathTaskGenerator.DetectionRange;
    }
}
