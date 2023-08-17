using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public List<AnimController> animControllers = new List<AnimController>();

    public void SetDirection(Vector2 dir)
    {
        foreach (var a in animControllers)
        {
            a.SetDirection(dir);
        }
    }

    public void SetMoving(bool IsMoving) { foreach (var a in animControllers) a.SetMoving(IsMoving); }

    public void Add(AnimController anim)
    {
        animControllers.Add(anim);
    }

    public void Remove(AnimController anim)
    {
        animControllers.Remove(anim);
    }
}
