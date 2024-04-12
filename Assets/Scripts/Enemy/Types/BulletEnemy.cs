using System.Collections;
using System.Collections.Generic;
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
                GameObject closestPlanet = FindClosestPlanet();
                if (closestPlanet != null)
                {
                    if(IsPlanetInRange(closestPlanet))
                    {
                        Shoot(closestPlanet);
                    }
                }
                else
                {
                    Shoot(sun);
                }
            }
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    bool IsPlanetInRange(GameObject planet)
    {
        float distance = Vector3.Distance(transform.position, planet.transform.position);
        return distance <= range;
    }

    GameObject FindClosestPlanet()
    {
        GameObject closestPlanet = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject planet in planets)
        {
            float distance = Vector3.Distance(transform.position, planet.transform.position);
            if (distance < closestDistance)
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
            bulletComponent.SetTarget(target);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
