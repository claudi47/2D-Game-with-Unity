using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    public float speed = 4f;
    public float lifeTime = 0.5f;
    private Rigidbody2D rb;

    void Start()
    {
        Invoke("DestroyIce", lifeTime);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.PlayerTakeDamage(1);
            DestroyIce();
        }
    }

    public void DestroyIce()
    {
        Destroy(gameObject);
    }

}