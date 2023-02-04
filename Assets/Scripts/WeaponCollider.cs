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
        print(1);
        if (player.IsAttacking)
        {
            print(2);
            if (other.CompareTag("Enemy"))
            {
                print(3);
                Destroy(other.gameObject);
            }
        }
    }
}
