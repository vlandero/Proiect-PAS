using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCanvas : MonoBehaviour
{
    [SerializeField] private GameObject winComponent;
    [SerializeField] private GameObject loseComponent;

    public bool IsGameOver { get; private set; }

    private void Start()
    {
        winComponent.SetActive(false);
        loseComponent.SetActive(false);
    }

    public void ShowWin()
    {
        winComponent.SetActive(true);
        PauseManager.Pause();
    }

    public void ShowLose()
    {
        loseComponent.SetActive(true);
        PauseManager.Pause();
    }
}
