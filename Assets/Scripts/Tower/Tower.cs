using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tower : MonoBehaviour
{
    public TowerNameType towerName;
    [Header("Tower Stats")]
    public int level; // START INDEX 1
    public int damage;
    public int range;
    public int upgradePrice;
    public float attackSpeed;
    public float bulletSpeed;
    public float maxHealth;
    public float hp;

    [Header("References")]
    [SerializeField] private TowerHp towerHp;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject bulletPrefab;

    private Planet parentPlanet;

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
        bulletSpeed = towerType.Levels[0].BulletSpeed;
        hp = maxHealth;

        StartCoroutine(Shoot());
    }

    private Enemy FindClosestEnemyInRange()
    {
        Enemy closestEnemy = null;
        foreach(var enemy in EnemyManager.Instance.enemies)
        {
            if(Vector3.Distance(transform.position, enemy.transform.position) <= range && !enemy.IsDying)
            {
                if(closestEnemy == null)
                {
                    closestEnemy = enemy;
                }
                else
                {
                    if(Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, closestEnemy.transform.position) && !enemy.IsDying)
                    {
                        closestEnemy = enemy;
                    }
                }
            }
        }
        return closestEnemy;
    }

    IEnumerator Shoot()
    {
        while(true)
        {
            Enemy closestEnemy = FindClosestEnemyInRange();
            if(closestEnemy != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
                Bullet bulletComponent = bullet.GetComponent<Bullet>();
                if (bulletComponent != null)
                {
                    bulletComponent.speed = bulletSpeed;
                    bulletComponent.damage = damage;
                    bulletComponent.attackTarget = AttackTarget.Spaceship;
                    bulletComponent.SetTarget(closestEnemy.targetPoint.gameObject);
                }
            }
            yield return new WaitForSeconds(attackSpeed);
        }
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
        if(level < towerType.Levels.Count)
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
        if(level == towerType.Levels.Count)
        {
            parentPlanet.upgradeTowerIcon.SetLock();
        }
    }

    private void Destroy()
    {
        parentPlanet.DestroyTower();
        Destroy(gameObject);
    }
}
