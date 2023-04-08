
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[SelectionBase]
public class Entity : MonoBehaviour
{
    [Header("Config")]
    public EntityConfig entityConfig;

    [Header("Control")]
    public int hp = 1;
    public bool isDead = false;
    public List<EntityEffect> currentEffects;

    EnemyBase enemy;
    EnemyDeathObject deathPrefab;
    
    List<EntityEffect> _cachedEffectList;

    [Header("Debug")]
    public bool logContacts = false;
    public bool logEffects = false;

    public Action onDamage;
    public Action onDeath;

    void Start()
    {
        enemy = GetComponent<EnemyBase>();

        if (enemy)
        {
            hp = enemy.entityConfig.hp;
            deathPrefab = enemy.entityConfig.deathObject;
        }
    }

    private void Update()
    {
        UpdateEffects();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead)
            return;

        if (other.CompareTag("Player"))
            OnPlayerContact(other.GetComponent<Entity>());
        else if (other.CompareTag("Enemy"))
            OnEnemyContact(other.GetComponent<Entity>());
    }

    public void OnPlayerContact(Entity otherEntity)
    {
        if (logContacts)
            Debug.Log("[E] Contact to Player: " + entityConfig.entityName + " x " + otherEntity.entityConfig.entityName, this);

        switch (entityConfig.touchEffectByPlayer)
        {
            case TouchEffectByPlayer.REVERSES:
                enemy.ReverseOnContact(otherEntity.transform);
                break;
            case TouchEffectByPlayer.DIES:
                Die(otherEntity.transform);
                break;
            default:
                break;
        }
    }

    public void OnEnemyContact(Entity otherEntity)
    {
        if (logContacts)
            Debug.Log("[E] Contact to Enemy: " + entityConfig.entityName + " x " + otherEntity.entityConfig.entityName, this);

        if (this.entityConfig.entityType == EntityType.PLAYER)
        {
            if (otherEntity.entityConfig.touchEffectToPlayer != null)
                ApplyEffect(otherEntity.entityConfig.touchEffectToPlayer);
        }
        else if (this.entityConfig.entityType == EntityType.ENEMY)
        {
            // compare priorities. bigger priority kills other.
            int otherEntityPriority = otherEntity.entityConfig.priority;

            if (otherEntityPriority > entityConfig.priority)
            {
                Die(otherEntity.transform);
            }
            else if (otherEntityPriority == entityConfig.priority)
            {
                if (otherEntity.entityConfig == entityConfig)
                {
                    switch (entityConfig.touchEffectToSibling)
                    {
                        case TouchEffectToSibling.REVERSES:
                            enemy.ReverseOnContact(otherEntity.transform);
                            break;
                        case TouchEffectToSibling.ONE_DIES:
                            if (UnityEngine.Random.value > 0.5f) // who dies?
                                Die(otherEntity.transform);
                            else
                                otherEntity.Die(transform);
                            break;
                        case TouchEffectToSibling.BOTH_DIE:
                            Die(otherEntity.transform);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Debug.LogWarning("[E] Different entities with same priorities: " + entityConfig.entityName + "x" + otherEntity.entityConfig.entityName, this);
                }
            }
            else if (otherEntityPriority < entityConfig.priority)
            {
                // do nothing. other entity own collision will handle it.
            }
        }

    }


    public void TakeDamage(int damage, DamageType damageType, Transform damageDealerTransform)
    {
        if (isDead)
            return;

        hp -= damage;

        if (hp <= 0)
        {
            if (damageType == DamageType.PLAYER_SWORD)
                Die(damageDealerTransform.transform);
            else
                Die(null);
        }
    }

    public void Die(Transform killer)
    {
        if (isDead)
            return;
        isDead = true;

        Destroy(gameObject);
        SpawnManager.Instance.currentEnemyCount--;

        if (deathPrefab != null && killer != null)
        {
            EnemyDeathObject deathObject = Instantiate(deathPrefab, transform.position, killer.transform.rotation * Quaternion.AngleAxis(0, Vector3.up));
            deathObject.Initialize(entityConfig);
        }
    }

    public void Dismiss()
    {
        if (isDead)
            return;
        isDead = true;

        Destroy(gameObject);
        SpawnManager.Instance.currentEnemyCount--;
    }

    public void UpdateEffects()
    {
        for (int i = currentEffects.Count-1; i >= 0; i--)
//        foreach (EntityEffect effect in currentEffects)
        {
            EntityEffect effect = currentEffects[i];
            if (effect.timeOfActivation + effect.effectConfig.duration <= Time.time)
            {
                if (effect.effectConfig.logUsage || logEffects)
                    Debug.LogFormat(this, "Effect off: [{0}], [{1}]", effect.effectConfig.name, gameObject.name);

                currentEffects.Remove(effect);

                if (effect.effectAttachment != null)
                    Destroy(effect.effectAttachment);

                if (effect.effectConfig.createOnDeactivation != null)
                    Instantiate(effect.effectConfig.createOnDeactivation, transform.position, effect.effectConfig.createOnDeactivation.transform.rotation);
            }
        }
    }

    public void ApplyEffect(EntityEffectConfig config)
    {
        if (isDead)
            return;

        if (config.logUsage || logEffects)
            Debug.LogFormat(this, "Effect on: [{0}], [{1}]", config.name, gameObject.name);

        if (config.replaceOnStack)
        {
            foreach (EntityEffect effect in currentEffects)
            {
                if (effect.effectConfig == config)
                {
                    //currentEffects.Remove(effect);
                    effect.timeOfActivation = -1000f;

                    if (config.logUsage || logEffects)
                        Debug.LogFormat(this, "Effect replace: [{0}], [{1}]", config.name, gameObject.name);
                    //break;
                }
            }
        }

        EntityEffect newEffect = new EntityEffect();
        newEffect.timeOfActivation = Time.time;
        newEffect.effectConfig = config;
        currentEffects.Add(newEffect);

        if (config.attachOnActivation)
            newEffect.effectAttachment = Instantiate(config.attachOnActivation, transform.position, transform.rotation, transform);
        if (config.createOnActivation)
            Instantiate(config.attachOnActivation, transform.position, config.createOnActivation.transform.rotation);
        if (config.animationTriggerOnActivation != "---")
            GetComponent<Animator>().SetTrigger(config.animationTriggerOnActivation);
    }

    public bool HasEffectOfType(EntityEffectType entityEffectType)
    {
        foreach (EntityEffect effect in currentEffects)
            if (effect.effectConfig.effectType == entityEffectType)
                return true;
        return false;
    }

    public List<EntityEffect> GetEffectsByType(EntityEffectType entityEffectType)
    {
        if (_cachedEffectList == null)
            _cachedEffectList = new List<EntityEffect>();
        _cachedEffectList.Clear();

        foreach (EntityEffect effect in currentEffects)
            if (effect.effectConfig.effectType == entityEffectType)
                _cachedEffectList.Add(effect);

        return _cachedEffectList;
    }





    //TODO: Remove effects type. Instead, put on effect config itself: "multiplicative slow", "prevents movement".
    // or "stuns"

    public float GetSpeedMultiplier()
    {
        float speedMultiplier = 1f;
        if (IsStunned() || IsFrozen())
        {
            speedMultiplier = 0f;
        }
        else if (HasEffectOfType(EntityEffectType.SLOW))
        {
            List<EntityEffect> _cachedEffects = GetEffectsByType(EntityEffectType.SLOW);
            foreach (EntityEffect effect in _cachedEffects)
                speedMultiplier *= effect.effectConfig.power;
        }

        return speedMultiplier;
    }

    public bool IsStunned()
    {
        return HasEffectOfType(EntityEffectType.STUN);
    }

    public bool IsFrozen()
    {
        return HasEffectOfType(EntityEffectType.FREEZE);
    }
}

public enum DamageType
{
    PLAYER_SWORD = 0,
    ACID = 10,
    ICE = 20
}