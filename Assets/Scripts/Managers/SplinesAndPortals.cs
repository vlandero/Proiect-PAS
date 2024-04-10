using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SplinesAndPortals : MonoBehaviour
{
    public static SplinesAndPortals Instance { get; private set; }
    public Spline spline;
    public GameObject spaceship;
    [SerializeField] private GameObject portalPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
    }
}
