using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private float falldelay = 1f;
    Vector2 t;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        t = gameObject.transform.position;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            StartCoroutine(fall());
        }


    }

    IEnumerator fall()
    {
        yield return new WaitForSeconds(falldelay);
        rb.isKinematic = false;
        bc.enabled = false;
        StartCoroutine(realive());
        yield return 0;
    }

    IEnumerator realive()
    {
        yield return new WaitForSeconds(5);
        rb.isKinematic = true;
        bc.enabled = true;
        gameObject.transform.position = t;
        rb.velocity = new Vector2(0, 0);
        gameObject.SetActive(true);
        yield return 0;
    }

    void Update()
    {

    }

}
