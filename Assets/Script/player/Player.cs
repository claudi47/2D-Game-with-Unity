using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, PlayerInterface<int>
{
    Rigidbody2D body;
    public float moveSpeed = 3f;
    public float jumpForce = 1.4f;
    bool isJumping = false;
    public static Animator animator;

    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public LayerMask EnemyLayers;
    public int AttackDamage = 1;
    public float AttackRate = 2f;
    float NextattackTime = 0f;

    public static int maxhealth = 10;
    public static int currenthealth = 10;
    public Collider2D player;
    public HealthBar healthBar;

    public Vector2 respawnPoint;
    public GameObject CheckPoint;
    public GameObject beginningSpawn;
    public bool hit_check = false;

    public Transform FirePoint;
    public GameObject FireBall;
    public bool right = true;

    public static bool isPlay;

    public static List<int> scores = new List<int>();
    public static List<string> names = new List<string>();
    public static string name;


    public void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currenthealth = maxhealth;
        healthBar.SetMaxHealth(maxhealth);
        isPlay = true;
        CoinManager.coins = 0;
    }

    public void Update()
    {
        if (currenthealth <= 0)
        {
            LifeUpManager.getInstance().AddLife(-1);
            Die();
        }

        movement();

        jumping();

        if (Time.time >= NextattackTime)
        {

            if (Input.GetKeyDown(KeyCode.Z))
            {
                Attack();
                NextattackTime = Time.time + 1f / AttackRate;
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                Cast();
                NextattackTime = Time.time + 1f / AttackRate;
            }
        }

        if (hit_check) respawnPoint = CheckPoint.transform.position;
        else respawnPoint = beginningSpawn.transform.position;

    }

    public void Cast()
    {
        animator.SetTrigger("Cast");
        if (!right) Instantiate(FireBall, FirePoint.position, Quaternion.Euler(0, 180, 0));
        else Instantiate(FireBall, FirePoint.position, Quaternion.identity);
    }

    public void movement()
    {
        float h = Input.GetAxis("Horizontal");
        Vector2 velocity;
        
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift))) velocity = new Vector2(Vector2.right.x * moveSpeed * 2 * h, body.velocity.y);
        else velocity = new Vector2(Vector2.right.x * moveSpeed * h, body.velocity.y);

        body.velocity = velocity;

        if (velocity.x != 0.0) animator.SetBool("isWalking", true);
        else animator.SetBool("isWalking", false);

        if (velocity.x < 0)
        {
            body.transform.localScale = new Vector2(-2f, 2f);
            right = false;
        }
        if (velocity.x > 0)
        {
            body.transform.localScale = new Vector2(2f, 2f);
            right = true;
        }


    }

    public void jumping()
    {
        if (isJumping)
        {
            if (body.velocity.y == 0.0)
            {
                isJumping = false;
                animator.SetBool("isJumping", false);
            }
        }
        else
        {
            if (Input.GetAxis("Jump") > 0)
            {
                body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
                animator.SetBool("isJumping", true);
            }
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);

        foreach (Collider2D enemy in HitEnemies)
        {
            enemy.GetComponent<Enemy>().EnemyTakeDamage(AttackDamage);
        }

    }

    public void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }

    public void Die()
    {
        if (LifeUpManager.life > 0)
        {
            animator.SetBool("isDead", true);
            transform.position = respawnPoint;
            currenthealth = 10;
            healthBar.SetHealth(currenthealth);
            animator.SetBool("isDead", false);
            ExtensionMethods.ResetTransformation(body.transform);
            ExtensionMethods.Freeze(body);
        }
        else
        {
            SceneManager.LoadScene(0);
        }

    }

    public void PlayerTakeDamage(int damageEnemy)
    {
        StartCoroutine(WaitDamage(damageEnemy));
    }

    public static void addHeart (int health_bonus)
    {
        if (currenthealth < maxhealth)
        {
            currenthealth += health_bonus;
            HealthBar.getInstance().SetHealth(currenthealth);
        }

    }

    public IEnumerator WaitDamage( int damageEnemy)
    {
        yield return new WaitForSeconds(0.3f);
        currenthealth -= damageEnemy;
        animator.SetTrigger("Hurt");

        healthBar.SetHealth(currenthealth);
        yield return 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Home"))
        {
            scores.Add(Punteggio.score1);
            names.Add(Player.name);
            SceneManager.LoadScene(0);
        }

        if (collision.CompareTag("Distruggi"))
        {
            LifeUpManager.getInstance().AddLife(-1);
            Die();
        }
    }

}