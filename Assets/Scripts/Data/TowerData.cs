using System;
using System.Collections.Generic;

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
    public List<TowerLevel> Levels = new List<TowerLevel>();
}

public enum TowerNameType
{
    Sentinel,
    Frostbite
}

public static class TowerData
{
    public static Dictionary<TowerNameType, TowerType> TowerTypes = new Dictionary<TowerNameType, TowerType>
    {
        {
            TowerNameType.Sentinel, new TowerType {
                Name = "Sentinel",
                Levels = new List<TowerLevel>
                {
                    new TowerLevel {
                        Level = 1,
                        Damage = 10,
                        Range = 50,
                        Health = 105,
                        BulletSpeed = 10,
                        AttackSpeed = 1.5f,
                        UpgradePrice = 100
                    },
                    new TowerLevel {
                        Level = 2,
                        Damage = 15,
                        Range = 50,
                        Health = 120,
                        BulletSpeed = 10,
                        AttackSpeed = 2.0f,
                        UpgradePrice = 100
                    }
                }
            }
        },
        {
            TowerNameType.Frostbite, new TowerType {
                Name = "Frostbite",
                Levels = new List<TowerLevel>
                {
                    new TowerLevel {
                        Level = 1,
                        Damage = 10,
                        Range = 50,
                        Health = 20,
                        BulletSpeed = 10,
                        AttackSpeed = 1.5f,
                        UpgradePrice = 100
                    },
                    new TowerLevel {
                        Level = 2,
                        Damage = 15,
                        Range = 50,
                        Health = 115,
                        BulletSpeed = 10,
                        AttackSpeed = 2.0f,
                        UpgradePrice = 100
                    }

                }
            }
        }
    };
}
