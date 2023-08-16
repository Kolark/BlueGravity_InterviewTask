using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float t;
    bool IsMoving = false;
    float horizontal = 0;
    float vertical = 0;
    Animator animator;
    [SerializeField] Animator head;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {

        if (IsMoving)
        {
            animator.SetFloat("X", horizontal);
            animator.SetFloat("Y", vertical);
            head.SetFloat("X", horizontal);
            head.SetFloat("Y", vertical);
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        t = horizontal;
        IsMoving = horizontal != 0 || vertical != 0;
        animator.SetBool("IsMoving", IsMoving);
        head.SetBool("IsMoving", IsMoving);

        rb.velocity = new Vector2(horizontal, vertical).normalized * 5;
        //rb.AddForce(new Vector2(horizontal, vertical).normalized * 5, ForceMode2D.Impulse);
    }
}
