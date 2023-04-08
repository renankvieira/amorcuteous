using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityEffectConfig_000", menuName = "General/New Entity Effect Config")]
public class EntityEffectConfig : ScriptableObject
{
    [Header("Config")]
    public string effectName = "---";
    public EntityEffectType effectType = EntityEffectType.NONE;
    public float duration = 3f;
    public float power = 0.5f;
    public bool replaceOnStack = true;
    public string animationTriggerOnActivation = "---";

    [Header("Config")]
    public int damage = 0;
    public DamageType damageType = DamageType.NOT_SET;


    [Header("Objects")]
    public GameObject attachOnActivation;
    public GameObject createOnActivation;
    public GameObject createOnDeactivation;

    [Header("Debug")]
    public bool logUsage = false;


    [Header("Temp/Tests")] // remove effect type, put these generic descriptions.
    public float speedDecreaseMultiplier = 0f;
    public bool stunsPlayer = false;
}

public enum EntityEffectType
{
    NONE = 0,
    SLOW = 10,
    STUN = 20,
    FREEZE = 30,
    SMASH = 40,
    ENCAPSULATE = 50,
    MELTDOWN = 60,

    ETC_01 = 1001,
    ETC_02 = 1002,
    ETC_03 = 1003
}