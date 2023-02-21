using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    Player player;

    private void Start()
    {
        if (player == null)
            player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (player.isAttacking)
        {
            if (other.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(this);
            }
        }
    }
}
