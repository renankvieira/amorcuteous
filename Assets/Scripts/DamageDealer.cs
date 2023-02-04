using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float damageOnTouch = 1f;
    public float damagePerSecond = 1f;

    public bool destroyOnTouch = false;

    private void OnTriggerEnter(Collider other)
    {
        if (damageOnTouch <= 0f)
            return;

        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(this, damageOnTouch);
            if (destroyOnTouch)
                Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (damagePerSecond <= 0f)
            return;

        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(this, damagePerSecond * Time.deltaTime);
            if (destroyOnTouch)
                Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (damageOnTouch <= 0f)
            return;

        Damageable damageable = collision.gameObject.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(this, damageOnTouch);
            if (destroyOnTouch)
                Destroy(gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (damagePerSecond <= 0f)
            return;

        Damageable damageable = collision.gameObject.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(this, damagePerSecond * Time.deltaTime);
            if (destroyOnTouch)
                Destroy(gameObject);
        }
    }

}
