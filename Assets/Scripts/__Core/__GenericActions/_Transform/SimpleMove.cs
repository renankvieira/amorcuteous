using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    [Header("Config")]
    public Vector3 moveDirection = Vector3.right;
    public float speedMultiplier = 1f;

    public MoveEvent moveEvent = MoveEvent.ON_UPDATE;
    public MoveMethod moveMethod = MoveMethod.BY_TRANSFORM;

    [Header("Control")]
    public bool canMove = true;
    public bool autoMove = true;

    [Header("References")]
    public Rigidbody rb;

    public void Initialize(Vector3 direction, float speedMultiplier = 0f)
    {
        moveDirection = direction;
        if (speedMultiplier > 0f)
            this.speedMultiplier = speedMultiplier;
    }

    private void Update()
    {
        if (moveEvent == MoveEvent.ON_UPDATE && autoMove)
            Move(moveDirection);
    }

    private void LateUpdate()
    {
        if (moveEvent == MoveEvent.ON_LATE_UPDATE && autoMove)
            Move(moveDirection);
    }

    private void FixedUpdate()
    {
        if (moveEvent == MoveEvent.ON_FIXED_UPDATE && autoMove)
            Move(moveDirection);
    }

    public void Move(Vector3 movementIntention_)
    {
        if (!canMove) return;

        Vector3 deltaPosition = movementIntention_ * (speedMultiplier * Time.deltaTime);

        if (moveMethod == MoveMethod.BY_TRANSFORM)
            transform.position += deltaPosition;

        if (moveMethod == MoveMethod.BY_RIGIDBODY)
            rb.MovePosition(rb.position + deltaPosition);
    }

    public enum MoveMethod
    {
        DEFAULT = 0,
        BY_TRANSFORM = 10,
        BY_RIGIDBODY = 20
    }

    public enum MoveEvent
    {
        DEFAULT = 0,
        ON_UPDATE = 10,
        ON_LATE_UPDATE = 20,
        ON_FIXED_UPDATE = 30
    }
}
