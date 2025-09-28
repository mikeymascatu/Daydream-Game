using UnityEngine;

public class AnimationRun_enemy : MonoBehaviour
{
    [SerializeField] Animator anim;          // assign the Animator that drives your run/idle
    [SerializeField] Rigidbody2D rb;         // assign the Player's Rigidbody2D

    static readonly int XVelocity = Animator.StringToHash("xVelocity");

    void Awake()
    {
        if (!anim) anim = GetComponent<Animator>();                  // if the Animator is on this GO
        if (!rb)   rb   = GetComponentInParent<Rigidbody2D>();        // else auto-find on a parent (e.g., Player)
        // If your Rigidbody2D is elsewhere, drag it in the Inspector instead of relying on auto-find.
    }

    void Update()
    {
        if (!anim || !rb) return;
        anim.SetFloat(XVelocity, Mathf.Abs(rb.linearVelocityX));           // <-- use velocity.x
    }
}