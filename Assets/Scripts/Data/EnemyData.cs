using System;

[Serializable]
public enum EnemyNameType
{
    NebulaRaider,
    LunarLurker,
    AstroAssassin
}

[Serializable]
public class EnemyType
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
