using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Vector3 mouseWorldPosition;

    [Header("Attack")]
    public float attackDuration = 0.5f;
    float lastAttackTime;

    public bool IsAttacking => Time.time < lastAttackTime + attackDuration;
    bool IsMouseOverGameWindow { get { return !(0 > Input.mousePosition.x || 0 > Input.mousePosition.y || Screen.width < Input.mousePosition.x || Screen.height < Input.mousePosition.y); } }


    [Header("Etc")]
    public Animator mainAnimator;

    [Header("Debug")]
    public float distanceWatch = 0f;

    Plane plane = new Plane(Vector3.up, 0);

    Entity entity;

    void Awake()
    {
        entity = GetComponent<Entity>();
    }

    private void Update()
    {
        MovementControl();
        AttackControl();
    }

    void MovementControl()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float distance))
            mouseWorldPosition = ray.GetPoint(distance);

        if (transform.position != mouseWorldPosition && !IsAttacking && IsMouseOverGameWindow)
        {
            transform.LookAt(mouseWorldPosition);

            float finalSpeed = moveSpeed;
            if (entity.HasEffectOfType(EntityEffectType.SLOW))
            {
                List<EntityEffect> _cachedEffects = entity.GetEffectsByType(EntityEffectType.SLOW);
                foreach (EntityEffect effect in _cachedEffects)
                    finalSpeed *= effect.effectConfig.power;
            }

            transform.position = Vector3.MoveTowards(transform.position, mouseWorldPosition, finalSpeed * Time.deltaTime);
        }
    }

    void AttackControl()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (!IsAttacking)
            {
                lastAttackTime = Time.time;
                mainAnimator.SetTrigger("attack");
            }

        }
    }
}
