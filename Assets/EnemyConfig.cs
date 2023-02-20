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
    public TouchEffectByPlayer touchEffectByPlayer;
    public TouchEffectToSibling touchEffectToSibling;

    public EntityEffectConfig entityEffectOnDeath;
}


public enum TouchEffectToPlayer
{
    NONE = 0,
    STUNS = 10,
    FREEZES = 20,
    SMASHES = 30,
    ENCAPSULATES = 40
}

public enum TouchEffectByPlayer
{
    NONE = 0,
    REVERSES = 10,
    DIES = 20
}

public enum TouchEffectToSibling
{
    NONE = 0,
    REVERSES = 10,
    ONE_DIES = 20,
    BOTH_DIES = 30
}