using UnityEngine;
using System.Collections;
using UnityEditor;

public class EnemyPath : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float stoppingDistance = 5f;
    public float shootInterval = 1f;
    public float shootSpeed = 20f;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootingPoint;
    private GameObject sun;
    private GameObject[] planets;
    private Rigidbody rb;
    private bool stopped = false;

    void Start()
    {
        sun = PlanetManager.Instance.sun;
        planets = PlanetManager.Instance.planets;
        if (sun == null)
        {
            Debug.LogError("Sun reference not found!");
            return;
        }

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found!");
            return;
        }

        rb.useGravity = false;

        StartCoroutine(ShootAtTarget());
    }

    void FixedUpdate()
    {
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = sun.transform.position - transform.position;

        float distanceToSun = direction.magnitude;
        if (distanceToSun <= stoppingDistance && !stopped)
        {
            rb.velocity = Vector3.zero;
            stopped = true;
        }

        if (!stopped)
        {
            direction.Normalize();
            rb.velocity = direction * moveSpeed;
        }
    }

    IEnumerator ShootAtTarget()
    {
        while (true)
        {
            if (stopped)
            {
                Shoot(sun);
            }
            else
            {
                GameObject closestPlanet = FindClosestPlanet();
                if (closestPlanet != null)
                {
                    Shoot(closestPlanet);
                }
                else
                {
                    Shoot(sun);
                }
            }
            yield return new WaitForSeconds(shootInterval);
        }
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
            bulletComponent.speed = shootSpeed;
            bulletComponent.SetTarget(target);
        }
    }
}
