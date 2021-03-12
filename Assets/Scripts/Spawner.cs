using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public enum SpawnModes
{ 
    Fixed,
    Random
}



public class Spawner : MonoBehaviour
{
    public static Action OnWaveCompleted;

    [Header("Settings")]
    [SerializeField] private SpawnModes spawnMode = SpawnModes.Fixed;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private float delayBtwWaves = 1f;
    
    
    [Header("Fixed Delay")]
    [SerializeField] private float delayBetwSpawns;

    [Header("Random Delay")]
    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandomDelay;

    private float spawnTimer;
    private int enemiesSpawned;
    private int enemiesRem;

    private ObjectPooler pooler;
    private Waypoint waypoint;


    private void Start()
    {
        pooler = GetComponent<ObjectPooler>();
        waypoint = GetComponent<Waypoint>();

        enemiesRem = enemyCount;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer = GetSpawnDelay();
            if (enemiesSpawned < enemyCount)
            {
                enemiesSpawned++;
                SpawnEnemy();
            }
        }
    }


    private void SpawnEnemy()
    {
        GameObject newInstance = pooler.GetInstanceFromPool();
        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.Waypoint = waypoint;
        enemy.ResetEnemy();

        enemy.transform.localPosition = transform.position;

        newInstance.SetActive(true);
    }

    private float GetSpawnDelay()
    {
        float delay = 0f;
        if (spawnMode == SpawnModes.Fixed)
        {
            delay = delayBetwSpawns;
        }
        else
        {
            delay = GetRandomDelay();
        }

        return delay;
    }
    private float GetRandomDelay()
    {
        float randomTimer = Random.Range(minRandomDelay, maxRandomDelay);
        return randomTimer;
    }

    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(delayBtwWaves);
        enemiesRem = enemyCount;
        spawnTimer = 0f;
        enemiesSpawned = 0;
    }

    private void RecordEnemy(Enemy enemy)
    {
        enemiesRem--;
        if (enemiesRem <= 0)
        {
            OnWaveCompleted?.Invoke();
            StartCoroutine(NextWave());
        }
    }

    private void OnEnable()
    {
        Enemy.OnEndReached += RecordEnemy;
        EnemyHealth.OnEnemyKilled += RecordEnemy;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= RecordEnemy;
        EnemyHealth.OnEnemyKilled -= RecordEnemy;
    }

}
