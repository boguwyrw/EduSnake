using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject crashPanel;
    [SerializeField] private GameObject movementCanvas;
    [SerializeField] private GameObject mathTaskGeneratorCanvas;
    [SerializeField] private GameObject gameplayControllerCanvas;

    [SerializeField] private SnakeHeadMovement snakeHeadMovement;
    [SerializeField] private GameplayController gameplayController;

    [SerializeField] private int gameSizeX = 23;
    [SerializeField] private int gameSizeY = 23;

    public int GameSizeX { get { return gameSizeX; } }
    public int GameSizeY { get { return gameSizeY; } }

    private void Start()
    {
        levelPanel.SetActive(true);

        movementCanvas.SetActive(false);
        mathTaskGeneratorCanvas.SetActive(false);
        gameplayControllerCanvas.SetActive(false);
        crashPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void RemoveSnakeLife()
    {
        gameplayController.RemoveLife();
        mathTaskGeneratorCanvas.SetActive(false);
        movementCanvas.SetActive(false);
        crashPanel.SetActive(true);
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
        snakeHeadMovement.StopMovingSnakeHead();
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
