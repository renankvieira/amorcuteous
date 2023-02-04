using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float maxHp = 1f;
    public float currentHp = 1f;

    public float destroyDelayOnDeath = 0f;

    bool isDead = false;

    public GameObject deathPS;

    public UnityEngine.Events.UnityEvent onDeath;

    public void TakeDamage(DamageDealer dealer, float damage)
    {
        if (isDead)
            return;

        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        currentHp -= damage;
        if (currentHp <= 0f)
        {
            currentHp = 0f;
            isDead = true;

            if (destroyDelayOnDeath <= 0f)
                Destroy(gameObject);
            else
                Destroy(gameObject, destroyDelayOnDeath);

            if (deathPS != null)
            {
                //Destroy(Instantiate(deathPS, transform.position, deathPS.transform.rotation), 1f);
                Instantiate(deathPS, transform.position, deathPS.transform.rotation);
            }

            onDeath.Invoke();
        }
    }
}
