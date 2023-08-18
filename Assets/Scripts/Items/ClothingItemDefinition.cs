using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Clothing Item", menuName = "Items/Create Clothing Item")]
public class ClothingItemDefinition : ItemDefinition
{
    public AnimController prefab;
    public ClothingType clothingType;

    public override Item GetItem() => new ClothingItem(prefab, clothingType, UIsprite, Color, Price);

}

public class ClothingItem : Item, IUsableItem
{
    public AnimController Anim;
    public ClothingType ClothingType;

    public ClothingItem(AnimController anim, ClothingType clothingType, Sprite uIsprite, Color color, int price) : base(uIsprite, color, price)
    {
        this.Anim = anim;
        this.ClothingType = clothingType;
    }
    bool isEquipped;
    public string actionName => isEquipped ? "Unequip" : "Equip";
    Action<IUsableItem> onUse;
    public Action<IUsableItem> OnUse { get => onUse; set => onUse = value; }
    public void Use()
    {
        if (!isEquipped)
        {
            PlayerController.Instance.ClothingController.Equip(this);
            isEquipped = true;
        }
        else
        {
            PlayerController.Instance.ClothingController.Unequip(this.ClothingType);
            isEquipped = false;
        }

        onUse?.Invoke(this);
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
    Action<IUsableItem> OnUse { get; set; }
}
