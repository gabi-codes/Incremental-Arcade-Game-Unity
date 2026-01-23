using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    [Header("Enemy")]
    public GameObject[] enemies;

    [Header("Timing")]
    public float timeBetweenSpawns = 0.5f;
    public float timeBetweenWaves = 4f;

    int currentWave = 0;

    private Coroutine spawnRoutine;
    enum Types
    {
        Squere = 0,
        Circle = 1,
        PointyCircle = 2,
        HalfCross = 3
    }

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
        yield return new WaitForSeconds(timeBetweenWaves);
        yield return SpawnWave3();
        yield return new WaitForSeconds(timeBetweenWaves);
        yield return SpawnWave4();
        yield return new WaitForSeconds(timeBetweenWaves);
        yield return SpawnWave5();
    }

    IEnumerator SpawnWave1()
    {
        currentWave = 1;

        for (int i = 0; i < 15; i++)
        {
            SpawnEnemy(Types.Squere, 0, 0);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator SpawnWave2()
    {
        currentWave = 2;

        for (int i = 0; i < 12; i++)
        {
            SpawnEnemy(Types.Squere, 2, 0);
            yield return new WaitForSeconds(0.5f);
            SpawnEnemy(Types.Squere, 3, 0);
            yield return new WaitForSeconds(0.5f);
        }

        for (int i = 0; i < 3; i++)
        {
            SpawnEnemy(Types.Squere, 2, 1);
            yield return new WaitForSeconds(0.5f);
            SpawnEnemy(Types.Squere, 3, 1);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator SpawnWave3()
    {
        currentWave = 3;

        for (int i = 0; i < 25; i++)
        {
            SpawnEnemy(Types.Squere, 1, 0);

            if (i > 10 && i % 2 == 0) { SpawnEnemy(Types.Squere, 2, 1); }
            else if (i > 10 && i % 2 == 1) { SpawnEnemy(Types.Squere, 3, 1); }

            yield return new WaitForSeconds(0.5f);
        }

    }

    IEnumerator SpawnWave4()
    {
        currentWave = 3;

        for (int i = 0; i < 4; i++)
        {
            SpawnEnemy(Types.Circle, i % 4, 2);

            yield return new WaitForSeconds(0.25f);
        }

        for (int i = 0; i < 12; i++)
        {
            SpawnEnemy(Types.Circle, i % 4, 0);

            yield return new WaitForSeconds(0.4f);
        }
    }

    IEnumerator SpawnWave5()
    {
        currentWave = 3;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                SpawnEnemy(Types.Squere, i % 4, 1);
                if (j == 4 || j == 7) {  SpawnEnemy(Types.Circle, 3 - i, 2); }

                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    void SpawnEnemy(Types enemyIndex, int pathVariant = 0, int tier = 0)
    {
        EnemyRoot enemy = Instantiate(enemies[(int)enemyIndex]).GetComponent<EnemyRoot>();
        enemy.Init(pathVariant, tier);
    }
}
