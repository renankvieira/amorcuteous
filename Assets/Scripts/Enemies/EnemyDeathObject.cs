using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class EnemyDeathObject : MonoBehaviour
{
    [Header("References")]
    public EntityEffectApplier gooEntityEffectApplier;
    public EntityEffectApplier blastEntityEffectApplier;
    public GameObject blastParent;

    public void Initialize(EntityConfig entityConfig)
    {
        gooEntityEffectApplier.entityEffectToApply = entityConfig.gooEntityEffect;
        blastEntityEffectApplier.entityEffectToApply = entityConfig.blastEntityEffect;

        blastParent.SetActive(entityConfig.blastEntityEffect != null);
    }
}
