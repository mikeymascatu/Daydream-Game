using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    private Rigidbody2D rb;
    public PlayerControllerV3 player;
    private int maxUses;
    public int maxUseValue;

    private bool top;

    // Start is called before the first frame update
    void Start(){
        player = GetComponent<PlayerControllerV3>();
        rb = GetComponent<Rigidbody2D>();
    }



    // Update is called once per frame
    void Update(){
        if (player.isGrounded == true)
        {
            maxUses = maxUseValue;
        }

        if (Input.GetKeyDown(KeyCode.E) && maxUses > 0)
        {
            rb.gravityScale *= -1;
            Rotation();
            maxUses--;
        }
        else if (Input.GetKeyDown(KeyCode.E) && maxUses == 0 && player.isGrounded == true)
        {
            rb.gravityScale *= -1;
            Rotation();
        }
    }

    void Rotation(){
        if(top == false){
            transform.eulerAngles = new Vector3(0, 0, 180f);
        } else {
            transform.eulerAngles = Vector3.zero;
        }
        player.facingright = !player.facingright;
        top = !top;
    }
}
