using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathNavMesh : MonoBehaviour
{
    [HideInInspector] public float moveSpeed = 5f;
    [HideInInspector] public float stoppingDistance = 5f;

    private Sun sun;
    private Renderer sunRenderer;
    private Rigidbody rb;
    public bool stopped = false;

    public NavMeshAgent agent;

    void Start()
    {
        sun = PlanetManager.Instance.sun;
        sunRenderer = sun.GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        agent.enabled = true;
    }

    void FixedUpdate()
    {
        CheckForStop();
        if (!stopped)
        {
            agent.SetDestination(sun.transform.position);
        }
    }

    void CheckForStop()
    {
        Vector3 direction = sun.transform.position - transform.position;

        float distanceToSun = direction.magnitude;
        if (distanceToSun <= stoppingDistance + sunRenderer.bounds.size.x / 2 && !stopped)
        {
            stopped = true;
            agent.enabled = false;
        }
    }
}
