using UnityEngine;

/// <summary>
/// PlayerControllerV2 (no animation):
/// - Horizontal movement with Rigidbody2D in FixedUpdate
/// - Multi-jump with reset on ground
/// - Ground check via OverlapCircle
/// - Sprite flip
/// </summary>
public class PlayerControllerV2 : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 6f;
    [SerializeField] float jumpForce = 12f;

    [Header("Jumping")]
    [SerializeField] int extraJumpsValue = 1; // additional jumps while airborne

    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;    // assign in Inspector (child at feet)
    [SerializeField] float checkRadius = 0.15f;
    [SerializeField] LayerMask whatIsGround;

    Rigidbody2D rb;
    bool facingRight = true;
    bool isGrounded;
    int extraJumps;
    float moveInput; // read in Update, applied in FixedUpdate

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!rb)
        {
            Debug.LogError("[PlayerControllerV2] Missing Rigidbody2D.", this);
            enabled = false; return;
        }
    }

    void Start()
    {
        extraJumps = extraJumpsValue;
    }

    void Update()
    {
        // Gather input
        moveInput = Input.GetAxisRaw("Horizontal"); // -1, 0, 1

        // Jump handling
        if (isGrounded) extraJumps = extraJumpsValue;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded || extraJumps > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                if (!isGrounded) extraJumps--;
            }
        }
    }

    void FixedUpdate()
    {
        // Ground check (null-safe)
        isGrounded = groundCheck
            ? Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround)
            : false;

        // Horizontal movement (preserve Y velocity from gravity/jumps)
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // Flip sprite to face movement direction
        if (moveInput > 0f && !facingRight) Flip();
        else if (moveInput < 0f && facingRight) Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        var s = transform.localScale;
        s.x *= -1f;
        transform.localScale = s;
    }

    void OnDrawGizmosSelected()
    {
        if (!groundCheck) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
