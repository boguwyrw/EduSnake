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

    [SerializeField] private GameObject gameManagerCanvas;
    [SerializeField] private GameObject movementCanvas;
    [SerializeField] private GameObject mathTaskGeneratorCanvas;

    [SerializeField] private SnakeMovement snakeMovement;

    [SerializeField] private int gameSizeX = 23;
    [SerializeField] private int gameSizeY = 23;

    public int GameSizeX { get { return gameSizeX; } }
    public int GameSizeY { get { return gameSizeY; } }

    private void Start()
    {
        gameManagerCanvas.SetActive(true);

        movementCanvas.SetActive(false);
        mathTaskGeneratorCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void StartLevelButton()
    {
        gameManagerCanvas.SetActive(false);

        movementCanvas.SetActive(true);
        mathTaskGeneratorCanvas.SetActive(true);

        snakeMovement.StartGame();
    }
}
