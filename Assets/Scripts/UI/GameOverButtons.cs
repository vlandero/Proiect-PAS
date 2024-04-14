using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(Scenes.MainMenu);
    }

    public void GoToLevelSelector()
    {
        SceneManager.LoadScene(Scenes.LevelSelector);
    }
}
