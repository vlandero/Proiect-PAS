using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct EnemyPrefabInstance
{
    public EnemyNameType name;
    public GameObject prefab;
}

[Serializable]
public struct TowerPrefabInstance
{
    public TowerNameType name;
    public GameObject prefab;
}

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager instance;
    public static Dictionary<EnemyNameType, GameObject> enemyPrefabs = new Dictionary<EnemyNameType, GameObject>();
    public static Dictionary<TowerNameType, GameObject> towerPrefabs = new Dictionary<TowerNameType, GameObject>();
    public EnemyPrefabInstance[] enemyPrefabsList;
    public TowerPrefabInstance[] towerPrefabsList;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (EnemyPrefabInstance prefab in enemyPrefabsList)
        {
            if(enemyPrefabs.ContainsKey(prefab.name))
            {
                continue;
            }
            enemyPrefabs.Add(prefab.name, prefab.prefab);
        }

        foreach (TowerPrefabInstance prefab in towerPrefabsList)
        {
            if(towerPrefabs.ContainsKey(prefab.name))
            {
                continue;
            }
            towerPrefabs.Add(prefab.name, prefab.prefab);
        }
    }
}
