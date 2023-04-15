using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityEffectConfig_000", menuName = "General/New Entity Effect Config")]
public class EntityEffectConfig : ScriptableObject
{
    [Header("Main")]
    public string effectName = "---";
    public float duration = 3f;

    public Damage damage;
    public Movement movement;
    public Persistence persistence;
    public Visuals visuals;
    public Debug debug;

    [System.Serializable] public class Damage
    {
        public int damage = 0;
        public DamageType damageType = DamageType.NOT_SET;
    }

    [System.Serializable] public class Movement
    {
        public bool preventsMovement = false;
        public bool preventsRotation = false;
        public bool preventsAttack = false;
        public float movementSpeedMultiplier = 1f;
    }

    [System.Serializable] public class Persistence
    {
        public bool renewsOnReapply = true;
        public bool makesFragile = false;
    }

    [System.Serializable] public class Visuals
    {
        public GameObject attachOnActivation;
        public GameObject createOnActivation;
        public GameObject createOnDeactivation;
    }

    [System.Serializable] public class Debug
    {
        public bool logUsage = false;
    }
}
