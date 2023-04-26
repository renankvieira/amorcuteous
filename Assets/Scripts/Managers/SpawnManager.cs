using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : SingletonOfType<SpawnManager>
{
    [Header("Main")]
    public bool spawnerIsActive = true;

    [Header("Progress")]
    [ReadOnly] public int currentEnemyCount = 0;
    [ReadOnly] public int currentMaxEnemies = 1;
    //public float maxEnemiesIncreaseFrequency = 5f;
    //public int limitOfMaxEnemies = 10;
    public float spawnFrequency = 3f;

    public AnimationCurve maxEnemiesCurve;
    [Range(0, 30)] public int curveMagnitude = 20;

    [Header("Boundaries")]
    public Transform upperLeftBoundary;
    public Transform lowerRightBoundary;

    [Header("Debug")]
    public bool useDebug;
    public EnemyBase[] enemyPrefabsDebug;

    //public EnemyBase[] ChosenEnemyPrefabs => useDebug ? enemyPrefabsDebug : enemyPrefabs;

    private void Start()
    {
        if (!spawnerIsActive)
            Debug.LogWarning("[SM] Spawner is inactive.", this);

        if (!useDebug)
            Debug.LogWarning("[SM] Using debug enemies.", this);

        //StartCoroutine(MaxEnemiesControlCoroutine());
        StartCoroutine(SpawnControlCoroutine());
    }

    //IEnumerator MaxEnemiesControlCoroutine()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(maxEnemiesIncreaseFrequency);
    //        currentMaxEnemies++;

    //        if (currentMaxEnemies >= limitOfMaxEnemies)
    //            yield break;
    //    }
    //}

    IEnumerator SpawnControlCoroutine()
    {
        while (true)
        {
            float maxEnemiesByCurve = maxEnemiesCurve.Evaluate(GameManager.Instance.NormalizedTimeOnRound) * curveMagnitude;
            currentMaxEnemies = (int)maxEnemiesByCurve;

            if (currentEnemyCount < currentMaxEnemies && spawnerIsActive)
            {
                //Spawns something in between 1 and 1 + (missing enemies/3)
                int missingEnemies = currentMaxEnemies - currentEnemyCount;
                int spawnedEnemies = 1 + (missingEnemies / 3);
                spawnedEnemies = Random.Range(1, spawnedEnemies + 1);

                for (int i = 0; i < spawnedEnemies; i++)
                    SpawnOneEnemy();
            }
            yield return new WaitForSeconds(spawnFrequency);
        }
    }

    void SpawnOneEnemy()
    {
        int horizontalPosition = 0;
        int verticalPosition = 0;

        while (Mathf.Abs(horizontalPosition) - Mathf.Abs(verticalPosition) == 0)
        {
            horizontalPosition = Random.Range(-1, 2);
            verticalPosition = Random.Range(-1, 2);
        }

        Vector3 spawnPosition = GetRandomBoundaryPosition(horizontalPosition, verticalPosition);
        Vector3 targetPosition = GetRandomBoundaryPosition(horizontalPosition * -1, verticalPosition * -1);

        EnemyBase chosenEnemy = SpawnChances.Instance.GetSpawnedEnemy(GameManager.Instance.NormalizedTimeOnRound);

        if (useDebug)
            chosenEnemy = enemyPrefabsDebug.GetRandom();

        EnemyBase newEnemy = Instantiate(chosenEnemy, spawnPosition, Quaternion.identity);

        if (newEnemy.entityConfig.spawnsAimedAtPlayer && GameManager.Instance.player != null)
            targetPosition = GameManager.Instance.player.transform.position;
        newEnemy.Initialize(targetPosition);

        currentEnemyCount++;
    }



    Vector3 GetRandomBoundaryPosition(int xAxis, int zAxis)
    {
        Vector3 randomPosition = Vector3.zero;
        if (xAxis < 0)
            randomPosition.x = upperLeftBoundary.position.x;
        else if (xAxis == 0)
            randomPosition.x = Random.Range(upperLeftBoundary.position.x, lowerRightBoundary.position.x);
        else
            randomPosition.x = lowerRightBoundary.position.x;

        if (zAxis < 0)
            randomPosition.z = lowerRightBoundary.position.z;
        else if (zAxis == 0)
            randomPosition.z = Random.Range(upperLeftBoundary.position.z, lowerRightBoundary.position.z);
        else
            randomPosition.z = upperLeftBoundary.position.z;

        return randomPosition;
    }

}
