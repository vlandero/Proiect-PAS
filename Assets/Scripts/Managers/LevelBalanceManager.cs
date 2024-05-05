using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBalanceManager : MonoBehaviour
{
    public static LevelBalanceManager Instance { get; private set; }

    public int sunHp = 100;
    public int coins = 100;
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CanvasManager.instance.mainGui.SetCoins(coins);
    }

    public void UpdateCoins(int amount)
    {
        coins += amount;
        CanvasManager.instance.mainGui.SetCoins(coins);
    }
}
