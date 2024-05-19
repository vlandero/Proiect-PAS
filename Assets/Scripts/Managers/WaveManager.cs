using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct Wave
{
    public float time;
    public EnemyNameType enemyType;
    public int enemyCount;
    public int spawnerIndex;
    public float timeBetweenEnemies;
}

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }
    [SerializeField] private Wave[] waves;
    [SerializeField] private GameObject waveSpawnersObject;

    public float timeToShowMarker = 30f;

    private Transform[] spawners;
    private List<WaypointData> waypoints = new List<WaypointData>();
    private int spawnerCount;
    private int waveIndex = 0;
    private float timeElapsed = 0;
    private Waypoint cameraWaypoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        spawnerCount = waveSpawnersObject.transform.childCount;
        spawners = new Transform[spawnerCount];
        for (int i = 0; i < spawnerCount; ++i)
        {
            spawners[i] = waveSpawnersObject.transform.GetChild(i);
        }
    }

    private void Start()
    {
        cameraWaypoint = Camera.main.GetComponent<Waypoint>();
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        for (int i = 0; i < waves.Length; ++i)
        {
            if (timeElapsed >= waves[i].time - timeToShowMarker)
            {
                
                bool waypointAdded = waypoints.Count > i;
                if (!waypointAdded)
                {
                    var wave = waves[i];
                    var waypoint = new WaypointData
                    {
                        target = spawners[wave.spawnerIndex],
                        waypoint = Instantiate(CanvasManager.instance.mainGui.indicatorPrefab, CanvasManager.instance.mainGui.GetComponent<RectTransform>())
                    };
                    Indicator indicator = waypoint.waypoint.GetComponent<Indicator>();
                    indicator.enemyTypeText.text = wave.enemyCount + " " + wave.enemyType.ToString();
                    indicator.timeLeftText.GetComponent<WaypointText>().SetTimer(wave.time - timeElapsed);
                    waypoints.Add(waypoint);
                    cameraWaypoint.AddWaypoint(waypoint);
                }
            }
        }

        if (waveIndex < waves.Length && timeElapsed >= waves[waveIndex].time)
        {
            Wave wave = waves[waveIndex];
            var waveEnumerable = SpawnWave(wave);

            StartCoroutine(waveEnumerable);
            cameraWaypoint.RemoveWaypoint(waypoints[waveIndex]);

            ++waveIndex;
        }
        if (waveIndex == waves.Length && EnemyManager.Instance.enemies.Count == 0)
        {
            StartCoroutine(CheckForWin());
        }
    }

    IEnumerator CheckForWin()
    {
        yield return new WaitForSeconds(2.5f);
        if (waveIndex == waves.Length && EnemyManager.Instance.enemies.Count == 0)
        {
            CanvasManager.instance.ShowGameOverWin();
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab, int spawnerIndex)
    {
        var enemy = Instantiate(enemyPrefab, spawners[spawnerIndex].position, Quaternion.identity);
        EnemyManager.Instance.AddEnemy(enemy.GetComponent<Enemy>());
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.enemyCount; ++i)
        {
            SpawnEnemy(PrefabManager.enemyPrefabs[wave.enemyType], wave.spawnerIndex);
            yield return new WaitForSeconds(wave.timeBetweenEnemies);
        }
    }
}