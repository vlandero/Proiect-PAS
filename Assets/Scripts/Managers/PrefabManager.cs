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

[Serializable]
public struct ScriptableEnemyType
{
    public EnemyNameType name;
    public EnemyType type;
}

[Serializable]
public struct ScriptableTowerType
{
    public TowerNameType name;
    public TowerType type;
}

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager instance;
    public static Dictionary<EnemyNameType, GameObject> enemyPrefabs = new Dictionary<EnemyNameType, GameObject>();
    public static Dictionary<TowerNameType, GameObject> towerPrefabs = new Dictionary<TowerNameType, GameObject>();
    public static Dictionary<EnemyNameType, EnemyType> enemyTypes = new Dictionary<EnemyNameType, EnemyType>();
    public static Dictionary<TowerNameType, TowerType> towerTypes = new Dictionary<TowerNameType, TowerType>();
    public EnemyPrefabInstance[] enemyPrefabsList;
    public TowerPrefabInstance[] towerPrefabsList;
    public ScriptableEnemyType[] enemyTypesList;
    public ScriptableTowerType[] towerTypesList;

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

        foreach (ScriptableEnemyType type in enemyTypesList)
        {
            if(enemyTypes.ContainsKey(type.name))
            {
                continue;
            }
            enemyTypes.Add(type.name, type.type);
        }

        foreach (ScriptableTowerType type in towerTypesList)
        {
            if (towerTypes.ContainsKey(type.name))
            {
                continue;
            }
            towerTypes.Add(type.name, type.type);
        }
    }
}
