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

    [HideInInspector] public Renderer towerRenderer;

    [Header("References")]
    [SerializeField] private TowerHp towerHp;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Sounds")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip takeDamageSound;

    private Planet parentPlanet;

    private void Start()
    {
        parentPlanet = GetComponentInParent<Planet>();
        TowerType towerType = PrefabManager.towerTypes[towerName];
        level = towerType.Levels[0].Level;
        damage = towerType.Levels[0].Damage;
        range = towerType.Levels[0].Range;
        upgradePrice = towerType.Levels[0].UpgradePrice;
        attackSpeed = towerType.Levels[0].AttackSpeed;
        maxHealth = towerType.Levels[0].Health;
        bulletSpeed = towerType.Levels[0].BulletSpeed;
        hp = maxHealth;

        StartCoroutine(Shoot());
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        towerRenderer = GetComponent<Renderer>();
        towerRenderer.material = MaterialsManager.Instance.materials[0];
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
            yield return new WaitForSeconds(1 / attackSpeed);
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

    public bool Upgrade()
    {
        TowerType towerType = PrefabManager.towerTypes[towerName];
        if(level < towerType.Levels.Count)
        {
            if (towerType.Levels[level].UpgradePrice > LevelBalanceManager.Instance.coins)
            {
                return false;
            }
            level++;
            damage = towerType.Levels[level - 1].Damage;
            range = towerType.Levels[level - 1].Range;
            upgradePrice = towerType.Levels[level - 1].UpgradePrice;
            attackSpeed = towerType.Levels[level - 1].AttackSpeed;
            maxHealth = towerType.Levels[level - 1].Health;
            towerRenderer.material = MaterialsManager.Instance.materials[level - 1];
            hp = maxHealth;
            towerHp.UpdateHp(1);
            LevelBalanceManager.Instance.UpdateCoins(-upgradePrice);
        }
        if(level == towerType.Levels.Count)
        {
            parentPlanet.upgradeTowerIcon.SetLock();
        }
        return true;
    }

    private void Destroy()
    {
        parentPlanet.DestroyTower();
        Destroy(gameObject);
    }
}
