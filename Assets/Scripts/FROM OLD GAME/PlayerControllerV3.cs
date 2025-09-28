using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerV3 : MonoBehaviour{

    public float speed;
    public float jumpforce;
    private float moveInput;

    private Rigidbody2D rb;

    public bool facingright = true;
    public bool isGrounded;
    public Transform GroundCheck;
    public float checkRadius;
    public LayerMask WhatIsGround;

    private int extraJumps;
    public int maxJumpsValue;

    private Vector3 respawnPoint;
    public GameObject fallDetector;


    // Start is called before the first frame update
    void Start()
    {
        extraJumps = maxJumpsValue;
        respawnPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrounded == true)
        {
            extraJumps = maxJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.linearVelocity = Vector2.up * jumpforce;
            extraJumps--;
        } else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.linearVelocity = Vector2.up * jumpforce;
        }


    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, WhatIsGround);

        moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        if (facingright == false && moveInput > 0)
        {
            flip();
        }
        else if (facingright == true && moveInput < 0)
        {
            flip();
        }
    }

    void flip()
    {
        facingright = !facingright;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        } 
        if (collision.tag == "Checkpoint"){
            respawnPoint = transform.position;
        }
        if (collision.tag == "Trans"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
        if(collision.tag == "Lava")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}