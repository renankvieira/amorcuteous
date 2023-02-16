using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonOfType<GameManager>
{
    public int currentEnemyCount = 0;

    public int currentMaxEnemies = 1;
    public float maxEnemiesIncreaseFrequency = 5f;
    public int limitOfMaxEnemies = 10;

    public float spawnFrequency = 3f;
    public Enemy enemyPrefab;
    public Transform upperLeftBoundary;
    public Transform lowerRightBoundary;

    private void Start()
    {
        StartCoroutine(MaxEnemiesControlCoroutine());
        StartCoroutine(SpawnControlCoroutine());
    }

    IEnumerator MaxEnemiesControlCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(maxEnemiesIncreaseFrequency);
            currentMaxEnemies++;

            if (currentMaxEnemies >= limitOfMaxEnemies)
                yield break;
        }
    }

    IEnumerator SpawnControlCoroutine()
    {
        while (true)
        {
            if (currentEnemyCount < currentMaxEnemies)
            {
                int horizontalPosition = 0;
                int verticalPosition = 0;

                while (Mathf.Abs(horizontalPosition) - Mathf.Abs(verticalPosition)== 0)
                {
                    horizontalPosition = Random.Range(-1, 2);
                    verticalPosition = Random.Range(-1, 2);
                }

                Vector3 spawnPosition = GetRandomBoundaryPosition(horizontalPosition, verticalPosition);
                Vector3 targetPosition = GetRandomBoundaryPosition(horizontalPosition * -1, verticalPosition * -1);

                Enemy newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                newEnemy.Initialize(targetPosition, horizontalPosition, verticalPosition);

                currentEnemyCount++;
            }


            yield return new WaitForSeconds(spawnFrequency);
        }
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
