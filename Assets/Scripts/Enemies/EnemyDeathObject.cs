using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathObject : MonoBehaviour
{
    [Header("Config")]
    public float timeToLive = 2f;
    public EntityEffectConfig entityEffectConfig;

    private void Start()
    {
        Destroy(gameObject, timeToLive);
    }
}
