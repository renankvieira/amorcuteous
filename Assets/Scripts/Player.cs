using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Vector3 mouseWorldPosition;

    bool IsMouseOverGameWindow { get { return !(0 > Input.mousePosition.x || 0 > Input.mousePosition.y || Screen.width < Input.mousePosition.x || Screen.height < Input.mousePosition.y); } }

    [Header("Etc")]
    public Animator mainAnimator;
    public bool isAttacking = false;

    Plane plane = new Plane(Vector3.up, 0);

    [HideInInspector] public Entity entity;

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

        if (transform.position != mouseWorldPosition && !isAttacking && IsMouseOverGameWindow)
        {
            bool isStunned = entity.HasEffectOfType(EntityEffectType.STUN);
            bool isSlowed = entity.HasEffectOfType(EntityEffectType.SLOW);

            float finalSpeed = moveSpeed;
            if (isStunned)
            {
                finalSpeed = 0f;
            }
            else if (isSlowed)
            {
                List<EntityEffect> _cachedEffects = entity.GetEffectsByType(EntityEffectType.SLOW);
                foreach (EntityEffect effect in _cachedEffects)
                    finalSpeed *= effect.effectConfig.power;
            }

            transform.position = Vector3.MoveTowards(transform.position, mouseWorldPosition, finalSpeed * Time.deltaTime);

            if (!isStunned)
                transform.LookAt(mouseWorldPosition);
        }
    }

    void AttackControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isAttacking && !entity.HasEffectOfType(EntityEffectType.STUN))
            {
                mainAnimator.SetTrigger("attack");
            }

        }
    }

    public void OnBodyContactWithEnemy(Enemy enemy)
    {
        // apply stun
    }

    public void ToggleSwingingState (bool active)
    {
        isAttacking = active;
    }
}
