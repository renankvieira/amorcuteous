using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig_", menuName = "General/New Enemy Config")]
public class EnemyConfig : ScriptableObject
{
    public string enemyName = "-";
    public int hp = 1;
    public int priority = 0;
    public bool spawnsAimedAtPlayer = false;
    public float movementSpeed = 5f;
    public float rotationToPlayer = 0f;

    public TouchEffectToPlayer touchEffectToPlayer;
    public EntityEffectConfig touchEffectToPlayer_EFC;
    public TouchEffectByPlayer touchEffectByPlayer;
    public TouchEffectToSibling touchEffectToSibling;

    public GameObject deathObject;

    public EntityEffectConfig entityEffectOnDeath;
}

