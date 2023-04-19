using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float minDistanceToMove = 0.25f;
    public Vector3 mouseWorldPosition;

    bool IsMouseOverGameWindow { get { return !(0 > Input.mousePosition.x || 0 > Input.mousePosition.y || Screen.width < Input.mousePosition.x || Screen.height < Input.mousePosition.y); } }

    [Header("Etc")]
    public Animator mainAnimator;
    public Animator movementAnimator;
    public Collider weaponCollider;
    public bool isAttacking = false;

    Plane plane = new Plane(Vector3.up, 0);

    [HideInInspector] public Entity entity;

    void Awake()
    {
        entity = GetComponent<Entity>();
        entity.onDeath += OnDeath;

        GameManager.Instance.player = this;
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

        bool isMoving = false;

        //bool isFarEnoughFromMouse = transform.position != mouseWorldPosition;
        bool isFarEnoughFromMouse = Vector3.Distance(transform.position, mouseWorldPosition) >= minDistanceToMove;

        if (isFarEnoughFromMouse && !isAttacking && IsMouseOverGameWindow && GameManager.Instance.roundIsOn)
        {
            float finalSpeed = moveSpeed * entity.GetSpeedMultiplier();
            transform.position = Vector3.MoveTowards(transform.position, mouseWorldPosition, finalSpeed * Time.deltaTime);

            isMoving = finalSpeed > 0f;

            if (!entity.RotationIsPrevented())
            {
                if (mouseWorldPosition != transform.position)
                {
                    Quaternion desiredRotation = Quaternion.LookRotation(mouseWorldPosition - transform.position, Vector2.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, 1080f * Time.deltaTime);
                }

                //transform.LookAt(mouseWorldPosition);
            }
        }

        movementAnimator.SetBool("isMoving", isMoving);
    }

    void AttackControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isAttacking && !entity.AttackIsPrevented())
                mainAnimator.SetTrigger("attack");
        }
        weaponCollider.enabled = isAttacking;
    }

    public void OnBodyContactWithEnemy(EnemyBase enemy)
    {
        // apply stun
    }

    public void ToggleSwingingState (bool active)
    {
        isAttacking = active;
        //Debug.Log("[P] Swing state: " + isAttacking, this);
    }

    void OnDeath()
    {
        print(1);
        GameManager.Instance.FinishRound(false);
    }
}
