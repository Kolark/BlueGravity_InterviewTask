using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    Animator anim;
    private void Awake() => anim = GetComponent<Animator>();
    public void SetDirection(Vector2 dir)
    {
        anim.SetFloat("X", dir.x);
        anim.SetFloat("Y", dir.y);
    }

    public void SetMoving(bool IsMoving) => anim.SetBool("IsMoving", IsMoving);
}
