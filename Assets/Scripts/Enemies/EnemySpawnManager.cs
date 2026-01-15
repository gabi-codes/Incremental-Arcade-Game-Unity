using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    [Header("Enemy")]
    public GameObject squareEnemyPrefab;
    public GameObject circleEnemyPrefab;

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
            SpawnEnemy(i % 4, i % 4);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    IEnumerator SpawnWave2()
    {
        currentWave = 2;

        int count = 20;

        for (int i = 0; i < count; i++)
        {
            SpawnEnemy(0);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    void SpawnEnemy(int pathVariant = 0, int tier = 0)
    {
        EnemyRoot enemy = Instantiate(circleEnemyPrefab).GetComponent<EnemyRoot>();
        enemy.Init(pathVariant, tier);
    }
}
