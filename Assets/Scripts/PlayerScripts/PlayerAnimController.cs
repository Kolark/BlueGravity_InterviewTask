using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Helps control animator parameter in an element whose animation is tied to to the player movement
public class PlayerAnimController : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spriteRend;
    
    private void Awake()
    {
        anim       = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color color) => spriteRend.color = color;

    public void SetDirection(Vector2 dir)
    {
        anim.SetFloat("X", dir.x);
        anim.SetFloat("Y", dir.y);
    }

    public void SetMoving(bool IsMoving) => anim.SetBool("IsMoving", IsMoving);
}
