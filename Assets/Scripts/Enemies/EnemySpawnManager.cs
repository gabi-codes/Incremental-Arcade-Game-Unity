using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    [Header("Enemy")]
    public GameObject squareEnemyPrefab;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("Timing")]
    public float timeBetweenSpawns = 0.5f;
    public float timeBetweenWaves = 3f;

    int currentWave = 0;

    private Coroutine spawnRoutine;

    void Start()
    {
        
    }

    public void StartSpawning()
    {
        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);

        spawnRoutine = StartCoroutine(SpawnWaves());
    }

    public void StopSpawning()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }
    
    IEnumerator SpawnWaves()
    {
        yield return SpawnWave1();
        yield return new WaitForSeconds(timeBetweenWaves);
        yield return SpawnWave2();
    }

    IEnumerator SpawnWave1()
    {
        currentWave = 1;

        int count = 10;

        for (int i = 0; i < count; i++)
        {
            Transform spawn = spawnPoints[i % spawnPoints.Length];
            SpawnEnemy(spawn.position, i % spawnPoints.Length, i % spawnPoints.Length);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    IEnumerator SpawnWave2()
    {
        currentWave = 2;

        int count = 20;
        Transform spawn = spawnPoints[0]; // jeden punkt

        for (int i = 0; i < count; i++)
        {
            SpawnEnemy(spawn.position, 0);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    void SpawnEnemy(Vector2 position, int pathVariant = 0, int tier = 0)
    {
        EnemyRoot enemy = Instantiate(squareEnemyPrefab).GetComponent<EnemyRoot>();
        enemy.Init(pathVariant, tier);
    }
}
