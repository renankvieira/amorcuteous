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
    public EntityEffectConfig touchEffectToPlayer_EFC;
    public TouchEffectByPlayer touchEffectByPlayer;
    public TouchEffectToSibling touchEffectToSibling;

    public GameObject deathObject;

    public EntityEffectConfig entityEffectOnDeath;

    public string test;
    public EnemyConfig enemyConfig;

    [ButtonSO]
    public void Copy()
    {
        entityName = enemyConfig.enemyName;
        hp = enemyConfig.hp;
        priority = enemyConfig.priority;
        spawnsAimedAtPlayer = enemyConfig.spawnsAimedAtPlayer;
        movementSpeed = enemyConfig.movementSpeed;
        rotationToPlayer = enemyConfig.rotationToPlayer;

        touchEffectToPlayer = enemyConfig.touchEffectToPlayer;
        touchEffectToPlayer_EFC = enemyConfig.touchEffectToPlayer_EFC;
        touchEffectByPlayer = enemyConfig.touchEffectByPlayer;
        touchEffectToSibling = enemyConfig.touchEffectToSibling;

        deathObject = enemyConfig.deathObject;

        entityEffectOnDeath = enemyConfig.entityEffectOnDeath;

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }

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