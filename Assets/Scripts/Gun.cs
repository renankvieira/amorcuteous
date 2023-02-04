using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet bulletPrefab;
    public float shotDelay = 0.2f;

    public float timeOfLastShot = 0f;

    public float TimeSinceLastShot => Time.time - timeOfLastShot;
    public bool EnoughTimeSinceLastShot => TimeSinceLastShot >= shotDelay;

    void Update()
    {
        if (EnoughTimeSinceLastShot)
        {
            timeOfLastShot = Time.time;
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }
}
