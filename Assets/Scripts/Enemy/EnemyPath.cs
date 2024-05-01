using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.Rendering;

public class EnemyPath : MonoBehaviour
{
    [HideInInspector] public float moveSpeed = 5f;
    [HideInInspector] public float stoppingDistance = 5f;

    private Sun sun;
    private Renderer sunRenderer;
    private Planet[] planets;
    private Renderer[] planetRenderers;
    private Rigidbody rb;
    private bool stopped = false;
    private float rotationSpeed = 5f;

    public enum AvoidanceAlgorithm
    {
        SingleRay,
        MultiRay,
    };
    public AvoidanceAlgorithm avoidanceAlgorithm = AvoidanceAlgorithm.MultiRay;

    public float steerToSunWeight = 1.5f;
    private Vector3 steerToSun;
    public float avoidPlanetsWeight = 1f;
    private Vector3 avoidPlanets;
    public float avoidDistance = 5f;

    public float avoidObstaclesWeight = 1f;
    public float steerWeight = 1f;

    public int numberOfRays = 7;
    public float viewAngle = 120f;
    private Vector3[] rays;

    public bool IsStopped { get { return stopped; } }

    void Start()
    {
        planets = PlanetManager.Instance.planets;
        planetRenderers = new Renderer[planets.Length];
        for (int i = 0; i < planets.Length; i++)
        {
            planetRenderers[i] = planets[i].planetMeshRenderer.GetComponent<Renderer>();
        }

        sun = PlanetManager.Instance.sun;
        sunRenderer = sun.GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;

        rays = new Vector3[numberOfRays];
        ComputeRays();
    }

    void ComputeRays()
    {
        for (int i = 0; i < numberOfRays; i++)
        {
            float angle = i * viewAngle / (numberOfRays - 1) - viewAngle / 2;
            Quaternion rotation = Quaternion.AngleAxis(angle, transform.up);
            rays[i] = rotation * transform.forward;
        }
    }

    // https://www.youtube.com/watch?v=SVazwHyfB7g
    private void OnDrawGizmos()
    {
        if (rays == null || rays.Length != numberOfRays)
        {
            rays = new Vector3[numberOfRays];
            ComputeRays();
        }
        for (int i = 0; i < numberOfRays; i++)
        {
            Gizmos.DrawRay(transform.position, rays[i].normalized * (avoidDistance + transform.localScale.x / 2));
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, steerToSun * avoidDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, avoidPlanets * avoidDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward.normalized * (avoidDistance + transform.localScale.x / 2));
    }

    void FixedUpdate()
    {
        ComputeRays();
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        if (ShouldStop())
        {
            rb.velocity = Vector3.zero;
            stopped = true;
        }

        if (!stopped)
        {
            Vector3 direction = Vector3.zero;
            if (avoidanceAlgorithm == AvoidanceAlgorithm.SingleRay)
            {
                steerToSun = steerToSunWeight * SteerToSun();
                avoidPlanets = avoidPlanetsWeight * AvoidPlanets();
                direction = steerToSun + avoidPlanets;
                rb.velocity = direction.normalized * moveSpeed;
            }
            else if (avoidanceAlgorithm == AvoidanceAlgorithm.MultiRay)
            {
                if (HitsObstacle())
                {
                    direction = AvoidObstacles();
                }
                else
                {
                    direction = SteerToSun();
                }
                rb.velocity = direction.normalized * moveSpeed;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    bool ShouldStop()
    {
        Vector3 direction = sun.transform.position - transform.position;
        float distanceToSun = direction.magnitude;
        return distanceToSun <= stoppingDistance + sunRenderer.bounds.size.x / 2;
    }

    Vector3 SteerToSun()
    {
        Vector3 direction = sun.transform.position - transform.position;
        return direction.normalized;
    }

    Vector3 AvoidPlanets()
    {
        Vector3 finalDirection = Vector3.zero;
        for (int i = 0; i < planets.Length; i++)
        {
            Vector3 directionToPlanet = planets[i].transform.position - transform.position;
            float planetRadius = planetRenderers[i].bounds.size.x / 2;
            float distanceToPlanet = directionToPlanet.magnitude - transform.localScale.x - planetRadius;
            float avoidanceRadius = planetRadius * 2;
            if (distanceToPlanet < avoidanceRadius)
            {
                float relativeX = 1f - Mathf.Clamp01(distanceToPlanet / avoidanceRadius);
                float steerStrength = -(relativeX * relativeX) + 2 * relativeX;
                steerStrength = relativeX;
                finalDirection += -directionToPlanet.normalized * steerStrength;
            }
        }
        finalDirection.y = 0;
        return finalDirection;
    }

    bool HitsObstacle()
    {
        for (int i = 0; i < numberOfRays; i++)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, rays[i]);
            if (Physics.Raycast(transform.position + rays[i].normalized * transform.localScale.x, rays[i], out hit, avoidDistance))
            {
                return true;
            }
        }
        return false;
    }

    Vector3 AvoidObstacles()
    {
        // Avoid direction is the weighted sum of all rays that hit an obstacle
        Vector3 avoidDirection = Vector3.zero;
        // Steer direction will be the direction of the ray closest to current direction, that doesn't hit an obstacle
        Vector3 steerDirection = Vector3.zero;
        float fullAvoidDistance = avoidDistance + transform.localScale.x / 2;
        for (int i = 0; i < numberOfRays; i++)
        {
            Vector3 ray = rays[i];
            RaycastHit hit;
            if (Physics.Raycast(transform.position + ray.normalized * transform.localScale.x, ray, out hit, avoidDistance))
            {
                float rayWeight = 1 - (Mathf.Clamp01(hit.distance / (fullAvoidDistance)));
                avoidDirection += -ray.normalized * rayWeight;
            }
            else if (steerDirection == Vector3.zero || Vector3.Angle(ray, transform.forward) < Vector3.Angle(steerDirection, transform.forward))
            {
                steerDirection = ray;
            }
        }

        // Final direction will be the weighted sum of avoid direction and steer direction
        Vector3 finalDirection = avoidObstaclesWeight * avoidDirection + steerWeight * steerDirection;

        return finalDirection;
    }
}
