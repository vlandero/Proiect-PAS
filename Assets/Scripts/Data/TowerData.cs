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
public class TowerType
{
    public string Name;
    public TowerLevel[] Levels;
}

[Serializable]
public enum TowerNameType
{
    Sentinel,
    Frostbite
}
