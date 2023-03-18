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
            hp = enemy.enemyConfig.hp;
            deathPrefab = enemy.enemyConfig.deathObject;
        }
    }

    private void Update()
    {
        UpdateEffects();
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