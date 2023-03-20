
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Entity : MonoBehaviour
{
    //[Header("Config")]
    public EntityConfig entityConfig;

    [Header("Control")]
    public int hp = 1;
    public bool isDead = false;
    public List<EntityEffect> currentEffects;

    EnemyBase enemy;
    GameObject deathPrefab;
    
    List<EntityEffect> _cachedEffectList;

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
        switch (entityConfig.touchEffectByPlayer)
        {
            case TouchEffectByPlayer.NONE:
                break;
            case TouchEffectByPlayer.REVERSES:
                break;
            case TouchEffectByPlayer.DIES:
                break;
            default:
                break;
        }
    }

    public void OnEnemyContact(Entity otherEntity)
    {
        if (this.entityConfig.entityType == EntityType.PLAYER)
        {
            switch (entityConfig.touchEffectToPlayer)
            {
                case TouchEffectToPlayer.STUNS:
                    //player is stunned
                    break;
                case TouchEffectToPlayer.FREEZES:
                    break;
                case TouchEffectToPlayer.SMASHES:
                    break;
                case TouchEffectToPlayer.ENCAPSULATES:
                    break;
                default:
                    break;
            }

            return;
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
                            break;
                        case TouchEffectToSibling.ONE_DIES:
                            break;
                        case TouchEffectToSibling.BOTH_DIE:
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
            GameObject deathObject = Instantiate(deathPrefab, transform.position, killer.transform.rotation * Quaternion.AngleAxis(30f, Vector3.up));
            Destroy(deathObject, 1f);
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
                if (effect.effectConfig.logUsage)
                    Debug.LogFormat(this, "Removing EffectConfig: {0}, {1}", effect.effectConfig.name, gameObject.name);

                currentEffects.Remove(effect);
                if (effect.effectConfig.createOnDeactivation != null)
                    Instantiate(effect.effectConfig.createOnDeactivation, transform.position, effect.effectConfig.createOnDeactivation.transform.rotation);
            }
        }
    }

    public void ApplyEffect(EntityEffectConfig config)
    {
        if (config.logUsage)
            Debug.LogFormat(this, "Applying EffectConfig: {0}, {1}", config.name, gameObject.name);

        if (config.replaceOnStack)
        {
            foreach (EntityEffect effect in currentEffects)
            {
                if (effect.effectConfig == config)
                {
                    //currentEffects.Remove(effect);
                    effect.timeOfActivation = -1000f;

                    if (effect.effectConfig.logUsage)
                        Debug.LogFormat(this, "ReplacingOnStack EffectConfig: {0}, {1}", config.name, gameObject.name);
                    //break;
                }
            }
        }

        EntityEffect newEffect = new EntityEffect();
        newEffect.timeOfActivation = Time.time;
        newEffect.effectConfig = config;
        currentEffects.Add(newEffect);

        if (config.attachOnActivation)
            Instantiate(config.attachOnActivation, transform.position, transform.rotation, transform);
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
}

public enum DamageType
{
    PLAYER_SWORD = 0,
    ACID = 10,
    ICE = 20
}