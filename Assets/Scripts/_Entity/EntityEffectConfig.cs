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

    [Header("Objects")]
    public GameObject attachOnActivation;
    public GameObject createOnActivation;
    public GameObject createOnDeactivation;

    [Header("Debug")]
    public bool logUsage = false;
}

public enum EntityEffectType
{
    NONE = 0,
    SLOW = 10,
    STUN = 20
}