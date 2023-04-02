using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityConfig_", menuName = "General/New Entity Config")]
public class EntityConfig : ScriptableObject
{
    public EntityType entityType;
    public string entityName = "-";
    public int hp = 1;
    public int priority = 0;
    public bool spawnsAimedAtPlayer = false;
    public float movementSpeed = 5f;
    public float rotationToPlayer = 0f;

    public TouchEffectToPlayer touchEffectToPlayer;
    [Expandable] public EntityEffectConfig touchEffectToPlayer_EFC;
    public TouchEffectByPlayer touchEffectByPlayer;
    public TouchEffectToSibling touchEffectToSibling;

    public EnemyDeathObject deathObject;

    public EntityEffectConfig gooEntityEffect;
    public EntityEffectConfig blastEntityEffect;
}

public enum EntityType
{
    PLAYER = 0,
    ENEMY = 10
}

public enum TouchEffectToPlayer
{
    NONE = 0,
    STUNS = 10,
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
    BOTH_DIE = 30
}