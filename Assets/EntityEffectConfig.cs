using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityEffectConfig_000", menuName = "General/New Entity Effect Config")]
public class EntityEffectConfig : ScriptableObject
{
    [Header("Config")]
    public string effectName = "slow";
    public EntityEffectType effectType = EntityEffectType.NONE;
    public float duration = 3f;
    public float power = 0.5f;
    public GameObject attachOnActivation;
    public GameObject createOnActivation;
    public GameObject createOnDeactivation;
}

public enum EntityEffectType
{
    NONE = 0,
    SLOW = 10
}