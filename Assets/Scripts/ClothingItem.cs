using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingItem : Item, IUsableItem
{
    public AnimController anim;
    public ClothingType clothingType;
    
    public string actionName => "Equip";

    public void Use()
    {
        PlayerController.Instance.Equip(this);
    }


}

public enum ClothingType 
{ 
    Head,
    Suit,
    Weapon
}


public interface IUsableItem 
{
    string actionName { get; }
    void Use();
}
