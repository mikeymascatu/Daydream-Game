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
    [SerializeField] AudioSource sfx;       // assign (or it will try to auto-find in Awake)
    [SerializeField] AudioClip swingSfx;    // whoosh
    [SerializeField] AudioClip hitSfx;      // impact

    [Range(0f, 1f)] public float swingVol = 1f;
    [Range(0f,1f)] public float hitVol   = 1f;

    void Awake()
    {
        if (!attackPos) attackPos = transform;
        if (!sfx) sfx = GetComponent<AudioSource>();
        if (!sfx) sfx = GetComponentInChildren<AudioSource>();
        if (sfx) { sfx.playOnAwake = false; sfx.spatialBlend = 0f; } // 2D so it’s not quiet by distance
    }

    void Update()
    {

        if (timeBtwAttack > 0f)
        {
            timeBtwAttack -= Time.deltaTime;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 1) Do the hit check first
            var hits  = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            int hitCount = 0;

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].TryGetComponent<Enemy>(out var enemy) && enemy != null)
                {
                    enemy.TakeDamage(damage);
                    hitCount++;
                }
            }

            // 2) Play either hit OR swing (not both)
            if (hitCount > 0)
            {
                if (sfx && hitSfx)
                {
                    sfx.Stop(); // in case a whoosh was still playing
                    sfx.PlayOneShot(hitSfx, hitVol);
                }
            }
            else
            {
                if (sfx && swingSfx)
                    sfx.PlayOneShot(swingSfx, swingVol);
            }

            // 3) Start cooldown only after an attack
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