using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEffectApplier : MonoBehaviour
{
    [Header("Control")]
    public EntityEffectConfig entityEffectToApply;
    public bool destroyOnApply = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            if (entityEffectToApply != null)
                other.GetComponent<Entity>().ApplyEffect(entityEffectToApply);
            if (destroyOnApply)
                Destroy(gameObject);
        }
    }
}
