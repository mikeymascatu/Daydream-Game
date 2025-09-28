using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BossProjectile : MonoBehaviour
{
    [SerializeField] float lifeSeconds = 6f;
    [SerializeField] bool destroyOnWalls = true;
    [SerializeField] LayerMask whatIsPlayer; // set by Launch
    int damage;
    Vector2 velocity;

    public void Launch(Vector2 dir, float speed, int dmg, LayerMask playerMask)
    {
        velocity = dir.normalized * speed;
        damage = dmg;
        whatIsPlayer = playerMask;
        transform.right = dir; // orient sprite if you want
        Invoke(nameof(Die), lifeSeconds);
    }

    void Update()
    {
        transform.position += (Vector3)(velocity * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Hit player?
        if (((1 << other.gameObject.layer) & whatIsPlayer) != 0)
        {
            if (other.TryGetComponent<PlayerHealth>(out var ph))
                ph.TakeDamage(damage);
            Die();
            return;
        }

        // Optional: die on any collider that isn't the boss
        if (destroyOnWalls && other.gameObject != gameObject)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}