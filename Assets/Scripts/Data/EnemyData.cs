using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyNameType
{
    NebulaRaider,
    LunarLurker,
    AstroAssassin
}

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
    public static Dictionary<EnemyNameType, EnemyType> enemyTypes = new Dictionary<EnemyNameType, EnemyType>
    {
        {
            EnemyNameType.NebulaRaider, new EnemyType {
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
            EnemyNameType.LunarLurker, new EnemyType {
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
            EnemyNameType.AstroAssassin, new EnemyType {
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
