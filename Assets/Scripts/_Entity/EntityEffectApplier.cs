using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEffectApplier : MonoBehaviour
{
    public EntityEffectConfig entityEffectConfig;
    public bool destroyOnApply = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            if (entityEffectConfig != null)
                other.GetComponent<Entity>().ApplyEffect(entityEffectConfig);
            if (destroyOnApply)
                Destroy(gameObject);
        }
    }
}
