using System.Collections;
using UnityEngine;

public class BulletEnemy : Enemy
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform shootingPoint;

    public int bulletSpeed;
    protected override IEnumerator ShootAtTarget()
    {
        while (true)
        {
            if (enemyPath.IsStopped)
            {
                Shoot(sun);
            }
            else
            {
                Planet closestPlanet = FindClosestPlanetWithTower();
                if (closestPlanet != null)
                {
                    if(IsInRange(closestPlanet.gameObject))
                    {
                        Shoot(closestPlanet);
                    }
                }
                else if(IsInRange(sun.gameObject))
                {
                    Shoot(sun);
                }
            }
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    bool IsInRange(GameObject planet)
    {
        float distance = Vector3.Distance(transform.position, planet.transform.position);
        return distance <= range;
    }

    Planet FindClosestPlanetWithTower()
    {
        Planet closestPlanet = null;
        float closestDistance = Mathf.Infinity;
        foreach (Planet planet in planets)
        {
            float distance = Vector3.Distance(transform.position, planet.transform.position);
            if (distance < closestDistance && planet.HasTower())
            {
                closestDistance = distance;
                closestPlanet = planet;
            }
        }
        return closestPlanet;
    }

    void Shoot(GameObject target)
    {
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);

        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        if (bulletComponent != null)
        {
            bulletComponent.speed = bulletSpeed;
            bulletComponent.damage = damage;
            bulletComponent.attackTarget = AttackTarget.Tower;
            bulletComponent.SetTarget(target.gameObject);
        }
    }

    void Shoot(Sun sun)
    {
        Shoot(sun.gameObject);
    }

    void Shoot(Planet planet)
    {
        Shoot(planet.tower.gameObject);
    }
}
