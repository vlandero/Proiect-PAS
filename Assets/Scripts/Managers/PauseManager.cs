using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;

    private static bool isPaused = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void Pause()
    {
        isPaused = true;
        Time.timeScale = 0.0f;
    }

    public static void Resume()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
    }

    public static void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = 1 - Time.timeScale;
    }

    public static bool IsPaused()
    {
        return isPaused;
    }
}
