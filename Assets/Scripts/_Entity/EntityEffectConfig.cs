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

    [Header("Objects")]
    public GameObject attachOnActivation;
    public GameObject createOnActivation;
    public GameObject createOnDeactivation;

    [Header("Effects")]
    public bool replaceOnStack = true;
    public bool reducesMovementSpeed = false;

}

public enum EntityEffectType
{
    NONE = 0,
    SLOW = 10,
    STUN = 20
}