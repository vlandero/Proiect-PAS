using UnityEngine;
using System.Collections;
using UnityEditor;

public class EnemyPath : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float stoppingDistance = 5f;

    private GameObject sun;
    private Renderer sunRenderer;
    private Rigidbody rb;
    private bool stopped = false;

    public bool IsStopped { get { return stopped; } }

    void Start()
    {
        sun = PlanetManager.Instance.sun;
        sunRenderer = sun.GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = sun.transform.position - transform.position;

        float distanceToSun = direction.magnitude;
        if (distanceToSun <= stoppingDistance + sunRenderer.bounds.size.x / 2 && !stopped)
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
}
