using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Config")]
    [Expandable] public EnemyConfig enemyConfig;

    [Header("Control")]
    public Vector3 direction = Vector3.forward;
    public float speed = 5f;

    public Entity entity;

    public void Initialize(Vector3 targetPosition)
    {
        direction = targetPosition - transform.position;
        direction.y = 0f;
        direction = direction.normalized;
    }

    private void Start()
    {
        speed = enemyConfig.movementSpeed;
    }

    private void Update()
    {
        if (GameManager.Instance.player != null)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(GameManager.Instance.player.transform.position - transform.position, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, enemyConfig.rotationToPlayer * Time.deltaTime * 1000f);



        }

        //transform.position = Vector3.MoveTowards(transform.position, transform.position + (direction * speed), speed * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void OnContactWithPlayerBody(PlayerBodyCollider body)
    {
        direction = transform.position - body.transform.position;
        direction = direction.normalized;

        //if (entityEffectOnPlayerContact != null)
            //body.player.entity.ApplyEffect(entityEffectOnPlayerContact);
    }
}


