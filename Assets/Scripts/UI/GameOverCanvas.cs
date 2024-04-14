using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCanvas : MonoBehaviour
{
    [SerializeField] private GameObject winComponent;
    [SerializeField] private GameObject loseComponent;

    private void Start()
    {
        winComponent.SetActive(false);
        loseComponent.SetActive(false);
    }

    public void ShowWin()
    {
        winComponent.SetActive(true);
        PauseManager.instance.Pause();
    }

    public void ShowLose()
    {
        loseComponent.SetActive(true);
        PauseManager.instance.Pause();
    }
}
