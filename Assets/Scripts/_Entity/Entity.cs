
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
                ApplyEffect(otherEntity.entityConfig.touchEffectToPlayer, otherEntity.transform);
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
                if (effect.effectConfig.debug.logUsage || logEffects)
                    Debug.LogFormat(this, "Effect off: [{0}], [{1}]", effect.effectConfig.name, gameObject.name);

                currentEffects.Remove(effect);

                if (effect.effectAttachment != null)
                    Destroy(effect.effectAttachment);

                if (effect.effectConfig.visuals.createOnDeactivation != null)
                    Instantiate(effect.effectConfig.visuals.createOnDeactivation, transform.position, effect.effectConfig.visuals.createOnDeactivation.transform.rotation);
            }
        }
    }

    public void ApplyEffect(EntityEffectConfig config, Transform applier)
    {
        if (isDead)
            return;

        if (config.debug.logUsage || logEffects)
            Debug.LogFormat(this, "Effect on: [{0}], [{1}]", config.name, gameObject.name);

        if (config.persistence.renewsOnReapply)
        {
            foreach (EntityEffect effect in currentEffects)
            {
                if (effect.effectConfig == config)
                {
                    //currentEffects.Remove(effect);
                    effect.timeOfActivation = -1000f;

                    if (config.debug.logUsage || logEffects)
                        Debug.LogFormat(this, "Effect replace: [{0}], [{1}]", config.name, gameObject.name);
                    //break;
                }
            }
        }

        EntityEffect newEffect = new EntityEffect();
        newEffect.timeOfActivation = Time.time;
        newEffect.effectConfig = config;
        currentEffects.Add(newEffect);

        if (config.damage.damage > 0)
            TakeDamage(config.damage.damage, config.damage.damageType, null);

        if (config.visuals.attachOnActivation)
            newEffect.effectAttachment = Instantiate(config.visuals.attachOnActivation, transform.position, transform.rotation, transform);
        if (config.visuals.createOnActivation)
            Instantiate(config.visuals.attachOnActivation, transform.position, config.visuals.createOnActivation.transform.rotation);
        //if (config.animationTriggerOnActivation != "---")
        //    GetComponent<Animator>().SetTrigger(config.animationTriggerOnActivation);
    }

    public float GetSpeedMultiplier()
    {
        float speedMultiplier = 1f;

        foreach (EntityEffect effect in currentEffects)
        {
            if (effect.effectConfig.movement.preventsMovement)
                return 0f;
            else 
                speedMultiplier *= effect.effectConfig.movement.movementSpeedMultiplier;
        }

        return speedMultiplier;
    }

    public bool RotationIsPrevented()
    {
        foreach (EntityEffect effect in currentEffects)
            if (effect.effectConfig.movement.preventsRotation)
                return true;
        return false;
    }

    public bool AttackIsPrevented()
    {
        foreach (EntityEffect effect in currentEffects)
            if (effect.effectConfig.movement.preventsAttack)
                return true;
        return false;
    }

}

public enum DamageType
{
    NOT_SET = 0,
    PLAYER_SWORD = 1,
    ACID = 10,
    SHATTER = 20
}

