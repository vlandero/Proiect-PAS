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
    public GameObject enemyPrefab;
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
        this.spawnerCount = waveSpawnersObject.transform.childCount;
        this.spawners = new Transform[spawnerCount];
        for (int i = 0; i < this.spawnerCount; ++i)
        {
            spawners[i] = waveSpawnersObject.transform.GetChild(i);
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        this.timeElapsed += Time.deltaTime;

        if (this.waveIndex < this.waves.Length && this.timeElapsed >= this.waves[this.waveIndex].time)
        {
            Wave wave = this.waves[waveIndex];
            var waveEnumerable = this.SpawnWave(wave);

            StartCoroutine(waveEnumerable);

            ++this.waveIndex;
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab, int spawnerIndex)
    {
        Instantiate(enemyPrefab, this.spawners[spawnerIndex]);
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.enemyCount; ++i)
        {
            yield return new WaitForSeconds(wave.timeBetweenEnemies);
            this.SpawnEnemy(wave.enemyPrefab, wave.spawnerIndex);
        }
    }
}