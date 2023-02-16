using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Enemy : MonoBehaviour
{
    public Vector3 targetPosition;
    public float speed = 1f;

    public int debugX = -100;
    public int debugZ = -100;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            GameManager.Instance.currentEnemyCount--;
            Destroy(gameObject);
        }
    }

    public void Initialize(Vector3 targetPosition, int initialX, int initialZ)
    {
        this.targetPosition = targetPosition;
        debugX = initialX;
        debugZ = initialZ;
    }
}
