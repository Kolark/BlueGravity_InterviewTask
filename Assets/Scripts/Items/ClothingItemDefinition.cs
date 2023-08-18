using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Clothing Item", menuName = "Items/Create Clothing Item")]
public class ClothingItemDefinition : ItemDefinition
{
    public PlayerAnimController prefab;
    public ClothingType clothingType;

    public override Item GetItem() => new ClothingItem(prefab, clothingType, UIsprite, Color, Price);
}

//Expands item data class and adds the necesary fields to represent clothes in the game, it also contains the methods related to its use
public class ClothingItem : Item, IUsableItem
{
    public PlayerAnimController Anim;
    public ClothingType ClothingType;
    public string actionName => IsEquipped ? "Unequip" : "Equip";
    
    Action<IUsableItem> onUse;
    public Action<IUsableItem> OnUse { get => onUse; set => onUse = value; }

    public bool IsEquipped = false;

    public ClothingItem(PlayerAnimController anim, ClothingType clothingType, Sprite uIsprite, Color color, int price) : base(uIsprite, color, price)
    {
        this.Anim = anim;
        this.ClothingType = clothingType;
    }

    //Equips if its current not equiped and Unequips if its currently equiped
    public void Use()
    {
        if (!IsEquipped)
        {
            PlayerController.Instance.ClothingController.Equip(this);
            IsEquipped = true;
        }
        else
        {
            PlayerController.Instance.ClothingController.Unequip(this.ClothingType);
            IsEquipped = false;
        }

        onUse?.Invoke(this);
    }
}

//Types of clothing currently defined
public enum ClothingType
{
    Head,
    Suit,
    Weapon
}

