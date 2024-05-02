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
    [SerializeField] private Wave[] waves;
    [SerializeField] private GameObject waveSpawnersObject;

    private Transform[] spawners;
    private int spawnerCount;
    private int waveIndex = 0;
    private float timeElapsed = 0;

    private void Awake()
    {
        spawnerCount = waveSpawnersObject.transform.childCount;
        spawners = new Transform[spawnerCount];
        for (int i = 0; i < spawnerCount; ++i)
        {
            spawners[i] = waveSpawnersObject.transform.GetChild(i);
        }
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (waveIndex < waves.Length && timeElapsed >= waves[waveIndex].time)
        {
            Wave wave = waves[waveIndex];
            var waveEnumerable = SpawnWave(wave);

            StartCoroutine(waveEnumerable);

            ++waveIndex;
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
            yield return new WaitForSeconds(wave.timeBetweenEnemies);
            SpawnEnemy(PrefabManager.enemyPrefabs[wave.enemyType], wave.spawnerIndex);
        }
    }
}