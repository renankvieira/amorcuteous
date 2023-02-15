using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public List<EntityEffect> currentEffects;

    private void Update()
    {
        UpdateEffects();
    }

    public void UpdateEffects()
    {
        for (int i = currentEffects.Count-1; i > 0;i--)
//        foreach (EntityEffect effect in currentEffects)
        {
            EntityEffect effect = currentEffects[i];
            if (effect.timeOfActivation + effect.effectConfig.duration > Time.time)
            {
                currentEffects.Remove(effect);
                if (effect.effectConfig.createOnDeactivation != null)
                    Instantiate(effect.effectConfig.createOnDeactivation, transform.position, effect.effectConfig.createOnDeactivation.transform.rotation);
            }
        }
    }

    public void ApplyEffect(EntityEffectConfig config, bool allowDuplicates)
    {
        if (!allowDuplicates)
        {
            foreach (EntityEffect effect in currentEffects)
            {
                if (effect.effectConfig == config)
                {
                    Debug.LogFormat(this, "Already has EffectConfig: {0}, {1}", config.name, gameObject.name);
                    return;
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
    }

    public bool HasEffectOfType(EntityEffectType entityEffectType)
    {
        foreach (EntityEffect effect in currentEffects)
            if (effect.effectConfig.effectType == entityEffectType)
                return true;
        return false;
    }

    public List<EntityEffect> _cachedEffectList;
    public List<EntityEffect> GetEffectsByType(EntityEffectType entityEffectType)
    {
        _cachedEffectList.Clear();

        foreach (EntityEffect effect in currentEffects)
            if (effect.effectConfig.effectType == entityEffectType)
                _cachedEffectList.Add(effect);

        return _cachedEffectList;
    }
}
