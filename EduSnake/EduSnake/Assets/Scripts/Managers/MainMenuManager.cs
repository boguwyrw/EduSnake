using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void PlaySnakeGameButton()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitSnakeGameButton()
    {
        Application.Quit();
    }
}
