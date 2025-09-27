using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack <= 0)
        {

            if (Input.GetKey(KeyCode.Q))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
        
}

/*
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack = 0.3f;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange = 0.8f;
    public int damage = 1;

    void Awake()
    {
        // Fallback so you don't null-ref if you forget to assign attackPos
        if (!attackPos) attackPos = transform;
    }

    void Update()
    {
        if (timeBtwAttack > 0f)
        {
            timeBtwAttack -= Time.deltaTime;
            return;
        }

        // Only trigger when actually pressed this frame
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // OverlapCircleAll will only return colliders on layers in whatIsEnemies
            Collider2D[] hits = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

            for (int i = 0; i < hits.Length; i++)
            {
                // Prefer an interface (see below), but keep a fallback to your Enemy script
                if (hits[i].TryGetComponent<IDamageable>(out var dmg))
                {
                    dmg.TakeDamage(damage);
                }
                else
                {
                    var enemy = hits[i].GetComponent<Enemy>();
                    if (enemy) enemy.TakeDamage(damage);
                }
            }

            // Reset cooldown ONLY after a real attack
            timeBtwAttack = startTimeBtwAttack;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!attackPos) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}

// Optional: a simple interface every damageable thing can implement
public interface IDamageable { void TakeDamage(int amount); }
*/