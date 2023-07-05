using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameManagerCanvas;
    [SerializeField] private GameObject movementCanvas;
    [SerializeField] private GameObject mathTaskGeneratorCanvas;

    [SerializeField] private SnakeMovement snakeMovement;

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
