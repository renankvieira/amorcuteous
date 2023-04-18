using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChances : SingletonOfType<SpawnChances>
{
    [Header("Handles")]
    [Range(0f, 1f)] public float e0;
    [Range(0f, 1f)] public float e1;
    [Range(0f, 1f)] public float e2;
    [Range(0f, 1f)] public float e3;
    [Range(0f, 1f)] public float e4;
    [Range(0f, 1f)] public float e5;

    [Header("References")]
    public EnemyBase eConfig0;
    public EnemyBase eConfig1;
    public EnemyBase eConfig2;
    public EnemyBase eConfig3;
    public EnemyBase eConfig4;
    public EnemyBase eConfig5;

    AnimationClip anim;

    public EnemyBase GetSpawnedEnemy(float roundNormalizedTime)
    {
        anim = GameManager.Instance.currentLevel.spawnProgression;

        anim.SampleAnimation(gameObject, roundNormalizedTime);

        float chancesSum = 0 + e0 + e1 + e2 + e3 + e4 + e5;
        float randomNumber = Random.Range(0f, chancesSum);

        if (randomNumber <= e0) return eConfig0;
        if (randomNumber <= e0 + e1) return eConfig1;
        if (randomNumber <= e0 + e1 + e2) return eConfig2;
        if (randomNumber <= e0 + e1 + e2 + e3) return eConfig3;
        if (randomNumber <= e0 + e1 + e2 + e3 + e4) return eConfig4;
        if (randomNumber <= e0 + e1 + e2 + e3 + e4 + e5) return eConfig5;

        Debug.LogWarningFormat(this, "[SC] randomNumber too high: {0} x {1}", randomNumber, chancesSum);
        return eConfig0;
    }





    //[Header("Debug")]
    //public bool autoTestOnValidate = false;
    //public EnemyBase testEnemy;
    //[Range(0f, 1f)] public float testNormalizedTime = 0f;

    //public float result;

    //[Button]
    //public void Test()
    //{
    //    result = GetChance(testEnemy, testNormalizedTime);
    //}

    //public float GetChance(EnemyBase enemyPrefab, float roundNormalizedTime)
    //{
    //    anim.SampleAnimation(gameObject, roundNormalizedTime);

    //    if (enemyPrefab == eConfig0)
    //        return e0;
    //    if (enemyPrefab == eConfig1)
    //        return e1;
    //    if (enemyPrefab == eConfig2)
    //        return e2;
    //    if (enemyPrefab == eConfig3)
    //        return e3;
    //    if (enemyPrefab == eConfig4)
    //        return e4;
    //    if (enemyPrefab == eConfig5)
    //        return e5;

    //    Debug.LogWarning("[SC] EnemyBase not found: " + enemyPrefab.name, this);
    //    return 0;
    //}

    //private void OnValidate()
    //{
    //    if (autoTestOnValidate)
    //        Test();
    //}

}
