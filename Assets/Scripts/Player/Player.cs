using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Input")]
    public bool moveByKeyboard = true;
    public bool useFakeInput = false;

    Vector3 mouseWorldPosition = Vector3.right;
    bool IsMouseOverGameWindow { get { return !(0 > Input.mousePosition.x || 0 > Input.mousePosition.y || Screen.width < Input.mousePosition.x || Screen.height < Input.mousePosition.y); } }

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float minDistanceToMove = 0.25f;
    public float movementRotationSpeed = 1080f;

    [Header("Attack")]
    public bool attackTowardsMouse = true;
    public bool rotateToMouseImmediately = false;
    public bool rotateToMouseSmoothly = true;
    public float smoothSpeed = 1080f;
    public float rotationDuration = 0.2f;
    public TrailRenderer swordTrail;
    [ReadOnly] public bool isAttacking = false;

    [Header("Etc")]
    public bool alwaysLookTowardsMouse = false;
    public Animator mainAnimator;
    public Animator movementAnimator;
    public Collider weaponCollider;

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
        UpdateMousePosition();

        MovementControlMouse();
        MovementControlWasd();
        AttackControl();
    }

    void UpdateMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float distance))
            mouseWorldPosition = ray.GetPoint(distance);

        if (useFakeInput)
        {
            switch (Mathf.FloorToInt(Time.time * 1.5f) % 4)
            {
                case 0:
                    mouseWorldPosition = transform.position + Vector3.forward;
                    break;
                case 1:
                    mouseWorldPosition = transform.position + Vector3.left;
                    break;
                case 2:
                    mouseWorldPosition = transform.position + Vector3.back;
                    break;
                case 3:
                    mouseWorldPosition = transform.position + Vector3.right;
                    break;
                case 4:
                    mouseWorldPosition = transform.position;
                    break;
            }
        }

        if (alwaysLookTowardsMouse)
        {
            if (!isAttacking && !entity.RotationIsPrevented())
            {
                Quaternion desiredRotation = Quaternion.LookRotation(mouseWorldPosition - transform.position, Vector2.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, movementRotationSpeed * Time.deltaTime);
            }
        }
    }

    void MovementControlMouse()
    {
        if (moveByKeyboard)
            return;

        bool isMoving = false;

        //bool isFarEnoughFromMouse = transform.position != mouseWorldPosition;
        bool isFarEnoughFromMouse = Vector3.Distance(transform.position, mouseWorldPosition) >= minDistanceToMove;

        if (isFarEnoughFromMouse && !isAttacking && (IsMouseOverGameWindow || useFakeInput) && GameManager.Instance.roundIsOn)
        {
            float finalSpeed = moveSpeed * entity.GetSpeedMultiplier();

            transform.position = Vector3.MoveTowards(transform.position, mouseWorldPosition, finalSpeed * Time.deltaTime);

            isMoving = finalSpeed > 0f;

            if (!entity.RotationIsPrevented())
            {
                if (mouseWorldPosition != transform.position)
                {
                    Quaternion desiredRotation = Quaternion.LookRotation(mouseWorldPosition - transform.position, Vector2.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, movementRotationSpeed * Time.deltaTime);
                }

                //transform.LookAt(mouseWorldPosition);
            }
        }

        movementAnimator.SetBool("isMoving", isMoving);
    }

    void MovementControlWasd()
    {
        if (!moveByKeyboard)
            return;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
        bool isMoving = false;

        if (!isAttacking && GameManager.Instance.roundIsOn)
        {
            float finalSpeed = moveSpeed * entity.GetSpeedMultiplier();
            transform.position += input * finalSpeed * Time.deltaTime;

            isMoving = (input * finalSpeed).sqrMagnitude > 0.1f;

            if (!entity.RotationIsPrevented())
            {
                if (input.sqrMagnitude > 0.1f && !alwaysLookTowardsMouse)
                {
                    //Quaternion desiredRotation = Quaternion.LookRotation(mouseWorldPosition - transform.position, Vector2.up);
                    Vector3 rotationDirectionRandomizer = new Vector3(Random.Range(-0.001f, 0.001f), 0f, Random.Range(-0.001f, 0.001f));

                    Quaternion desiredRotation = Quaternion.LookRotation(input + rotationDirectionRandomizer, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, movementRotationSpeed * Time.deltaTime);
                }

                //transform.LookAt(mouseWorldPosition);
            }
        }

        movementAnimator.SetBool("isMoving", isMoving);
    }

    void AttackControl()
    {
        if (!isAttacking && !entity.AttackIsPrevented())
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (attackTowardsMouse && !entity.RotationIsPrevented())
                {
                    if (rotateToMouseImmediately)
                        transform.rotation = Quaternion.LookRotation(mouseWorldPosition - transform.position, Vector2.up);
                    if (rotateToMouseSmoothly)
                        StartCoroutine(RotateOnAttack());
                }
                mainAnimator.SetTrigger("attack");
            }
            else if (Input.GetMouseButton(1))
            {
                mainAnimator.SetTrigger("whirlwind");
            }
        }

        weaponCollider.enabled = isAttacking;
    }

    IEnumerator RotateOnAttack()
    {
        Quaternion desiredRotation = Quaternion.LookRotation(mouseWorldPosition - transform.position, Vector2.up);

        if (mouseWorldPosition.x == transform.position.x && mouseWorldPosition.z == transform.position.z)
            yield return null;

        float elapsedTime = 0f;

        while (elapsedTime < rotationDuration && !entity.RotationIsPrevented())
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, smoothSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();

            if (!isAttacking)
                yield return null;
        }
    }


    public void OnBodyContactWithEnemy(EnemyBase enemy)
    {
        // apply stun
    }

    public void ToggleSwingingState (bool active)
    {
        isAttacking = active;
        //Debug.Log("[P] Swing state: " + isAttacking, this);
        swordTrail.emitting = isAttacking;
    }

    void OnDeath()
    {
        GameManager.Instance.FinishRound(false);
    }
}
