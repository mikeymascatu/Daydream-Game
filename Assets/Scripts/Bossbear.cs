using UnityEngine;

public class BossBear : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] Transform player;                 // auto-found by tag if left empty
    [SerializeField] string playerTag = "Player";
    [SerializeField] LayerMask whatIsPlayer;

    [Header("Melee")]
    [SerializeField] Transform meleePoint;             // a child transform in front of the bear
    [SerializeField] float meleeRange = 1.4f;
    [SerializeField] int meleeDamage = 1;
    [SerializeField] float meleeCooldown = 1.0f;

    [Header("Ranged")]
    [SerializeField] Transform firePoint;              // where projectiles spawn
    [SerializeField] BossProjectile projectilePrefab;  // assign prefab below
    [SerializeField] float projectileSpeed = 8f;
    [SerializeField] int projectileDamage = 1;
    [SerializeField] float projectileCooldown = 1.5f;

    [Header("General")]
    [SerializeField] float faceFlipThreshold = 0.05f;  // deadzone for flipping

    float meleeTimer;
    float shootTimer;
    bool facingRight = true;

    void Awake()
    {
        if (!player)
        {
            var p = GameObject.FindGameObjectWithTag(playerTag);
            if (p) player = p.transform;
        }
        if (!meleePoint) meleePoint = transform; // fallback
        if (!firePoint)  firePoint  = transform; // fallback
    }

    void Update()
    {
        if (meleeTimer > 0f) meleeTimer -= Time.deltaTime;
        if (shootTimer > 0f) shootTimer -= Time.deltaTime;

        if (!player) return;

        FacePlayer();

        float dist = Vector2.Distance(player.position, transform.position);
        bool canMelee = dist <= meleeRange * 1.1f && Mathf.Abs(player.position.y - transform.position.y) < meleeRange;

        if (canMelee && meleeTimer <= 0f)
        {
            DoMelee();
            meleeTimer = meleeCooldown;
        }
        else if (shootTimer <= 0f)
        {
            ShootAtPlayer();
            shootTimer = projectileCooldown;
        }
    }

    void DoMelee()
    {
        var hits = Physics2D.OverlapCircleAll(meleePoint.position, meleeRange, whatIsPlayer);
        foreach (var h in hits)
        {
            if (h.TryGetComponent<PlayerHealth>(out var ph))
            {
                ph.TakeDamage(meleeDamage);
                // (optional) apply knockback here if you want
            }
        }
        // TODO: play swipe SFX / animation if you have them
    }

    void ShootAtPlayer()
    {
        if (!projectilePrefab) return;
        Vector2 dir = (player.position - firePoint.position).normalized;
        var proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        proj.Launch(dir, projectileSpeed, projectileDamage, whatIsPlayer);
        // TODO: play roar/throw SFX
    }

    void FacePlayer()
    {
        float dx = player.position.x - transform.position.x;
        if (dx > faceFlipThreshold && !facingRight) Flip();
        else if (dx < -faceFlipThreshold && facingRight) Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        var s = transform.localScale; s.x *= -1f; transform.localScale = s;
    }

    void OnDrawGizmosSelected()
    {
        if (meleePoint)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(meleePoint.position, meleeRange);
        }
    }
}