using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Enemy : MonoBehaviour
{
    //public Vector3 targetPosition;
    public Vector3 direction = Vector3.forward;
    public float speed = 1f;

    public int debugX = -100;
    public int debugZ = -100;

    public GameObject deathBloodPrefab;

    bool isDead = false;

    public EntityEffectConfig entityEffectOnPlayerContact;

    public void Initialize(Vector3 targetPosition, int initialX, int initialZ)
    {
        direction = targetPosition - transform.position;
        direction.y = 0f;
        direction = direction.normalized;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + (direction * speed), speed * Time.deltaTime);
    }

    public void TakeDamage(WeaponCollider weaponCollider)
    {
        if (isDead)
            return;

        Die(true, weaponCollider.transform);
    }

    public void Die(bool withEffect, Transform killer)
    {
        if (isDead)
            return;
        isDead = true;

        Destroy(gameObject);
        SpawnManager.Instance.currentEnemyCount--;

        if (deathBloodPrefab != null && withEffect)
        {
            GameObject deathObject = Instantiate(deathBloodPrefab, transform.position, killer.transform.rotation);
            Destroy(deathObject, 5f);
        }
    }

    public void OnContactWithPlayerBody(PlayerBodyCollider body)
    {
        direction = direction *= -1f;

        if (entityEffectOnPlayerContact != null)
            body.player.entity.ApplyEffect(entityEffectOnPlayerContact);
    }
}
