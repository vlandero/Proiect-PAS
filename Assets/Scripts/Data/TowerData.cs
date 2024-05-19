using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TowerLevel
{
    public int Level;
    public int Damage;
    public int Range;
    public int Health;
    public float BulletSpeed;
    public int UpgradePrice;
    public float AttackSpeed;
}

[Serializable]
[CreateAssetMenu(fileName = "TowerData", menuName = "ScriptableObjects/TowerData")]
public class TowerType : ScriptableObject
{
    public string Name;
    public List<TowerLevel> Levels = new List<TowerLevel>();

    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;
}

public enum TowerNameType
{
    Sentinel,
    Frostbite
}
