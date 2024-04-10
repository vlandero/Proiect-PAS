using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType
{
    public int Damage;
    public int Hp;
    public int Range;
    public float AttackSpeed;
    public int HealPower;
    public float HealSpeed;
    public float MoveSpeed;
}

public static class EnemyData
{
    public static Dictionary<string, EnemyType> enemyTypes = new Dictionary<string, EnemyType>
    {
        {
            "Nebula Raider", new EnemyType {
                Damage = 10,
                Hp = 20,
                Range = 30,
                AttackSpeed = 1.2f,
                HealPower = 3,
                HealSpeed = 10,
                MoveSpeed = 5
            }
        },
        {
            "Lunar Lurker", new EnemyType {
                Damage = 10,
                Hp = 20,
                Range = 30,
                AttackSpeed = 1.2f,
                HealPower = 3,
                HealSpeed = 10,
                MoveSpeed = 5
            }
        },
        {
            "Astro Assassin", new EnemyType {
                Damage = 10,
                Hp = 20,
                Range = 30,
                AttackSpeed = 1.2f,
                HealPower = 3,
                HealSpeed = 10,
                MoveSpeed = 5
            }
        },
    };
}
