using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{
    public bool destroyEnemyOnExit = false;

    private void OnTriggerExit(Collider other)
    {
        if (!destroyEnemyOnExit)
            return;

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Die(false, null);
        }
    }
}
