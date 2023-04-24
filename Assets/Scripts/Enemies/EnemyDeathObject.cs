using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class EnemyDeathObject : MonoBehaviour
{
    [Header("References")]
    public EntityEffectApplier gooEntityEffectApplier;
    public EntityEffectApplier blastEntityEffectApplier;
    public EntityEffectApplier poolEntityEffectApplier;
    public GameObject blastParent;
    public GameObject poolParent;

    public void Initialize(EntityConfig entityConfig)
    {
        gooEntityEffectApplier.entityEffectToApply = entityConfig.gooEntityEffect;
        blastEntityEffectApplier.entityEffectToApply = entityConfig.blastEntityEffect;
        poolEntityEffectApplier.entityEffectToApply = entityConfig.poolEntityEffect;

        blastParent.SetActive(entityConfig.blastEntityEffect != null);
        poolParent.SetActive(entityConfig.poolEntityEffect != null);
    }
}
