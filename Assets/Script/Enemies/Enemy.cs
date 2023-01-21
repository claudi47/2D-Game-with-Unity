using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IKillable<int>
{
    public float tmp;
    public float speed;
    public float distance;
    public int maxhealth = 5;
    public int currenthealth;
    public GameObject Blood;
    public Transform RayPoint;
    private bool goingRight = true;
    public Animator animator;
    public Transform AttackPoint;
    public LayerMask PlayerLayers;
    public float AttackRange = 0.5f;
    public int damagePlayer = 2;
    public bool dead=false;

    void Start()
    {
        currenthealth = maxhealth;
    }

    protected void Update()
    {
        if (currenthealth <= 0)
        {
            dead = true;
            Die();
        }
        if (speed != 0) tmp = speed;
 
        if (currenthealth > 0) Move();
    }

    public void Die()
    {
        animator.SetBool("isDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

    }

    public void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D hit2D = Physics2D.Raycast(RayPoint.position, Vector2.down, distance);
        if (hit2D.collider == false)
        {
            if (goingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                goingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                goingRight = true;
            }
        }
    }

    public void EnemyTakeDamage(int damage)
    {
        if (currenthealth > 0)
        {
            currenthealth -= damage;

            animator.SetTrigger("Hurt");
            Instantiate(Blood, transform.position, Quaternion.identity);

            StartCoroutine(Idle());
        }
    }

    public IEnumerator Idle()
    {  
        speed = 0;
        yield return new WaitForSeconds(1.35f);
        speed = tmp;
        animator.SetBool("isWalking", true);
        yield return 0;
    }

    public virtual void Attack()
    {
        animator.SetTrigger("isAttacking");
        animator.SetBool("isWalking", false);
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, PlayerLayers);

        foreach (Collider2D player in HitEnemies)
        {
            player.GetComponent<Player>().PlayerTakeDamage(damagePlayer);
        }

        speed = 0;
    }

    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dead == false && collision.isTrigger != true && collision.CompareTag("Player"))
        {

            Vector2 player = GameObject.FindGameObjectWithTag("Player").transform.position;
            if (player.x < transform.position.x && goingRight)
            {
                transform.eulerAngles = new Vector2(0, -180);
                goingRight = false;
            }
            if (player.x > transform.position.x && !goingRight)
            {
                transform.eulerAngles = new Vector2(0, 0);
                goingRight = true;
            }
            Attack();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (dead == false && collision.CompareTag("Player"))
        {
            speed = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Idle());
        }
    }

}

