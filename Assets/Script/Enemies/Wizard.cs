using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy
{
    public Transform player;
    private float TimeBtwShots;
    public float startTimeBtwShots;
    public GameObject bullet;

    public bool goingright = false;
    [SerializeField] float agroRange;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        TimeBtwShots = startTimeBtwShots;
    }

    private new void Update()
    {
        base.Update();
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if(distToPlayer < agroRange) Attack();
    }

    public override void Attack()
    {

        if (TimeBtwShots <= 0)
        {
            animator.SetTrigger("isAttacking");
            if (goingright) Instantiate(bullet, transform.position, Quaternion.identity);
            else Instantiate(bullet, transform.position, Quaternion.Euler(0, 180, 0));
            TimeBtwShots = startTimeBtwShots;
        }
        else
        {
            TimeBtwShots -= Time.deltaTime;
        }
    }
}