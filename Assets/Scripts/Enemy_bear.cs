using UnityEngine;

public class Enemy_bear : MonoBehaviour
{
    [Header("Stats")]
    public int health = 3;

    [Header("Contact Damage")]
    public int contactDamage = 1;
    public float contactCooldown = 0.5f; // seconds between damage ticks while touching

    Animator anim;
    float contactTimer; // cooldown timer between hits

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // die
        if (health <= 0)
        {
            Destroy(gameObject);
            return;
        }

        // tick cooldown
        if (contactTimer > 0f) contactTimer -= Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("enemy damaged, health remaining: " + health);
    }

    // ---------- CONTACT DAMAGE ----------

    // Use these if your enemy/player colliders are NOT triggers:
    void OnCollisionEnter2D(Collision2D col)  { TryDamagePlayer(col.gameObject); }
    void OnCollisionStay2D(Collision2D col)   { TryDamagePlayer(col.gameObject); }

    // Use these if your enemy collider IS a trigger:
    void OnTriggerEnter2D(Collider2D other)   { TryDamagePlayer(other.gameObject); }
    void OnTriggerStay2D(Collider2D other)    { TryDamagePlayer(other.gameObject); }

    void TryDamagePlayer(GameObject obj)
    {
        if (contactTimer > 0f) return; // still on cooldown

        // Player must have a PlayerHealth component
        if (obj.TryGetComponent<PlayerHealth>(out var playerHealth))
        {
            playerHealth.TakeDamage(contactDamage);
            contactTimer = contactCooldown; // reset cooldown so we don't tick every frame
        }
    }
}