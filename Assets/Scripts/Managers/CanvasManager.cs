using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    public GameOverCanvas gameOverCanvas;
    public PauseCanvas pauseCanvas;
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

    private void Start()
    {
        gameOverCanvas.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseManager.instance.TogglePause();
            pauseCanvas.gameObject.SetActive(PauseManager.instance.IsPaused());
        }
    }
}