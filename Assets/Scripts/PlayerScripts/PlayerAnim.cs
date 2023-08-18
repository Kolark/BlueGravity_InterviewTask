using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public Dictionary<ClothingType , ClothingItem> animControllers = new Dictionary<ClothingType, ClothingItem>();

    public void SetDirection(Vector2 dir)
    {
        foreach (var a in animControllers.Values.Select(p => p.anim))
        {
            a.SetDirection(dir);
        }
    }

    public void SetMoving(bool IsMoving) { foreach (var a in animControllers.Values.Select(p => p.anim)) a.SetMoving(IsMoving); }

    public void Equip(ClothingType clothingType ,AnimController anim)
    {
        if(animControllers.TryGetValue(clothingType, out var currentAnim))
        {
            Unequip(clothingType);
        }
        animControllers[clothingType] = anim;
    }

    public void Unequip(ClothingType clothingType)
    {
        if (animControllers.Remove(clothingType, out var animController)) 
        { 
        
        };
    }
}
