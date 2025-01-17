using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityConfig_", menuName = "General/New Entity Config")]
public class EntityConfig : ScriptableObject
{
    [Header("Main")]
    public EntityType entityType;
    public string entityName = "-";
    public int hp = 1;
    public int priority = 0;
    public bool spawnsAimedAtPlayer = false;
    public float movementSpeed = 5f;
    public float rotationToPlayer = 0f;

    [Header("Touch")]
    public EntityEffectConfig touchEffectToPlayer;
    public TouchEffectByPlayer touchEffectByPlayer;
    public TouchEffectToSibling touchEffectToSibling;

    [Header("Immunities")]
    public List<EntityEffectConfig> effectImmunities;
    public List<DamageType> damageTypeImmunities;

    [Header("Death")]
    public EnemyDeathObject deathObject;
    public EntityEffectConfig gooEntityEffect;
    public EntityEffectConfig blastEntityEffect;
    public EntityEffectConfig poolEntityEffect;
}

public enum EntityType
{
    PLAYER = 0,
    ENEMY = 10
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