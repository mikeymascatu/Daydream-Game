using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : MonoBehaviour {
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public int extraJumpsValue = 2;
    private int extraJumps;


    void Start() {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");
        Debug.Log(moveInput);
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (facingRight == false && moveInput > 0) {
            Flip();
        } else if (facingRight == true && moveInput < 0) {
            Flip();
        }
    }

    void Update() {
        if (isGrounded == true) {
            extraJumps = extraJumpsValue;
        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0) {
            rb.linearVelocity = Vector2.up * jumpForce;
            extraJumps--;
        } else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true) {
            rb.linearVelocity = Vector2.up * jumpForce;
        }
    }

    void Flip(){
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    
}