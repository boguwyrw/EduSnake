using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    /// <summary>
    /// Method assign to UI button responsible for loading first game scene
    /// </summary>
    public void PlaySnakeGameButton()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Method assign to UI button responsible for turn off aplication
    /// </summary>
    public void QuitSnakeGameButton()
    {
        Application.Quit();
    }
}
