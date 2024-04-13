using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBalanceManager : MonoBehaviour
{
    public static LevelBalanceManager Instance { get; private set; }

    public int sunHp = 100;

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
}
