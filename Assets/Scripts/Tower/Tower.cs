using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerNameType towerName;
    [Header("Tower Stats")]
    public int level;
    public int damage;
    public int range;
    public int upgradePrice;
    public float attackSpeed;
    public float maxHealth;
    public float hp;

    [SerializeField] private TowerHp towerHp;
    [SerializeField] private Planet parentPlanet;

    private void Start()
    {
        parentPlanet = GetComponentInParent<Planet>();
        TowerType towerType = TowerData.TowerTypes[towerName];
        level = towerType.Levels[0].Level;
        damage = towerType.Levels[0].Damage;
        range = towerType.Levels[0].Range;
        upgradePrice = towerType.Levels[0].UpgradePrice;
        attackSpeed = towerType.Levels[0].AttackSpeed;
        maxHealth = towerType.Levels[0].Health;
        hp = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        towerHp.UpdateHp(hp / maxHealth);
        if(hp <= 0)
        {
            Destroy();
        }
    }

    public void Upgrade()
    {
        TowerType towerType = TowerData.TowerTypes[towerName];
        if(level < towerType.Levels.Count - 1)
        {
            level++;
            damage = towerType.Levels[level - 1].Damage;
            range = towerType.Levels[level - 1].Range;
            upgradePrice = towerType.Levels[level - 1].UpgradePrice;
            attackSpeed = towerType.Levels[level - 1].AttackSpeed;
            maxHealth = towerType.Levels[level - 1].Health;
            hp = maxHealth;
            towerHp.UpdateHp(1);
        }
    }

    private void Destroy()
    {
        parentPlanet.DestroyTower();
        Destroy(gameObject);
    }
}
