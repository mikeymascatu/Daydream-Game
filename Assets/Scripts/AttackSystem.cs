using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AttackSystem : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack = 0.3f;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange = 0.8f;
    public int damage = 1;

    [Header("Audio")]
    [SerializeField] private AudioSource sfx;    // assign in Inspector (or will auto-find)
    [SerializeField] private AudioClip swingSfx; // normal swing/whoosh
    [SerializeField] private AudioClip hitSfx;   // impact when an enemy is hit
    [Range(0f,1f)] public float swingVol = 1f;
    [Range(0f,1f)] public float hitVol   = 1f;

    
    void Awake()
    {
        if (!attackPos) attackPos = transform;          // fallback so we never null-ref
        if (!sfx) sfx = GetComponent<AudioSource>();
        if (!sfx) sfx = GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        if (timeBtwAttack > 0f)
        {
            timeBtwAttack -= Time.deltaTime;
            return;
        }

        // Fire once per key press
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 1) Play swing immediately (whoosh)
            if (sfx && swingSfx) sfx.PlayOneShot(swingSfx, swingVol);

            // 2) Hit check + damage
            var hits = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            bool anyHit = false;

            for (int i = 0; i < hits.Length; i++)
            {
                // Safer than GetComponent<Enemy>() alone
                if (hits[i].TryGetComponent<Enemy>(out var enemy) && enemy != null)
                {
                    enemy.TakeDamage(damage);
                    anyHit = true;
                }
            }

            // 3) If anything was hit, also play the impact sound
            if (anyHit && sfx && hitSfx) sfx.PlayOneShot(hitSfx, hitVol);

            // Start cooldown only after a real attack
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