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

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyType : ScriptableObject
{
    public int Damage;
    public int Hp;
    public int Range;
    public float AttackSpeed;
    public int HealPower;
    public float HealSpeed;
    public float MoveSpeed;
    public float BulletSpeed;
    public int Reward;
}
