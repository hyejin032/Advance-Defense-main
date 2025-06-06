using System.Threading.Tasks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Playerspeed = 1f;
    public float PlayerHp = 5f;
    public float MaxPlayerHp = 5f;
    public float Cooldown = 0.7f;
    public float AttackRange = 0.1f;

    private bool isDead = false;

    public Vector3 inputVec;

    public Transform attackPoint;
    public LayerMask enemyLayer;

    Rigidbody2D rigid;
    SpriteRenderer sprite;
    Animator anima;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anima = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        Cooldown += Time.deltaTime;
        if (Cooldown >= 0.7f)
        {
            Attack();
        }
    }
    void FixedUpdate()
    {
        if (isDead) return;
        Move();
    }
    void Move()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        bool enemyInFront = false;
        if (inputVec.x != 0)
        {
            Vector2 direction = inputVec.x > 0 ? Vector2.right : Vector2.left;
            float checkDistance = 0.4f;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.6f, enemyLayer);
            if (hit.collider != null)
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy == null || !enemy.IsDead)
                {
                    inputVec.x = 0;
                }
            }

            Vector2 checkPos = (Vector2)transform.position + direction * checkDistance;
            Collider2D overlap = Physics2D.OverlapCircle(checkPos, 0.2f, enemyLayer);
            if (overlap != null)
            {
                Enemy enemy = overlap.GetComponent<Enemy>();
                if (enemy == null || !enemy.IsDead)
                {
                    enemyInFront = true;
                }
            }

            if (enemyInFront)
            {
                inputVec.x = 0;
            }
        }
        float newX = transform.position.x + inputVec.x * Playerspeed * Time.deltaTime;
        float minX = -7f;
        float maxX = 7f;
        newX = Mathf.Clamp(newX, minX, maxX);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        if (inputVec.x != 0)
        {
            Vector3 attackPos = attackPoint.localPosition;
            attackPos.x = Mathf.Abs(attackPos.x) * Mathf.Sign(inputVec.x);
            attackPoint.localPosition = attackPos;
        }
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, AttackRange, enemyLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(1);
                }
                else
                {
                    Base baseScript = enemy.GetComponent<Base>();
                    if (baseScript != null)
                    {
                        baseScript.TakeDamage(1);
                    }
                }
            }

            anima.SetTrigger("doAttack");
            Cooldown = 0f;
        }
    }
    public void TakeDamage(int damage)
    {
        PlayerHp -= damage;
        PlayerHp = Mathf.Max(PlayerHp, 0);
        if (PlayerHp > 0)
        {
            anima.SetTrigger("doHit");
        }
        else
        {
            if (isDead) return;
            anima.SetTrigger("doDie");
            Time.timeScale = 0f;
        }
    }
    void LateUpdate()
    {
        if (inputVec.x != 0)
        {
            sprite.flipX = inputVec.x < 0;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(attackPoint.position, AttackRange);
        }
    }

}
