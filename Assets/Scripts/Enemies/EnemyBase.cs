using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Control")]
    public float speed = 5f;

    public Entity entity;
    public EntityConfig entityConfig => entity.entityConfig;

    public void Initialize(Vector3 targetPosition)
    {
        Vector3 direction;
        direction = targetPosition - transform.position;
        direction.y = 0f;
        direction = direction.normalized;

        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    private void Start()
    {
        speed = entityConfig.movementSpeed;
    }

    private void Update()
    {
        if (GameManager.Instance.player != null)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(GameManager.Instance.player.transform.position - transform.position, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, entityConfig.rotationToPlayer * Time.deltaTime * 1000f);
        }

        transform.Translate(Vector3.forward * speed * entity.GetSpeedMultiplier() * Time.deltaTime);
    }

    public void OnContactWithPlayerBody(PlayerBodyCollider body)
    {
        //Vector3 direction;
        //direction = transform.position - body.transform.position;
        //direction = direction.normalized;
        //transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

        //if (entityConfig.touchEffectToPlayer_EFC != null)
        //    body.player.entity.ApplyEffect(entityConfig.touchEffectToPlayer_EFC);
    }

    public void ReverseOnContact(Transform other)
    {
        Vector3 direction;
        direction = transform.position - other.position;
        direction = direction.normalized;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

}


