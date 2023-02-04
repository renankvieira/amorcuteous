using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;

    public float timeToLive = 2f;
    float spawnTime = 0f;

    public GameObject destroyParticles;

    void Start()
    {
        spawnTime = Time.time;
        rb.velocity = speed * transform.forward;
    }

    private void Update()
    {
        if (Time.time > spawnTime + timeToLive)
            DestroyBullet();
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
        if (destroyParticles != null)
            Instantiate(destroyParticles, transform.position, transform.rotation);
    }

}
