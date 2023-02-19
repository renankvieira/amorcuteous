using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public bool destroyEnemyOnTouch = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!destroyEnemyOnTouch)
            return;

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Die(false, null);
        }
    }
}
