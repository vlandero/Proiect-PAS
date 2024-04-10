using System;
using System.Collections.Generic;

[Serializable]
public class TowerLevel
{
    public int Level;
    public int Damage;
    public int Range;
    public int UpgradePrice;
    public float AttackSpeed;
}

[Serializable]
public class TowerType
{
    public string Name;
    public List<TowerLevel> Levels = new List<TowerLevel>();
}

public static class TowerData
{
    public static Dictionary<string, TowerType> TowerTypes = new Dictionary<string, TowerType>
    {
        {
            "Sentinel", new TowerType {
                Name = "Sentinel",
                Levels = new List<TowerLevel>
                {
                    new TowerLevel {
                        Level = 1,
                        Damage = 10,
                        Range = 20,
                        AttackSpeed = 1.5f,
                        UpgradePrice = 100
                    },
                    new TowerLevel {
                        Level = 2,
                        Damage = 15,
                        Range = 25,
                        AttackSpeed = 2.0f,
                        UpgradePrice = 100
                    }
                }
            }
        },
        {
            "Frostbite", new TowerType {
                Name = "Frostbite",
                Levels = new List<TowerLevel>
                {
                    new TowerLevel {
                        Level = 1,
                        Damage = 10,
                        Range = 20,
                        AttackSpeed = 1.5f,
                        UpgradePrice = 100
                    },
                    new TowerLevel {
                        Level = 2,
                        Damage = 15,
                        Range = 25,
                        AttackSpeed = 2.0f,
                        UpgradePrice = 100
                    }

                }
            }
        }
    };
}
