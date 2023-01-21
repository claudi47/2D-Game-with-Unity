using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 4f;
    public float lifeTime = 2f;
    private Rigidbody2D rb;

    void Start()
    {
        Invoke("DestroyFireBall", lifeTime);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.EnemyTakeDamage(2);
            DestroyFireBall();
        }
    }

    public void DestroyFireBall()
    {
        Destroy(gameObject);
    }

}